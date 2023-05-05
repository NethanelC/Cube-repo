using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using DG.Tweening;

public class ButtonSoundEvents : MonoBehaviour, IPointerClickHandler, IPointerEnterHandler, IPointerExitHandler
{
    [SerializeField] private ButtonSounds _sounds;

    public void OnPointerEnter(PointerEventData eventData)
    {
        AudioManager.instance.PlaySound(_sounds._buttonSounds[0]);
        transform.DOScale(1.2f, 0.5f);
    }
    public void OnPointerClick(PointerEventData eventData)
    {
        AudioManager.instance.PlaySound(_sounds._buttonSounds[1]);
        transform.DOScale(1f, 0.5f);
    }
    public void OnPointerExit(PointerEventData eventData)
    {
        transform.DOScale(1f, 0.5f);
    }
}
