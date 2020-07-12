using UnityEngine;

public class Room_Boom : MonoBehaviour, IRoom_Interactable
{
    //--- Setup Variables ---//
    public Ship_GridManager ship;
    public Transform spawnPoint;
    public GameObject projectile;

    // Start is called before the first frame update
    public void Awake()
    {
        ship = FindObjectOfType<Ship_GridManager>();
    }
    //--- IRoom_Interactable Interface ---//
    public void OnInteractionStart()
    {
        Debug.Log("InteractionStart()");
        Instantiate(projectile,spawnPoint.position,this.transform.rotation,null);
    }

    public void OnInteraction()
    {
        Debug.Log("Interaction()");

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