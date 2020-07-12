using UnityEngine;
using UnityEngine.SceneManagement;

public class Scene_Loader : MonoBehaviour
{
    //--- Public Variables ---/
    public string m_sceneNameToLoad;
    public KeyCode m_triggerKey;



    //--- Unity Methods ---//
    void Update()
    {
        if (Input.GetKeyDown(m_triggerKey))
            SceneManager.LoadScene(m_sceneNameToLoad);
    }
}
