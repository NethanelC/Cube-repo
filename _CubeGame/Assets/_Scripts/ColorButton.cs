using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ColorButton : MonoBehaviour
{
    [SerializeField] private Button _button;
    public static event Action _pressed;

    public void NewColorButton(Color color, int index)
    {
        _button.onClick.AddListener(() => { PlayerPrefs.SetInt("SkinColor", index); _pressed?.Invoke(); });
        _button.image.color = color;
    }
}
