using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Levels", menuName = "SO/Levels", order = 1)]
public class Levels : ScriptableObject
{
    public List<Sprite> _levelImage;
}
