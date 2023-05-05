using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using DG.Tweening;

public class SkinsShop : MonoBehaviour
{
    [SerializeField] private Skins _skins;
    [SerializeField] private SkinButton _skinButton;
    [SerializeField] private ColorButton _colorButton;
    [SerializeField] private RectTransform _skinsTransform, _colorsTransform, _tabsTransform;
    [SerializeField] private TextMeshProUGUI _starsText, _skinNameText;
    [SerializeField] private SpriteRenderer _rotatingSkinSprite;
    [SerializeField] private Image _shopSkinImage, _shopTabsImage;
    [SerializeField] private Button _buttonPrev, _buttonNext;
    private int _currentTab;
    private Image[] _tabsImages;
    private void Awake()
    {
        SetNewSprite();
        SetNewColor();
        _rotatingSkinSprite.gameObject.transform.DOScale(Vector3.one * 2, 3).SetEase(Ease.OutCubic);
        _rotatingSkinSprite.gameObject.transform.DORotate(Vector3.forward * 180, 20).SetEase(Ease.Linear).SetLoops(-1);
        for (int i = 0; i < _skins._sprites.Count; i++)
        {
            Instantiate(_skinButton, _skinsTransform).Init(_skins._stars[i], _skins._sprites[i], i);
        }
        for (int i = 0; i < _skins._colors.Count; i++)
        {
            Instantiate(_colorButton, _colorsTransform).NewColorButton(_skins._colors[i], i);
        }
        _starsText.text = $"{PlayerPrefs.GetInt("TotalStars")}";
        int maximumTabs = Mathf.CeilToInt(_skins._sprites.Count * 0.1f);
        _tabsImages = new Image[maximumTabs];
        for (int i = 0; i < maximumTabs; i++)
        {
            _tabsImages[i] = Instantiate(_shopTabsImage, _tabsTransform);
        }
        SlideMenu(maximumTabs);
        _buttonNext.onClick.AddListener(() =>
        {
            _tabsImages[_currentTab].color = new Color(0.129f, 0.145f, 0.160f, 1);
            _currentTab += 1 % maximumTabs;
            SlideMenu(maximumTabs);
        });
        _buttonPrev.onClick.AddListener(() =>
        {
            _tabsImages[_currentTab].color = new Color(0.129f, 0.145f, 0.160f, 1);
            _currentTab -= 1 % maximumTabs;
            SlideMenu(maximumTabs);
        });
    }
    private void OnEnable()
    {
        SkinButton.Clicked += SetNewSprite;
        ColorButton._pressed += SetNewColor;
    }
    private void OnDisable()
    {
        SkinButton.Clicked -= SetNewSprite;
        ColorButton._pressed -= SetNewColor;
    }
    private void SetNewColor()
    {
        Color color = _skins._colors[PlayerPrefs.GetInt("SkinColor", 0)];
        _rotatingSkinSprite.color = color;
        _shopSkinImage.color = color;
    }
    private void SetNewSprite()
    {
        Sprite sprite = _skins._sprites[PlayerPrefs.GetInt("SkinSprite", 0)];
        _rotatingSkinSprite.sprite = sprite;
        _shopSkinImage.sprite = sprite;
        _skinNameText.text = sprite.name;
    }
    private void SlideMenu(int max)
    {
        _skinsTransform.DOAnchorPosX(-750 * (_currentTab), 0.5f).SetEase(Ease.Linear);
        _tabsImages[_currentTab].color = Color.white;
        _buttonPrev.interactable = _currentTab != 0;
        _buttonNext.interactable = _currentTab != max - 1;
    }
}
