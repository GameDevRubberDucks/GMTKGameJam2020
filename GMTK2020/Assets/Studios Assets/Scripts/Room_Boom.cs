using UnityEngine;

public class Room_Boom : MonoBehaviour, IRoom_Interactable
{
    //--- Setup Variables ---//
    public Ship_GridManager ship;
    public Transform spawnPoint;
    public GameObject projectile;
    public ParticleSystem gunParticles;

    //--- Private Setup Variables ---//
    private Camera_Shake camShake;
    private Audio_Manager audioManager;

    // Start is called before the first frame update
    public void Awake()
    {
        ship = FindObjectOfType<Ship_GridManager>();
        camShake = FindObjectOfType<Camera_Shake>();
        audioManager = FindObjectOfType<Audio_Manager>();
    }

    //--- IRoom_Interactable Interface ---//
    public void OnInteractionStart()
    {
        camShake.Shake(0.5f, 0.25f);
        Instantiate(projectile,spawnPoint.position,this.transform.rotation,null);
        gunParticles.Play();
        audioManager.PlaySFX(Audio_SFX.Gun_Shoot);
    }

    public void OnInteraction()
    {

    }

    public void OnInteractionEnd()
    {
    }

    public void OnNoInteraction()
    {
    }
}