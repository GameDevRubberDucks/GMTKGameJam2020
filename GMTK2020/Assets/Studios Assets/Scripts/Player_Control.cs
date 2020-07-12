using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player_Control : MonoBehaviour
{

    //--- Public Variables ---//

    //The higher the slow the player
    public float movementSpeed = 50.0f;

    public Vector2 movement = new Vector2(0.0f, 0.0f);

    public Animator charAnim;

    //--- Private Variables ---//
    private int animDir;
    private bool animIsWalking;

    void Start()
    {
        //Init private variables
        animDir = 0;
        animIsWalking = false;
    }

    // Update is called once per frame
    void Update()
    {
        movement =  new Vector2 (Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical")) ;
        movement.Normalize();
        movement *= movementSpeed * Time.fixedDeltaTime;

        //if (Input.GetAxis("Interact") > 0.0f)
        // {
        //     Debug.Log(Input.GetAxis("Interact"));
        // }

        DetermineAnimParams();
    }

    private void FixedUpdate()
    {
       this.transform.localPosition += new Vector3(movement.x, movement.y, 0.0f);
    }

    private void DetermineAnimParams()
    {
        //Facing front/down
        if (Input.GetKeyDown(KeyCode.S))
            animDir = 1;
        //Facing right
        else if (Input.GetKeyDown(KeyCode.D))
            animDir = 2;
        //Facing back/up
        else if (Input.GetKeyDown(KeyCode.W))
            animDir = 3;
        //Facing left
        else if (Input.GetKeyDown(KeyCode.A))
            animDir = 4;

        //Set is walking to true if the char is moving at all
        if (Input.anyKey)
            animIsWalking = true;
        else
            animIsWalking = false;

        charAnim.SetInteger("Direction", animDir);
        charAnim.SetBool("isWalking", animIsWalking);
    }

}
