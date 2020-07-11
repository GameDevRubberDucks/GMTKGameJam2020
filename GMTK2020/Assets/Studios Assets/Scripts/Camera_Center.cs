using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Camera_Center : MonoBehaviour
{
    // Start is called before the first frame update

    //Change with appropriate script
    public Temp_Room[] rooms;
    public Temp_Room[] roomsAttached;

    public Vector3 cameraOffset = new Vector3(0.0f,0.0f,0.0f);
    public Vector3 baseCameraPos = new Vector3 (2.5f,0.0f,-10.0f);
    public int baseCameraSize = 5;


    void Start()
    {

        AdjustCamera();
    }

    // Update is called once per frame
    void Update()
    {
        //This will change the zoom of the camera, might be an easier way but oh well
        this.GetComponent<Camera>().orthographicSize = rooms.Length + baseCameraSize;


        if (Input.GetKeyDown(KeyCode.Q))
        {
            AdjustCamera();
        }
    }


    //call this whenever a new room is attached
    void AdjustCamera()
    {
        //Reset Camera
        cameraOffset = new Vector3(2.5f, 0.0f, 0.0f);
        //Finds all the rooms in the scene
        //---Change with the right script
        //Then need to check if they are attached
        rooms = FindObjectsOfType<Temp_Room>();

        //loops through all the rooms in the scene finds which ones are attached and places it into another list
        //for (int x = 0; x <= rooms.Length - 1; x++)
        //{
        //    if (rooms[x].isAttached)
        //        roomsAttached[x] = rooms[x];
        //}

        //Loops through all of the attached rooms and finds the center of mass
        //Adds them all their local positions and averages
        for (int i = 0; i <= rooms.Length - 1; i++)
        {
            cameraOffset += rooms[i].GetComponent<Transform>().position;
        }
        cameraOffset /= rooms.Length;

        //Add the base cameraoffset
        cameraOffset += baseCameraPos;

        //Move the camera
        this.transform.position = cameraOffset;
    }
}
