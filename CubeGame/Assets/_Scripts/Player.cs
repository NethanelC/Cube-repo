using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;
public class Player : MonoBehaviour
{
    [SerializeField] private GameObject _panelEsc;
    [SerializeField] private Skins _skins;
    [SerializeField] private Camera _cam;
    [SerializeField] private Animator _anim;
    [SerializeField] private SpriteRenderer _spriteRenderer;
    [SerializeField] private Rigidbody2D _rb;
    [SerializeField] private TrailRenderer _tr;
    [SerializeField] private ParticleSystem _psDeath, _psWalk;
    [SerializeField] private Image _cooldownImage;
    [SerializeField] private AudioManager _audioManager;
    [SerializeField] private AudioSource _audioSource;
    [SerializeField] private Collider2D _collider;
    public static event Action Respawned; 
    private float _dashCooldown = 5;
    private float _jumpTimer;
    private bool _flop, _flappy, _dashing, _jumping;
    private const float _gravityScale = 11, _xVelocity = 9.5f;
    //Set the player's current skin and color
    private void Awake()
    {
        _spriteRenderer.sprite = _skins._sprites[PlayerPrefs.GetInt("SkinSprite")];
        Color color = _skins._colors[PlayerPrefs.GetInt("SkinColor")];
        ParticleSystem.MainModule death = _psDeath.main;
        ParticleSystem.MainModule walk = _psWalk.main;
        death.startColor = color;
        walk.startColor = color; 
        _tr.startColor = color;
        _spriteRenderer.color = color; 
    }
    //Init velocity
    private void Start()
    {
        _rb.velocity = new(_xVelocity, 0);
    }
    //Check for inputs and execute the according functions
    private void Update()
    {
        if (Input.GetKey(KeyCode.Space) && !_jumping || Input.GetKeyDown(KeyCode.Space) && _flappy)
        {
            Jump();
        }
        if (Input.GetKeyDown(KeyCode.LeftShift) && !_dashing)
        {
            Dash();
        }
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            GamePausing(); 
        }
    }   
    //Subscribing to events
    private void OnEnable()
    {
        Obstacle.Obstacled += Death;
        Finish.FinishedGame += StopMovement;
        Jumpy.Jumpied += Jumpied;
        Speedy.Speedied += Speedied;
        Flappy.Flappied += Flappied;
        Flip.Flipped += Flipped;
        Flop.Flopped += Flopped;
    }
    //Unsubscribing from events
    private void OnDisable()
    {
        Obstacle.Obstacled -= Death;
        Finish.FinishedGame -= StopMovement;
        Jumpy.Jumpied -= Jumpied;
        Speedy.Speedied += Speedied;
        Flappy.Flappied -= Flappied;
        Flip.Flipped -= Flipped;
        Flop.Flopped -= Flopped;
    }
    private void OnCollisionEnter2D(Collision2D coll)
    {
        if (coll.gameObject.layer == 3)
        {
            _jumping = false;
            _rb.velocity = new(_xVelocity, _rb.velocity.y);
            _psWalk.gameObject.SetActive(true);
        }
    }
    private void Speedied()
    {
        _rb.AddForce(Vector2.right * _xVelocity,ForceMode2D.Impulse);
    }
    private void Death()
    {
        StopMovement();
        _dashing = true;
        _jumping = true;
        DOTween.KillAll(this);
        _cooldownImage.fillAmount = 0;
        _psDeath.Play();
        AudioSource.PlayClipAtPoint(_audioManager.ClipList[0], transform.position);
        _cam.DOShakePosition(1, 0.2f, 10, 90, true, ShakeRandomnessMode.Harmonic);
        _anim.SetTrigger("Death");
    }
    public void Respawn()
    {
        Respawned?.Invoke();
        _jumping = false;
        _flop = false;
        _flappy = false;
        _dashing = false;
        _cam.transform.rotation = Quaternion.identity;
        _psWalk.Play();
        _anim.Play("PlayerIdle", 0);
        _anim.Play("PlayerIdle", 1);
        transform.position = Checkpoint.CheckpointPosition;
        _rb.gravityScale = _gravityScale;
        _rb.constraints = RigidbodyConstraints2D.FreezeRotation;
        _collider.enabled = true;
        _rb.velocity = new(_xVelocity, 0);
    }   
    private void Dash()
    {
        _dashing = true;
        _rb.AddForce(Vector2.right * _xVelocity, ForceMode2D.Impulse);
        _cooldownImage.fillAmount = 1;
        _cooldownImage.DOFillAmount(0, _dashCooldown).SetEase(Ease.Linear).OnComplete(() =>
        {
            _dashing = false;
        });
        _tr.emitting = true;
        _tr.DOTime(0.2f, 0.2f).OnComplete(() =>
        {
            _tr.emitting = false;
        });
    }
    private void Jump()
    {
        _jumping = true;
        if (!_flop)
        {
            _rb.velocity = new(_xVelocity, 0);
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
    private void Jumpied()
    {
        _jumping = true;
        _rb.velocity = new(_xVelocity, 0);
        _rb.AddForce(Vector2.up * 42, ForceMode2D.Impulse);
        _anim.SetTrigger("Jump");
    }
    private void Flappied()
    {
        _flappy = !_flappy;
    }
    private void Flipped()
    {
        _cam.transform.rotation = Quaternion.Euler(0, 0, _cam.transform.rotation.z == 0 ? 180 : 0);
    }
    private void Flopped()
    {
        _rb.gravityScale = _flop ? _gravityScale : -_gravityScale;
        _flop = !_flop;
    }
    public void GamePausing()
    {
        Time.timeScale = Time.timeScale == 1 ? 0 : 1;
        _panelEsc.SetActive(!_panelEsc.activeSelf);
    }
    private void StopMovement()
    {
        _rb.velocity = Vector2.zero;
        _rb.constraints = RigidbodyConstraints2D.FreezeAll;
        _collider.enabled = false;
        _psWalk.Stop();
    }
}