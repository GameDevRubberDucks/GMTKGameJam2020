using UnityEngine;
using UnityEngine.Events;

public class Room : MonoBehaviour
{
    //--- Public Variables ---//
    [Header("Scene References")]
    public GameObject[] m_doors;
    public SpriteRenderer m_occupiedIndicator;
    public SpriteRenderer m_specialSymbolObj;

    [Header("Generic Controls")]
    public KeyCode m_interactKey;

    [Header("This Room's Controls")]
    public Sprite m_roomSymbol;
    public string m_interactPrompt;
    public UnityEvent m_onInteract;
    public UnityEvent m_onInteractEnd;



    //--- Private Variables ---//
    private bool m_isOccupied;



    //--- Unity Methods ---//
    private void OnValidate()
    {
        // Assign the special symbol to the scene object if it is assigned in the inspector
        m_specialSymbolObj.sprite = m_roomSymbol;
        m_specialSymbolObj.gameObject.SetActive(m_roomSymbol != null);
    }

    private void Awake()
    {
        // Init the private variables
        m_isOccupied = false;
    }

    private void Update()
    {
        // When occupied, the player can interact with the room
        if (m_isOccupied)
        {
            // There are different interaction states to handle different key states
            if (Input.GetKey(m_interactKey))
                m_onInteract.Invoke();
            else if (Input.GetKeyUp(m_interactKey))
                m_onInteractEnd.Invoke();
        }
    }



    //--- Methods ---//
    public void ToggleDoor(Room_AttachPoint _doorID, bool _isOpen)
    {
        int doorIdx = (int)_doorID;
        m_doors[doorIdx].SetActive(!_isOpen);
    }

    public void RemoveTriggerCollider()
    {

    }



    //--- Setters and Getters ---//
    public bool IsOccupied
    {
        get => m_isOccupied;
        set
        {
            m_isOccupied = value;
            m_occupiedIndicator.enabled = m_isOccupied;
        }
    }

    public string InteractPromptStr
    {
        get => m_interactPrompt; 
        set => m_interactPrompt = value;
    }
}
