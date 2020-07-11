using UnityEngine;
using System.Collections.Generic;

public class Room_Node
{
    //--- Public Variables ---//
    public Vector2 m_gridLoc;
    public GameObject m_roomObj;
    public Room m_roomScript;
    public Room_Node[] m_neighbours;



    //--- Constructors ---//
    public Room_Node(Vector2 _gridLoc, GameObject _roomObj)
    {
        m_gridLoc = _gridLoc;
        m_roomObj = _roomObj;
        m_roomScript = m_roomObj.GetComponent<Room>();
        m_neighbours = new Room_Node[(int)Room_AttachPoint.Num_AttachPoints];
    }



    //--- Getters ---//
    public bool GetHasOpenNeighbour()
    {
        // If there is at least one open neighbour slot, this is true
        return (GetOpenNeighbours().Count > 0);
    }

    public List<Room_AttachPoint> GetOpenNeighbours()
    {
        // Keep track of the open ones
        List<Room_AttachPoint> m_openAttachPoints = new List<Room_AttachPoint>();

        // If the neighbour is open, add its attachment point to the list
        for(int i = 0; i < m_neighbours.Length; i++)
        {
            if (m_neighbours[i] == null)
                m_openAttachPoints.Add((Room_AttachPoint)i);
        }

        // Return the list of open points
        return m_openAttachPoints;
    }

    public Vector2 GetNeighbourCoord(Room_AttachPoint _attachPoint)
    {
        // Determine the grid location of the given neighbour
        switch(_attachPoint)
        {
            case Room_AttachPoint.Right:
                return this.m_gridLoc + Vector2.right;

            case Room_AttachPoint.Bottom:
                return this.m_gridLoc + Vector2.down;

            case Room_AttachPoint.Left:
                return this.m_gridLoc + Vector2.left;

            default:
            case Room_AttachPoint.Top:
                return this.m_gridLoc + Vector2.up;
        }
    }
}
