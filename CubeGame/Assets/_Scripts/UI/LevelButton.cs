using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class LevelButton : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    [SerializeField] private LevelsManager _levelsManager;
    [SerializeField] private Button _button, _normalButton, _hardButton;
    [SerializeField] private Image _levelImage;
    public void Init(int index, Level levelDetails)
    {
        _levelImage.sprite = levelDetails.Sprite;
        _normalButton.onClick.AddListener(() => _levelsManager.LoadLevel(index + 1, Level.Difficulty.Normal));
        _hardButton.onClick.AddListener(() => _levelsManager.LoadLevel(index + 1, Level.Difficulty.Hard));
    }
    public void OnPointerEnter(PointerEventData eventData)
    {
        _normalButton.gameObject.SetActive(true);
        _hardButton.gameObject.SetActive(true);
    }
    public void OnPointerExit(PointerEventData eventData)
    {
        _normalButton.gameObject.SetActive(false);
        _hardButton.gameObject.SetActive(false);
    }
}
