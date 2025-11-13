using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum SoundType
{
    BackgroudMusic,
    GameOver,
    ReachingDestination,
    HitObstacle
}
public class SoundManager : MonoBehaviour
{
    [SerializeField] private AudioSource MusicaudioSource;
    [SerializeField] private AudioSource SFXaudioSource;
    [SerializeField] private AudioClip[] soundList;
    private static SoundManager instance;
    private void Awake()
    {
        instance = this;
    }

    private void Start()
    {
        MusicaudioSource.clip = soundList[(int)SoundType.BackgroudMusic];
        MusicaudioSource.Play();
    }

    public static void PlaySound(SoundType sound, float volume = .5f)
    {
        instance.SFXaudioSource.PlayOneShot(instance.soundList[(int)sound], volume);
    }

    public void stopBackgroundMusic()
    {
        MusicaudioSource.Stop();
    }
}
