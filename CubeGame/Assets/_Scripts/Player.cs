using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;
using Zenject;

public class Player : MonoBehaviour
{
    public static event Action Death; 
    [Inject] readonly private PlayerInputs _playerInputs;
    [SerializeField] private Camera _cam;
    [SerializeField] private Animator _anim;
    [SerializeField] private Rigidbody2D _rb;
    [SerializeField] private Image _cooldownImage;
    [SerializeField] private AudioSource _audioSource;
    [SerializeField] private Collider2D _collider;
    private readonly Dictionary<LayerMask, Action> _collisionMappings = new();
    private const float GRAVITY_SCALE = 11, X_VELOCITY = 9.5f, DASH_COOLDOWN = 5;
    private bool _flop, _flappy, _dashing, _jumping;
    private float _jumpTimer;
    #region Customization
    [Header("Customization")]
    [SerializeField] private Skins _skins;
    [SerializeField] private SpriteRenderer _spriteRenderer;
    [SerializeField] private TrailRenderer _trail;
    [SerializeField] private ParticleSystem _psDeath, _psWalk;
    #endregion
    private void Awake()
    {
        #region Collision Mappings
        _collisionMappings[3] = OnTouchGround;
        _collisionMappings[7] = OnTouchObstacle;
        _collisionMappings[8] = StopMovement;
        #endregion
        #region Customization Init
        _spriteRenderer.sprite = _skins.GetSkin(PlayerPrefs.GetInt("SkinSprite")).Sprite;
        Color color = _skins.GetColor(PlayerPrefs.GetInt("SkinColor"));
        ParticleSystem.MainModule death = _psDeath.main;
        ParticleSystem.MainModule walk = _psWalk.main;
        death.startColor = color;
        walk.startColor = color;
        _trail.startColor = color;
        _spriteRenderer.color = color;
        #endregion
        _rb.velocity = new(X_VELOCITY, 0);
    }
    //Events subscription
    private void OnEnable()
    {
        _playerInputs.Player.Dash.performed += OnDash;
        Jumpy.Jumpied += Jumpied;
        Speedy.Speedied += Speedied;
    }
    //Events unsubscription
    private void OnDisable()
    {
        _playerInputs.Player.Dash.performed -= OnDash;
        Jumpy.Jumpied -= Jumpied;
        Speedy.Speedied -= Speedied;
    }
    private void Update()
    {
        if (Input.GetKey(KeyCode.Space) && !_jumping || Input.GetKeyDown(KeyCode.Space) && _flappy)
        {
            Jump();
        }
    }   
    private void OnCollisionEnter2D(Collision2D coll)
    {
        if (_collisionMappings.TryGetValue(coll.gameObject.layer, out Action action))
        {
            action?.Invoke();
        }
    }
    private void OnDash(UnityEngine.InputSystem.InputAction.CallbackContext obj) => TryDash();
    public void Jumpied()
    {
        _jumping = true;
        _rb.velocity = new(X_VELOCITY, 0);
        _rb.AddForce(Vector2.up * 42, ForceMode2D.Impulse);
        _anim.SetTrigger("Jump");
    }
    public void Speedied() => _rb.AddForce(Vector2.right * X_VELOCITY, ForceMode2D.Impulse);
    public void Flappied() => _flappy = !_flappy;
    public void Flipped() => _cam.transform.rotation = Quaternion.Euler(0, 0, _cam.transform.rotation.z == 0 ? 180 : 0);
    public void Flopped()
    {
        _rb.gravityScale = _flop ? GRAVITY_SCALE : -GRAVITY_SCALE;
        _flop = !_flop;
    }
    private void OnTouchGround()
    {
        _jumping = false;
        _rb.velocity = new(X_VELOCITY, _rb.velocity.y);
        _psWalk.gameObject.SetActive(true);
    }
    private void OnTouchObstacle()
    {
        StopMovement();
        _dashing = true;
        _jumping = true;
        DOTween.KillAll(this);
        _cooldownImage.fillAmount = 0;
        _psDeath.Play();
        AudioManager.Instance.PlaySound(AudioManager.Sound.Death);
        _cam.DOShakePosition(1, 0.2f, 10, 90, true, ShakeRandomnessMode.Harmonic);
        _anim.SetTrigger("Death");
        Death?.Invoke();
    }
    public void Respawn()
    {
        _jumping = false;
        _flop = false;
        _flappy = false;
        _dashing = false;
        _cam.transform.rotation = Quaternion.identity;
        _psWalk.Play();
        _anim.Play("PlayerIdle", 0);
        _anim.Play("PlayerIdle", 1);
        transform.position = Checkpoint.CheckpointPosition;
        _rb.gravityScale = GRAVITY_SCALE;
        _rb.constraints = RigidbodyConstraints2D.FreezeRotation;
        _collider.enabled = true;
        _rb.velocity = new(X_VELOCITY, 0);
    }   
    private void TryDash()
    {
        if (_dashing)
        {
            return;
        }
        _dashing = true;
        _trail.emitting = true;
        _rb.AddForce(Vector2.right * X_VELOCITY, ForceMode2D.Impulse);
        _cooldownImage.fillAmount = 1;
        _cooldownImage.DOFillAmount(0, DASH_COOLDOWN).SetEase(Ease.Linear).OnComplete(() => _dashing = false);
        _trail.DOTime(0.2f, 0.2f).OnComplete(() => _trail.emitting = false);
    }
    private void Jump()
    {
        _jumping = true;
        if (!_flop)
        {
            _rb.velocity = new(X_VELOCITY, 0);
            _rb.AddForce(Vector2.up * 26, ForceMode2D.Impulse);
        }
        else
        {
            _rb.gravityScale *= -1;
        }
        _psWalk.Stop();
        _anim.SetTrigger("Jump");
        _audioSource.Play();
        _audioSource.pitch = Time.time - _jumpTimer <= 1 ? _audioSource.pitch + 0.1f : 1;
        _jumpTimer = Time.time;
    }
    private void StopMovement()
    {
        _rb.velocity = Vector2.zero;
        _rb.constraints = RigidbodyConstraints2D.FreezeAll;
        _collider.enabled = false;
        _psWalk.Stop();
    }
}