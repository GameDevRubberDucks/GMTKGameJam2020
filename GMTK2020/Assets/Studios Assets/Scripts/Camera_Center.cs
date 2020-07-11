﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Camera_Center : MonoBehaviour
{
  
    //--- Public Variables ---///
    //Change with appropriate script
    public Temp_Room[] rooms;

    public Vector3 cameraOffset = new Vector3(0.0f,0.0f,0.0f);
    public Vector3 baseCameraPos = new Vector3 (2.5f,0.0f,-10.0f);
    public int baseCameraSize = 5;

    public Transform ship;

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
    void AdjustCamera()
    {
        //Reset Camera
        cameraOffset = new Vector3(2.5f, 0.0f, 0.0f);
        //Finds all the rooms in the scene
        //---Change with the right script
        //Then need to check if they are attached
        rooms = FindObjectsOfType<Temp_Room>();

        //Loops through all of the attached rooms and finds the center of mass
        //Adds them all their local positions and averages
        for (int i = 0; i <= rooms.Length - 1; i++)
        {
            if (rooms[i].isAttached)
            {
                cameraOffset += rooms[i].GetComponent<Transform>().position;
            }
        }
        cameraOffset /= rooms.Length;

        //Add the base cameraoffset
        cameraOffset += baseCameraPos;
        
        //This will change the zoom of the camera, might be an easier way but oh well
        this.GetComponent<Camera>().orthographicSize = rooms.Length + baseCameraSize;
    }


    void CameraFollow()
    {
        //cameraoffset is the calculated offset from the rooms. BasecameraPos is the overall offset to the right
        Vector3 desiredPosition = ship.position + cameraOffset + baseCameraPos;
        Vector3 smoothPosition = Vector3.Lerp(transform.position, desiredPosition, 0.125f);
        this.transform.position = new Vector3 (smoothPosition.x,smoothPosition.y,-10.0f); //-10.0 because thats how it works
    }
}