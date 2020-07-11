using UnityEngine;

public enum Room_AttachPoint
{
    Top,
    Right,
    Bottom,
    Left,

    Num_AttachPoints
}

public interface IRoom_Interactable
{
    void OnInteractionStart();
    void OnInteraction();
    void OnInteractionEnd();
    void OnNoInteraction();
}