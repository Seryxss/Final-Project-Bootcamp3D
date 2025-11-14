using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum UISound
{
    OpeningMusic,
    ButtonClick,
}
public class UISoundManager : MonoBehaviour
{
    [SerializeField] private AudioSource OpeningaudioSource;
    [SerializeField] private AudioSource UIaudioSource;
    [SerializeField] private AudioClip[] soundList;
    private static UISoundManager instance;
    private void Awake()
    {
        instance = this;
    }

    private void Start()
    {
        OpeningaudioSource.clip = soundList[(int)SoundType.BackgroudMusic];
        OpeningaudioSource.Play();
    }

    public static void PlaySound(SoundType sound, float volume = .5f)
    {
        instance.UIaudioSource.PlayOneShot(instance.soundList[(int)sound], volume);
    }

    public void stopBackgroundMusic()
    {
        OpeningaudioSource.Stop();
    }

}
