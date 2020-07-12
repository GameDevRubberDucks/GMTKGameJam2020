using UnityEngine;
using UnityEngine.SceneManagement;

public class Room_Drive : MonoBehaviour, IRoom_Interactable
{
    //--- Public Variables ---//
    public string m_endScreenName;



    //--- IRoom_Interactable Interface ---//
    public void OnInteractionStart()
    {
        FindObjectOfType<Audio_Manager>().PlaySFX(Audio_SFX.Warp_Drive);
        SceneManager.LoadScene(m_endScreenName);
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