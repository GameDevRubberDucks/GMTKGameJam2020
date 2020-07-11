using UnityEngine;
using TMPro;

public class Player_RoomDetector : MonoBehaviour
{
    //--- Public Variables ---//
    public TextMeshProUGUI m_txtInteractPrompt;
    public string m_promptBeforeInteractKey = "Press '";
    public string m_promptAfterInteractKey = "' To ";



    //--- Unity Methods ---//
    private void OnTriggerEnter2D(Collider2D collision)
    {
        // When entering a room, set it to be occupied and show its interact prompt
        var roomComp = collision.gameObject.GetComponentInParent<Room>();

        if (roomComp != null)
        {
            roomComp.IsOccupied = true;

            if (roomComp.m_interactPrompt == "")
                m_txtInteractPrompt.text = "";
            else
            {
                string promptText = m_promptBeforeInteractKey;
                promptText += roomComp.m_interactKey.ToString();
                promptText += m_promptAfterInteractKey;
                promptText += roomComp.m_interactPrompt;

                m_txtInteractPrompt.text = promptText;
            }
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        // When leaving a room, set it to be empty and no longer show an interact prompt
        var roomComp = collision.gameObject.GetComponentInParent<Room>();

        if (roomComp != null)
        {
            roomComp.IsOccupied = false;
            m_txtInteractPrompt.text = "";
        }
    }
}
