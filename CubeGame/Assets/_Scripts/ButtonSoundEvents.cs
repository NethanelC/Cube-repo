using UnityEngine;
using UnityEngine.EventSystems;
using DG.Tweening;

public class ButtonSoundEvents : MonoBehaviour, IPointerClickHandler, IPointerEnterHandler, IPointerExitHandler
{
    public void OnPointerEnter(PointerEventData eventData)
    {
        AudioManager.Instance.PlaySound(AudioManager.Sound.ButtonHover);
        ScaleUp();
    }
    public void OnPointerClick(PointerEventData eventData)
    {
        AudioManager.Instance.PlaySound(AudioManager.Sound.ButtonClick);
        ScaleDown();
    }
    public void OnPointerExit(PointerEventData eventData) => ScaleDown();
    private void ScaleDown() => transform.DOScale(1f, 0.5f);
    private void ScaleUp() => transform.DOScale(1.2f, 0.5f);
}
