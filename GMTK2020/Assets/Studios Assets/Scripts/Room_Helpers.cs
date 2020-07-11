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
    void OnInteraction();
    void OnInteractionEnd();
}