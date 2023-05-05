using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Skins", menuName = "SO/Skins", order = 1)]
public class Skins : ScriptableObject
{
    public List<Sprite> _sprites = new();
    public List<int> _stars = new();
    public List<Color> _colors = new();
}
