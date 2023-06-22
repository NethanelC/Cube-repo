using UnityEngine;
using UnityEngine.UI;   
using Zenject;

public class PauseMenu : MonoBehaviour
{
    private Player _player;
    private PlayerInputs _playerInputs;
    [SerializeField] private GameObject _pausePanel;
    [SerializeField] private Button _retryButton;
    [Inject]
    private void Construct(PlayerInputs playerInputs, Player player)
    {
        _playerInputs = playerInputs;
        _player = player;
    }
    private void Awake() => _retryButton.onClick.AddListener(() => _player.Respawn());
    private void OnEnable() => _playerInputs.Player.Pause.performed += OnPause;
    private void OnDisable() => _playerInputs.Player.Pause.performed -= OnPause;
    private void OnPause(UnityEngine.InputSystem.InputAction.CallbackContext obj) => TogglePause();
    public void TogglePause()
    {
        Time.timeScale = Time.timeScale == 1 ? 0 : 1;
        _pausePanel.SetActive(!_pausePanel.activeSelf);
    }
}
