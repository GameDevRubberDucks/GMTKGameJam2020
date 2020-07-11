using UnityEngine;

public class Room_Drive : MonoBehaviour, IRoom_Interactable
{
    //--- IRoom_Interactable Interface ---//
    public void OnInteraction()
    {
        Debug.Log("Interaction()");
    }

    public void OnInteractionEnd()
    {
        Debug.Log("InteractionEnd()");
    }
}