using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    [SerializeField] private AudioSource audioSource;
    private bool isMuted = false;

    #region Singleton
    public static AudioManager Instance { get; private set; }

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }
    #endregion

    public void ToggleMute()
    {
        if (isMuted)
        {
            audioSource.Play();
            isMuted = false;
        }
        else
        {
            audioSource.Pause();
            isMuted = true;
        }
    }

}