using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player_Control : MonoBehaviour
{

    //--- Public Variables ---//

    //The higher the slow the player
    public float movementSpeed = 50.0f;

    //--- Private Variables ---//
    public Vector2 movement = new Vector2(0.0f, 0.0f);

    public Transform ship;


    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        movement = new Vector2 (Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical")) / movementSpeed;
        

       //if (Input.GetAxis("Interact") > 0.0f)
       // {
       //     Debug.Log(Input.GetAxis("Interact"));
       // }
    }

    private void FixedUpdate()
    {
        this.transform.localPosition += new Vector3(movement.x, movement.y, 0.0f);
    }
}
