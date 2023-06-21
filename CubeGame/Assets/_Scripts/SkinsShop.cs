using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using DG.Tweening;

public class SkinsShop : MonoBehaviour
{
    [SerializeField] private Skins _skins;
    [SerializeField] private SkinButton _skinButtonPrefab;
    [SerializeField] private ColorButton _colorButtonPrefab;
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
        _starsText.text = PlayerPrefs.GetInt("TotalStars").ToString();
        _rotatingSkinSprite.gameObject.transform.DOScale(Vector3.one * 2, 3).SetEase(Ease.OutCubic);
        _rotatingSkinSprite.gameObject.transform.DORotate(Vector3.forward * 180, 20).SetEase(Ease.Linear).SetLoops(-1);
        for (int i = 0; i < _skins.SkinsAmount; i++)
        {
            Instantiate(_skinButtonPrefab, _skinsTransform).Init(_skins.GetSkin(i), i);
        }
        for (int i = 0; i < _skins.ColorsAmount; i++)
        {
            Instantiate(_colorButtonPrefab, _colorsTransform).Init(_skins.GetColor(i), i);
        }
        int maximumTabs = Mathf.CeilToInt(_skins.SkinsAmount * 0.1f);
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
        ColorButton.Pressed += SetNewColor;
    }
    private void OnDisable()
    {
        SkinButton.Clicked -= SetNewSprite;
        ColorButton.Pressed -= SetNewColor;
    }
    private void SetNewColor()
    {
        Color color = _skins.GetColor(PlayerPrefs.GetInt("SkinColor", 0));
        _rotatingSkinSprite.color = color;
        _shopSkinImage.color = color;
    }
    private void SetNewSprite()
    {
        Sprite sprite = _skins.GetSkin(PlayerPrefs.GetInt("SkinSprite", 0)).Sprite;
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
