using System;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.EventSystems;

public class SkinButton : MonoBehaviour, IPointerClickHandler, IUpdateSelectedHandler
{
    public static event Action Clicked;
    [SerializeField] private Image _skinImage;
    [SerializeField] private TextMeshProUGUI _skinStarsNeeded;
    [SerializeField] private GameObject _neededStars;
    private bool _isClickable;
    private int _index;
    public void Init(Skin skin, int index)
    {
        _index = index; 
        _skinImage.sprite = skin.Sprite;
        if (PlayerPrefs.GetInt("TotalStars", 0) < skin.ReqStars)
        {
            _isClickable = false;
            _skinStarsNeeded.text = skin.ReqStars.ToString();
            _neededStars.SetActive(true);
            return;
        }
        _neededStars.SetActive(false);
    }
    public void OnPointerClick(PointerEventData eventData) => TryUpdateSelectedSkin();
    public void OnUpdateSelected(BaseEventData eventData) => TryUpdateSelectedSkin();
    private void TryUpdateSelectedSkin()
    {
        if (!_isClickable)
        {
            return;
        }
        PlayerPrefs.SetInt("SkinSprite", _index); 
        Clicked?.Invoke();
    }
}
