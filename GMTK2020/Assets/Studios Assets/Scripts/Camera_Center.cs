using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Camera_Center : MonoBehaviour
{
  
    //--- Public Variables ---///
    //Change with appropriate script
    public Room[] rooms;

    public Vector3 cameraOffset = new Vector3(0.0f,0.0f,0.0f);
    public Vector3 baseCameraPos = new Vector3 (2.5f,0.0f,-10.0f);
    public int baseCameraSize = 5;
    public float cameraIncreaseAmount = 1.0f;

    public Transform ship;
    public Ship_GridManager shipManager;

    public float shipBounds = 10.0f;
    public float shipBoundsMultiplier = 1.25f;
    public float minOrthoSize = 20.0f;

    // Start is called before the first frame update
    void Start()
    {

        AdjustCamera();
    }

    // Update is called once per frame
    void FixedUpdate()
    {


        if (Input.GetKeyDown(KeyCode.Q))
        {
            AdjustCamera();
        }

        CameraFollow();
    }


    //call this whenever a new room is attached
    public void AdjustCamera()
    {
        //Reset Camera
        cameraOffset = new Vector3(0.0f, 0.0f, 0.0f);
        //Finds all the rooms in the scene
        //---Change with the right script
        //Then need to check if they are attached
        rooms = FindObjectsOfType<Room>();
        int numAttached = 0;

        //Loops through all of the attached rooms and finds the center of mass
        //Adds them all their local positions and averages
        for (int i = 0; i <= rooms.Length - 1; i++)
        {
            if (rooms[i].IsAttached)
            {
                cameraOffset += rooms[i].GetComponent<Transform>().localPosition;
                numAttached++;
            }
        }
        cameraOffset /= (float)numAttached;

        //Add the base cameraoffset
        cameraOffset += baseCameraPos;

        //This will change the zoom of the camera, might be an easier way but oh well
        //this.GetComponent<Camera>().orthographicSize = (cameraIncreaseAmount * numAttached) + baseCameraSize;
        //this.GetComponent<Camera>().orthographicSize = shipBounds * shipBoundCurve.Evaluate(shipBounds);
        //this.GetComponent<Camera>().orthographicSize = shipBounds * Screen.height / Screen.width * 0.5f;
        this.GetComponent<Camera>().orthographicSize = Mathf.Max(shipBounds * shipBoundsMultiplier, minOrthoSize);
    }


    void CameraFollow()
    {
        //lerp not working
        //cameraoffset is the calculated offset from the rooms. BasecameraPos is the overall offset to the right
        //Vector3 desiredPosition = ship.position + cameraOffset + baseCameraPos;
        //Vector3 smoothPosition = Vector3.Lerp(transform.position, desiredPosition, 0.125f);
        //this.transform.position = new Vector3 (smoothPosition.x,smoothPosition.y,-10.0f); //-10.0 because thats how it works
        
        //Hard Set
        this.transform.localPosition = cameraOffset; //-10.0 because thats how it works
        this.transform.localPosition += new Vector3(0.0f, 0.0f, -10.0f);
    }
}
