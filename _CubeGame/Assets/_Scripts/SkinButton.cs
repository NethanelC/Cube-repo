using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using DG.Tweening;

public class SkinButton : MonoBehaviour
{
    [SerializeField] private Button _button;
    [SerializeField] private Image _skinImage;
    [SerializeField] private TextMeshProUGUI _skinStarsNeeded;
    [SerializeField] private GameObject _neededStars;
    public static event Action Clicked;
    public void Init(int stars, Sprite sprite, int index)
    {
        _button.onClick.AddListener(() => { PlayerPrefs.SetInt("SkinSprite", index); Clicked?.Invoke(); });
        _skinImage.sprite = sprite;
        if (PlayerPrefs.GetInt("TotalStars", 0) < stars)
        {
            _button.interactable = false;
            _skinStarsNeeded.text = $"{stars}";
            _neededStars.SetActive(true);
            return;
        }
        _neededStars.SetActive(false);
    }
}
