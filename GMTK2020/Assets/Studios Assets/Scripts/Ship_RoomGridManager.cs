using UnityEngine;
using System.Collections.Generic;

public class Ship_RoomGridManager : MonoBehaviour
{
    //--- Public Variables ---//
    public GameObject m_startRoom;
    public float m_roomWorldSize;
    public Transform m_roomParent;



    //--- Private Variables ---//
    public Dictionary<Vector2, Room_Node> m_roomGrid;



    //--- Unity Methods ---//
    private void Start()
    {
        // Initialize the room grid with the starting room
        m_roomGrid = new Dictionary<Vector2, Room_Node>();
        m_roomGrid.Add(Vector2.zero, new Room_Node(Vector2.zero, m_startRoom));
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        // When we collide with a room floating in space, we should add it to the ship
        if (collision.tag == "FloatingRoom")
            AddRoom(collision.gameObject);
    }



    //--- Methods ---//
    public void AddRoom(GameObject _newRoom)
    {
        // Randomly determine the grid coordinates that the new piece will go to
        Vector2 gridPlacement = RandomGridLocation();

        // Add the new room to the grid at the given coordinates
        Room_Node newNode = CreateRoomNode(gridPlacement, _newRoom);

        // Put the room object into the correct world space coordinates
        PlaceRoomInWorld(newNode);

        // Open the required doors for the new room and its neighbours
        // Also, set up the links within the nodes themselves
        LinkNeighbouringRooms(newNode);
    }



    //--- Utility Functions ---//
    private Vector2 RandomGridLocation()
    {
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
        // Move the actual gameobject to the right place in Unity's world grid
        // worldCoordinates = gridCoordinates * roomWorldSize
        var roomTransform = _roomNode.m_roomObj.transform;
        roomTransform.position = _roomNode.m_gridLoc * m_roomWorldSize;

        // Make the room a child so it moves with the ship
        roomTransform.parent = m_roomParent;
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
}
