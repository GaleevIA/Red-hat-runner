using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.Events;

[RequireComponent(typeof(Rigidbody))]
[RequireComponent(typeof(Animator))]
[RequireComponent(typeof(AudioSource))]
public class PlayerController : MonoBehaviour
{
    #region Fields
    private Rigidbody _rigidBody;
    private Animator _animator;
    private AudioSource _audioSource;
    [SerializeField]
    private PlayerData _playerData;
    [SerializeField]
    private float _jumpForce;
    private bool _isJumping;
    
    private PlayerModel _playerModel;
    private PlayerView _playerView;
    #endregion

    #region Events
    public delegate void DeathAction();
    public event DeathAction OnDeath;
    public delegate void CoinPickUpAction();
    public event CoinPickUpAction OnCoinPickUp;
    public delegate void JumpAction();
    public event JumpAction OnJump;
    #endregion

    #region MonoBehavior
    private void Awake()
    {
        _animator = GetComponent<Animator>();
        _audioSource = GetComponent<AudioSource>();
        _rigidBody = GetComponent<Rigidbody>();

        _playerView = new PlayerView(_animator, _audioSource, _playerData);
        _playerModel = new PlayerModel();                    
    }

    private void Start()
    {
        OnDeath += _playerView.OnDeath;
        OnCoinPickUp += _playerModel.OnCoinPickUp;
        OnCoinPickUp += _playerView.OnCoinPickUp;
        OnJump += _playerView.OnJump;
    }

    private void OnDestroy()
    {
        OnDeath -= _playerView.OnDeath;
        OnCoinPickUp -= _playerModel.OnCoinPickUp;
        OnCoinPickUp -= _playerView.OnCoinPickUp;
        OnJump -= _playerView.OnJump;
    }

    void Update()
    {
        if(transform.position.y < -0)
        {
            OnDeath();
        }

        bool btnLeftPressed = Input.GetKeyDown(KeyCode.LeftArrow);
        bool btnRightPressed = Input.GetKeyDown(KeyCode.RightArrow);
        bool btnJumpPressed = Input.GetKeyDown(KeyCode.Space);

        if(btnLeftPressed && transform.position.x > -1)
        {
            transform.position += new Vector3(-1, 0, 0);
        }
        else if(btnRightPressed && transform.position.x < 1)
        {
            transform.position += new Vector3(1, 0, 0);
        }
        if (btnJumpPressed && !_isJumping)
        {
            _isJumping = true;

            _rigidBody.AddForce(transform.up * _jumpForce);
            OnJump();
        }
    }   

    private void OnCollisionEnter(Collision collision)
    {
        if(collision.gameObject.tag == "Obstacle")
        {
            OnDeath();
        } 
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Coin")
        {
            OnCoinPickUp();
            Destroy(other.gameObject);
        }   
    }
    #endregion

    #region Service
    public void OnJumpEnd()
    {
        _isJumping = false;
    }
    
    public int GetCoinsCount()
    {
        return _playerModel.coinsCount;
    }
    #endregion
}
