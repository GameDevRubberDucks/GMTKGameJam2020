using UnityEngine;

public class Camera_Shake : MonoBehaviour
{
    //--- Private Variables ---//
    private float m_shakeTimeLeft;
    private float m_shakeStrength;



    //--- Unity Methods ---//
    private void Awake()
    {
        // Init the private variables
        m_shakeTimeLeft = 0.0f;
        m_shakeStrength = 0.0f;
    }

    private void Update()
    {
        if (m_shakeTimeLeft > 0.0f)
        {
            // Move the camera randomly within a range
            Vector3 currentCameraPos3D = this.transform.position;
            Vector2 cameraPos2D = new Vector2(currentCameraPos3D.x, currentCameraPos3D.y);
            cameraPos2D += Random.insideUnitCircle * m_shakeStrength;
            this.transform.position = new Vector3(cameraPos2D.x, cameraPos2D.y, currentCameraPos3D.z);

            // Lower the shake time
            m_shakeTimeLeft -= Time.deltaTime;
        }
    }



    //--- Methods ---//
    public void Shake(float _strength, float _duration)
    {
        m_shakeStrength = _strength;
        m_shakeTimeLeft = _duration;
    }
}
