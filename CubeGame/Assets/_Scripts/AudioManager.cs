using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class AudioManager : MonoBehaviour
{
    public static AudioManager Instance { get; private set; }
    [SerializeField] private AudioSource _audioSource;
    #region Audioclips
    [SerializeField] private AudioClip _buttonHover, _buttonClicked, _death;
    #endregion
    public enum Sound
    {
        ButtonHover,
        ButtonClick,
        Death
    }
    private void Awake()
    {
        Instance = Instance != null ? Instance : this;
    }
    public void PlaySound(Sound sound)
    {
        _audioSource.PlayOneShot(GetAudioClip(sound));
    }
    private AudioClip GetAudioClip(Sound sound) => sound switch
    {
        Sound.ButtonHover => _buttonHover,
        Sound.ButtonClick => _buttonClicked,
        Sound.Death => _death,
        _ => throw new ArgumentOutOfRangeException(nameof(sound), $"Not expected sound: {sound}"),
    };
}
