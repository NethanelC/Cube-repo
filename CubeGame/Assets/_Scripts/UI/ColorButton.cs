using System;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class ColorButton : MonoBehaviour, IPointerClickHandler, IUpdateSelectedHandler
{
    public static event Action Pressed;
    [SerializeField] private Image _buttonImage;
    private int _index;
    public void Init(Color color, int index)
    {
        _buttonImage.color = color;
        _index = index;
    }
    public void OnPointerClick(PointerEventData eventData) => UpdateSelectedColor();
    public void OnUpdateSelected(BaseEventData eventData) => UpdateSelectedColor();
    private void UpdateSelectedColor()
    {
        PlayerPrefs.SetInt("SkinColor", _index);
        Pressed?.Invoke();
    }
}
