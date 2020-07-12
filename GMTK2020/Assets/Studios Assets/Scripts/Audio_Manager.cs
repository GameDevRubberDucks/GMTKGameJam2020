using UnityEngine;

public enum Audio_SFX
{
    Engine,
    Gun_Shoot,
    Explosion,
    Warp_Drive,
    Crash,
    New_Room
}

public class Audio_Manager : MonoBehaviour
{
    public AudioSource m_srcMusic;
    public AudioSource[] m_srcSFX;

    private static Audio_Manager m_instance;

    private void Awake()
    {
        // Keep this persistent
        if (m_instance != null && m_instance != this)
        {
            Destroy(this.gameObject);
        }
        else
        {
            m_instance = this;
            DontDestroyOnLoad(this.gameObject);
        }

        // Play background music
        PlayMusic();
    }

    public void PlayMusic()
    {
        m_srcMusic.Play();
    }

    public void PlaySFX(Audio_SFX _sfxName)
    {
        Debug.Log("Playing sound [" + _sfxName.ToString() + "]");

        m_srcSFX[(int)_sfxName].Play();
    }

    public void StopSFX(Audio_SFX _sfxName)
    {
        Debug.Log("Stopping sound [" + _sfxName.ToString() + "]");

        m_srcSFX[(int)_sfxName].Stop();
    }
}
