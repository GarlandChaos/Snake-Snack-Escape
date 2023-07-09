using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    [Header("Audio Source")]
    [SerializeField] private AudioSource audioSource;
    [SerializeField] private AudioSource sfxAudioSource;
    public AudioSource walkingAudioSource;

    [Header("Music")]
    [SerializeField] private AudioClip menuMusic;
    [SerializeField] private AudioClip stageMusic;

    [Header("SFX")]
    [SerializeField] private AudioClip defeatSFX;
    [SerializeField] private AudioClip victorySFX;
    [SerializeField] private AudioClip doorOpenSFX;
    [SerializeField] private AudioClip keySFX;
    [SerializeField] private AudioClip walkingSFX;

    private bool isMuted = false;

    #region Singleton
    public static AudioManager Instance { get; private set; }

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }
    #endregion

    private void Update()
    {

        if(Input.GetKeyDown(KeyCode.Z))
        {
            PlayDefeatSFX();
        }

        if (Input.GetKeyDown(KeyCode.X))
        {
            PlayVictorySFX();
        }

        if (Input.GetKeyDown(KeyCode.C))
        {
            PlayDoorOpenSFX();
        }

        if (Input.GetKeyDown(KeyCode.V))
        {
            PlayKeySFX();
        }

    }

    public void ToggleMute()
    {
        if (isMuted)
        {
            audioSource.mute = false;
            isMuted = false;
        }
        else
        {
            audioSource.mute = true;
            isMuted = true;
        }
    }

    public void PlayWalkingSFX()
    {
        walkingAudioSource.Play();
    }

    public void StopWalkingSFX()
    {
        walkingAudioSource.Stop();
    }

    public void PlayMenuMusic()
    {
        audioSource.clip = menuMusic;
        audioSource.Play();
    }

    public void PlayStageMusic()
    {
        audioSource.clip = stageMusic;
        audioSource.Play();
    }

    public void StopMusic()
    {
        audioSource.Stop();
    }

    public void PlayDefeatSFX()
    {
        sfxAudioSource.PlayOneShot(defeatSFX);
    }

    public void PlayVictorySFX()
    {
        sfxAudioSource.PlayOneShot(victorySFX);
    }

    public void PlayDoorOpenSFX()
    {
        sfxAudioSource.PlayOneShot(doorOpenSFX);
    }

    public void PlayKeySFX()
    {
        sfxAudioSource.PlayOneShot(keySFX);
    }

}