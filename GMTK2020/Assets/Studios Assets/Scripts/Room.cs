using UnityEngine;
using UnityEngine.Events;

public enum Room_DoorID
{
    Top,
    Right,
    Bottom,
    Left,

    Num_Doors
}

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
    public UnityEvent m_OnStartInteract;
    public UnityEvent m_OnContinueInteract;
    public UnityEvent m_OnEndInteract;



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
            if (Input.GetKeyDown(m_interactKey))
                m_OnStartInteract.Invoke();
            else if (Input.GetKey(m_interactKey))
                m_OnContinueInteract.Invoke();
            else if (Input.GetKeyUp(m_interactKey))
                m_OnEndInteract.Invoke();
        }
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

    private void OnTriggerEnter2D(Collider2D other)
    {
        Debug.Log("OnTriggerEnter2D - " + other.gameObject.name);

        if (other.tag == "Player")
        {
            m_occupiedIndicator.enabled = true;
            m_isOccupied = true;
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        Debug.Log("OnTriggerEnter2D - " + other.gameObject.name);

        if (other.tag == "Player")
        {
            m_occupiedIndicator.enabled = false;
            m_isOccupied = false;
        }
    }
}
