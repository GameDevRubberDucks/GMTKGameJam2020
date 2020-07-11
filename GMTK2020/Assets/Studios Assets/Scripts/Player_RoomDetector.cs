using UnityEngine;

public class Player_RoomDetector : MonoBehaviour
{
    //--- Unity Methods ---//
    private void OnTriggerEnter2D(Collider2D collision)
    {
        // When entering a room, set it to be occupied
        var roomComp = collision.gameObject.GetComponentInParent<Room>();

        if (roomComp != null)
        {
            roomComp.IsOccupied = true;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        // When leaving a room, set it to be empty
        var roomComp = collision.gameObject.GetComponentInParent<Room>();

        if (roomComp != null)
        {
            roomComp.IsOccupied = false;
        }
    }
}
