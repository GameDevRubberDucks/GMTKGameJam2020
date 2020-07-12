using UnityEngine;
using System.Collections;

public enum Audio_SFX
{
    Engine,
    Gun_Shoot,
    Explosion,
    Warp_Drive,
    Crash
}

public class Audio_Manager : MonoBehaviour
{
    public AudioSource m_srcMusic;
    public AudioSource m_srcSFX;
    public AudioClip[] m_audioClips;

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

    public void PlaySFX(Audio_SFX _clipName)
    {
        Debug.Log("Playing sound [" + _clipName.ToString() + "]");

        var sfxClip = m_audioClips[(int)_clipName];
        m_srcSFX.PlayOneShot(sfxClip);
    }
}
