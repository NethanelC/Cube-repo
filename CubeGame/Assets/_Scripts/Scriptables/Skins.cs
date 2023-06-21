using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Skins", menuName = "SO/Skins", order = 1)]
public class Skins : ScriptableObject
{
    [SerializeField] private Skin[] _skins = new Skin[5];
    [SerializeField] private Color[] _colors = new Color[5];
    public int SkinsAmount => _skins.Length;
    public int ColorsAmount => _colors.Length;
    public Skin GetSkin(int index) => _skins[index];
    public Color GetColor(int index) => _colors[index];
}
[System.Serializable]
public class Skin
{
    [field: SerializeField] public Sprite Sprite { get; private set; }
    [field: SerializeField] public int ReqStars { get; private set; }
}
