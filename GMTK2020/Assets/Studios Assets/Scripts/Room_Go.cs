using UnityEngine;

public class Room_Go : MonoBehaviour, IRoom_Interactable
{
    //--- Setup Variables ---//
    public Ship_GridManager ship;
    public ParticleSystem thrusterParticles;
    private Rigidbody2D shipRB;
    private Audio_Manager audioManager;

    //--- Public Variables ---///
    public float maxSpeed = 50.0f;
    public float acceleration = 0.2f;
    public float currentSpeed = 0.0f;

    //--- IRoom_Interactable Interface ---//
    public void Awake()
    {
        ship = FindObjectOfType<Ship_GridManager>();
        shipRB = ship.GetComponent<Rigidbody2D>();
        audioManager = FindObjectOfType<Audio_Manager>();
    }
    public void OnInteractionStart()
    {
        thrusterParticles.Play();
        audioManager.PlaySFX(Audio_SFX.Engine);
    }

    public void OnInteraction()
    {
        //shipRB.AddForce(new Vector2(1.0f,0.0f) * 5.0f);
        if ( currentSpeed < maxSpeed)
        {
            currentSpeed += acceleration;
        }
        ship.transform.position += ship.transform.right * currentSpeed * Time.deltaTime;
    }

    public void OnInteractionEnd()
    {
        thrusterParticles.Stop();
        audioManager.StopSFX(Audio_SFX.Engine);
    }

    public void OnNoInteraction()
    {
        //shipRB.AddForce(new Vector2(-1.0f, 0.0f) * 0.5f);

        if (currentSpeed > 0.0f)
        {
            currentSpeed -= acceleration;
        }
        ship.transform.position += ship.transform.right * currentSpeed * Time.deltaTime;
        thrusterParticles.Stop();
    }
}
