using UnityEngine;
using UnityEngine.Events;

public class Room : MonoBehaviour
{
    //--- Public Variables ---//
    [Header("Scene References")]
    public GameObject[] m_doors;
    public SpriteRenderer m_occupiedIndicator;
    public SpriteRenderer m_specialSymbolObj;
    public Collider2D m_outerTriggerCollider;
    public GameObject m_roomBlocker;

    [Header("Generic Controls")]
    public KeyCode m_interactKey;

    [Header("This Room's Controls")]
    public Sprite m_roomSymbol;
    public string m_interactPrompt;
    public UnityEvent m_onInteractStart;
    public UnityEvent m_onInteract;
    public UnityEvent m_onInteractEnd;
    public UnityEvent m_onNoInteract;



    //--- Private Variables ---//
    private bool m_isOccupied;
    private bool m_isAttached;



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
        m_isAttached = false;
    }

    private void Update()
    {
        // When occupied, the player can interact with the room
        if (m_isOccupied)
        {
            // There are different interaction states to handle different key states
            if (Input.GetKeyDown(m_interactKey))
                m_onInteractStart.Invoke();
            else if (Input.GetKey(m_interactKey))
                m_onInteract.Invoke();
            else if (Input.GetKeyUp(m_interactKey))
                m_onInteractEnd.Invoke();
            else
                m_onNoInteract.Invoke();
        }
    }



    //--- Methods ---//
    public void ToggleDoor(Room_AttachPoint _doorID, bool _isOpen)
    {
        int doorIdx = (int)_doorID;
        m_doors[doorIdx].SetActive(!_isOpen);
    }

    public void OnAttachedToShip()
    {
        // This is the collider that goes around the room
        // Once this room is attached, the collider merges with the ship
        // To do this, it can no longer have a rigidbody
        Destroy(this.GetComponent<Rigidbody2D>());
        m_outerTriggerCollider.usedByComposite = true;

        // Hide the blocker since it is no longer in space
        m_roomBlocker.SetActive(false);

        // The room is now attached
        m_isAttached = true;
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

    public bool IsAttached
    {
        get => m_isAttached;
        set => m_isAttached = value;
    }
}
