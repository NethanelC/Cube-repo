using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Button Sounds", menuName = "SO/Button Sounds", order = 1)]
public class ButtonSounds : ScriptableObject
{
    public List<AudioClip> _buttonSounds = new();
}
