using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using DG.Tweening;

public class ButtonSoundEvents : MonoBehaviour, IPointerClickHandler, IPointerEnterHandler, IPointerExitHandler
{
    public void OnPointerEnter(PointerEventData eventData)
    {
        AudioManager.Instance.PlaySound(AudioManager.Sound.ButtonHover);
        transform.DOScale(1.2f, 0.5f);
    }
    public void OnPointerClick(PointerEventData eventData)
    {
        AudioManager.Instance.PlaySound(AudioManager.Sound.ButtonClick);
        transform.DOScale(1f, 0.5f);
    }
    public void OnPointerExit(PointerEventData eventData)
    {
        transform.DOScale(1f, 0.5f);
    }
}
