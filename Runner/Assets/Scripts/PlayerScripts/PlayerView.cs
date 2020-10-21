using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerView
{
    #region Fields
    private Animator _animator;
    private AudioSource _audioSource;
    private PlayerData _playerData;
    #endregion

    #region Constructors
    public PlayerView(Animator animator, AudioSource audioSource, PlayerData playerData)
    {
        _animator = animator;
        _audioSource = audioSource;
        _playerData = playerData;
    }
    #endregion

    #region Service
    public void OnDeath()
    {
        _animator.SetTrigger("isDead");
        _audioSource.PlayOneShot(_playerData.deathClip);
    }

    public void OnJump()
    {
        _animator.SetTrigger("isJump");
        _audioSource.PlayOneShot(_playerData.jumpClip);
    }

    public void OnCoinPickUp()
    {        
        _audioSource.PlayOneShot(_playerData.coinPickUpClip);
    }
    #endregion
}
