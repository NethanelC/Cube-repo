using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Zenject;

public class PauseMenu : MonoBehaviour
{
    [Inject] private readonly PlayerInputs _playerInputs;
    [SerializeField] private GameObject _pausePanel;
    private void OnEnable() => _playerInputs.Player.Pause.performed += OnPause;
    private void OnDisable() => _playerInputs.Player.Pause.performed -= OnPause;
    private void OnPause(UnityEngine.InputSystem.InputAction.CallbackContext obj) => TogglePause();
    private void TogglePause()
    {
        Time.timeScale = Time.timeScale == 1 ? 0 : 1;
        _pausePanel.SetActive(!_pausePanel.activeSelf);
    }
}
