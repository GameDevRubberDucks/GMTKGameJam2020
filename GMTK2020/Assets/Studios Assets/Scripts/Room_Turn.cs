using UnityEngine;

public class Room_Turn : MonoBehaviour, IRoom_Interactable
{
    //--- Setup Variables ---//
    public Transform pivot;
    public Camera_Center camPos;
    public Ship_GridManager ship;

    public float rotateSpeed = 10.0f;

    public Vector3 centerMass;
    //--- IRoom_Interactable Interface ---//

    public void Awake()
    {
        camPos = FindObjectOfType<Camera_Center>();
        ship = FindObjectOfType<Ship_GridManager>();
    }
    public void OnInteractionStart()
    {
        Debug.Log("InteractionStart()");
        //pivot.localPosition = camPos.cameraOffset;
        //pivot.localPosition += new Vector3(0.0f, 0.0f, 10.0f);
        centerMass = camPos.cameraOffset + ship.transform.position;
        centerMass += new Vector3(0.0f, 0.0f, 10.0f);
    }

    public void OnInteraction()
    {
        Debug.Log("Interaction()");
        //pivot.RotateAround(pivot.lo pivot.forward * 0.5f );
        ship.transform.RotateAround(centerMass,Vector3.forward, rotateSpeed * Time.deltaTime);
        Debug.DrawLine(centerMass, Vector3.zero,Color.yellow,1.0f,false);
    }

    public void OnInteractionEnd()
    {
        Debug.Log("InteractionEnd()");
    }

    public void OnNoInteraction()
    {
        Debug.Log("NoInteraction()");
    }
}