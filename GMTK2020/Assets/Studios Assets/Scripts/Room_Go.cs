using UnityEngine;

public class Room_Go : MonoBehaviour, IRoom_Interactable
{

    public Ship_GridManager ship;
    private Rigidbody2D shipRB;

    public float maxSpeed = 50.0f;
    public float acceleration = 0.2f;
    public float currentSpeed = 0.0f;
    //--- IRoom_Interactable Interface ---//
    public void Awake()
    {
        ship = FindObjectOfType<Ship_GridManager>();
        shipRB = ship.GetComponent<Rigidbody2D>();
    }
    public void OnInteractionStart()
    {
        Debug.Log("InteractionStart()");
    }

    public void OnInteraction()
    {
        Debug.Log("Interaction()");
        //shipRB.AddForce(new Vector2(1.0f,0.0f) * 5.0f);
        if ( currentSpeed < maxSpeed)
        {
            currentSpeed += acceleration;
        }
        ship.transform.position += ship.transform.right * currentSpeed * Time.deltaTime;
    }

    public void OnInteractionEnd()
    {
        Debug.Log("InteractionEnd()");
    }

    public void OnNoInteraction()
    {
        Debug.Log("NoInteraction()");
        //shipRB.AddForce(new Vector2(-1.0f, 0.0f) * 0.5f);

        if (currentSpeed > 0.0f)
        {
            currentSpeed -= acceleration;
        }
        ship.transform.position += ship.transform.right * currentSpeed * Time.deltaTime;
    }
}
