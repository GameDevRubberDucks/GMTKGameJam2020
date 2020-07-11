using UnityEngine;
using System.Collections.Generic;

public class Ship_GridManager : MonoBehaviour
{
    //--- Public Variables ---//
    public GameObject m_startRoom;
    public float m_roomWorldSize;
    public Transform m_roomParent;
    public Camera_Center m_cameraCenter;
    public GameObject m_thruster;
    public GameObject m_gun;



    //--- Private Variables ---//
    private Dictionary<Vector2, Room_Node> m_roomGrid;
    private Room_Node m_lastThrusterAttachment;
    private Room_Node m_lastGunAttachment;
    private bool m_hasGun;



    //--- Unity Methods ---//
    private void Start()
    {
        // Initialize the private variables
        m_lastThrusterAttachment = null;
        m_lastGunAttachment = null;
        m_hasGun = false;

        // Initialize the room grid with the starting room
        m_roomGrid = new Dictionary<Vector2, Room_Node>();
        AddRoom(m_startRoom);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        // When we collide with a room floating in space, we should add it to the ship
        if (collision.tag == "Room_Floating")
            AddRoom(collision.gameObject);
    }



    //--- Methods ---//
    public void AddRoom(GameObject _newRoom)
    {
        // If the room has already been added, back out
        // This can happen as the trigger overlaps more than one collider?
        if (GetGridAlreadyHasRoom(_newRoom))
            return;

        // Randomly determine the grid coordinates that the new piece will go to
        Vector2 gridPlacement = RandomGridLocation();

        // Add the new room to the grid at the given coordinates
        Room_Node newNode = CreateRoomNode(gridPlacement, _newRoom);

        // Put the room object into the correct world space coordinates
        PlaceRoomInWorld(newNode);

        // Open the required doors for the new room and its neighbours
        // Also, set up the links within the nodes themselves
        LinkNeighbouringRooms(newNode);

        // Update the camera
        m_cameraCenter.AdjustCamera();

        // Place the thruster along the ship
        PlaceThruster();

        // Optionally place the gun
        PlaceGun(newNode);
    }



    //--- Utility Functions ---//
    private Vector2 RandomGridLocation()
    {
        // If the grid is empty, the first room is always at (0,0)
        if (m_roomGrid.Values.Count == 0)
            return Vector2.zero;

        // Find all of the rooms that have at least one spot open and store them
        List<Room_Node> m_eligibleRooms = new List<Room_Node>();
        foreach(Room_Node roomNode in m_roomGrid.Values)
        {
            if (roomNode.GetHasOpenNeighbour())
                m_eligibleRooms.Add(roomNode);
        }

        // Randomly select one of the rooms
        var randomRoomIdx = Random.Range(0, m_eligibleRooms.Count);
        Room_Node selectedRoom = m_eligibleRooms[randomRoomIdx];

        // Now, randomly select one of the open neighbour attachment points
        var openAttachmentPoints = selectedRoom.GetOpenNeighbours();
        var randomAttachIdx = Random.Range(0, openAttachmentPoints.Count);
        Room_AttachPoint selectedAttachPoint = openAttachmentPoints[randomAttachIdx];

        // Finally, we can return the grid location corresponding to that attachment point
        return selectedRoom.GetNeighbourCoord(selectedAttachPoint);
    }

    private Room_Node CreateRoomNode(Vector2 _gridLocation, GameObject _roomObj)
    {
        // Create a new room node with the given information
        Room_Node newNode = new Room_Node(_gridLocation, _roomObj);

        // Add the node to the grid itself
        m_roomGrid.Add(_gridLocation, newNode);

        // Return the newly created node
        return newNode;
    }

    private void PlaceRoomInWorld(Room_Node _roomNode)
    {
        // Make the room a child so it moves with the ship
        var roomTransform = _roomNode.m_roomObj.transform;
        roomTransform.parent = m_roomParent;

        // Move the actual gameobject to the right place in Unity's world grid
        // localCoordinates = gridCoordinates * roomWorldSize
        roomTransform.localPosition = _roomNode.m_gridLoc * m_roomWorldSize;

        // Trigger the room's attachment code
        _roomNode.m_roomScript.OnAttachedToShip();
    }

    private void LinkNeighbouringRooms(Room_Node _roomNode)
    {
        // Link all of the neighbours
        for(int i = 0; i < (int)Room_AttachPoint.Num_AttachPoints; i++)
        {
            var currentAttachPoint = (Room_AttachPoint)i;
            OpenLinkingDoors(currentAttachPoint, GetOppositeAttachPoint(currentAttachPoint), _roomNode);
        }
    }

    private void OpenLinkingDoors(Room_AttachPoint _thisDoor, Room_AttachPoint _neighbourDoor, Room_Node _thisNode)
    {
        // Find the grid location of the neighbour at the given door
        Vector2 neighbourLoc = _thisNode.GetNeighbourCoord(_thisDoor);

        // Determine if there is a room at that location
        Room_Node neighbourRef = GetRoomRef(neighbourLoc);

        // If there is a room, we should link the rooms together
        if (neighbourRef != null)
        {
            // Link the neighbour to this node
            neighbourRef.m_roomScript.ToggleDoor(_neighbourDoor, true);
            neighbourRef.m_neighbours[(int)_neighbourDoor] = _thisNode;

            // Link this node to the neighbour
            _thisNode.m_roomScript.ToggleDoor(_thisDoor, true);
            _thisNode.m_neighbours[(int)_thisDoor] = neighbourRef;
        }
    }

    private void PlaceThruster()
    {
        // Find the left-most x position currently occupied in the grid
        float leftmostX = Mathf.Infinity;
        foreach (var roomNode in m_roomGrid.Values)
            leftmostX = Mathf.Min(leftmostX, roomNode.m_gridLoc.x);

        // Determine all of the nodes that are at that x position
        List<Room_Node> eligibleNodes = new List<Room_Node>();
        foreach(var roomNode in m_roomGrid.Values)
        {
            if (roomNode.m_gridLoc.x == leftmostX)
                eligibleNodes.Add(roomNode);
        }

        // If the thruster's current attached node is in that list, then there is no need to move
        if (eligibleNodes.Contains(m_lastThrusterAttachment))
            return;

        // Randomly select one of them
        var randomNodeIdx = Random.Range(0, eligibleNodes.Count);
        Room_Node selectedNode = eligibleNodes[randomNodeIdx];
        m_lastThrusterAttachment = selectedNode;

        // Determine the grid location of the selected node's left neighbour
        Vector2 thrusterGridPos = selectedNode.GetNeighbourCoord(Room_AttachPoint.Left);

        // Scale to world space
        Vector2 thrusterLocalPos = thrusterGridPos * m_roomWorldSize;

        // Move the thruster object
        m_thruster.transform.localPosition = thrusterLocalPos;
    }

    private void PlaceGun(Room_Node _newRoom)
    {
        // If the player doesn't already have the gun, we should see if the new room is the gun
        if (!m_hasGun)
        {
            var gunScript = _newRoom.m_roomObj.GetComponent<Room_Boom>();

            if (gunScript != null)
            {
                m_hasGun = true;
                m_gun.SetActive(true);
            }
        }

        // If the player has the gun (either from before or just got it now), we should update its placement if needed
        // Find the right-most x position currently occupied in the grid
        float rightmostX = -Mathf.Infinity;
        foreach (var roomNode in m_roomGrid.Values)
            rightmostX = Mathf.Max(rightmostX, roomNode.m_gridLoc.x);

        // Determine all of the nodes that are at that x position
        List<Room_Node> eligibleNodes = new List<Room_Node>();
        foreach (var roomNode in m_roomGrid.Values)
        {
            if (roomNode.m_gridLoc.x == rightmostX)
                eligibleNodes.Add(roomNode);
        }

        // If the gun's current attached node is in that list, then there is no need to move
        if (eligibleNodes.Contains(m_lastGunAttachment))
            return;

        // Randomly select one of them
        var randomNodeIdx = Random.Range(0, eligibleNodes.Count);
        Room_Node selectedNode = eligibleNodes[randomNodeIdx];
        m_lastGunAttachment = selectedNode;

        // Determine the grid location of the selected node's right neighbour
        Vector2 gunGridPos = selectedNode.GetNeighbourCoord(Room_AttachPoint.Right);

        // Scale to world space
        Vector2 gunLocalPos = gunGridPos * m_roomWorldSize;

        // Move the gun object
        m_gun.transform.localPosition = gunLocalPos;
    }



    //--- Getters ---//
    private Room_Node GetRoomRef(Vector2 _coord)
    {
        // Try to find the room in the grid. If it exists, return it. Otherwise, return null
        Room_Node roomRef;

        if (m_roomGrid.TryGetValue(_coord, out roomRef))
            return roomRef;
        else
            return null;
    }

    private Room_AttachPoint GetOppositeAttachPoint(Room_AttachPoint _attachPoint)
    {
        // Return the opposite attachment point
        switch (_attachPoint)
        {
            case Room_AttachPoint.Right:
                return Room_AttachPoint.Left;

            case Room_AttachPoint.Bottom:
                return Room_AttachPoint.Top;

            case Room_AttachPoint.Left:
                return Room_AttachPoint.Right;

            default:
            case Room_AttachPoint.Top:
                return Room_AttachPoint.Bottom;

        }
    }

    private bool GetGridAlreadyHasRoom(GameObject _room)
    {
        // If the room exists in the grid, return true
        foreach(var roomNode in m_roomGrid.Values)
        {
            if (roomNode.m_roomObj == _room)
                return true;
        }

        // Otherwise, return false
        return false;
    }
}
