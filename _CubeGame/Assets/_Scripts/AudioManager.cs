using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class AudioManager : MonoBehaviour
{
    public static AudioManager instance;
    public List<AudioClip> ClipList;
    [SerializeField] private AudioSource _audioSource;
    private void Awake()
    {
        instance = instance != null ? instance : this;
    }
    public void PlaySound(AudioClip clip)
    {
        _audioSource.PlayOneShot(clip);
    }
}
