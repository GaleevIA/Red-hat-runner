using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InterfaceManager : MonoBehaviour
{
    #region Fields
    [SerializeField]
    private GameObject _player;
    [SerializeField]
    private Text _txtScore;

    private PlayerController _playerController;
    #endregion

    #region MonoBehaviourEvents
    void Start()
    {
        _playerController = _player.GetComponent<PlayerController>();
        _playerController.OnCoinPickUp += OnCoinPickUp;
    }
    #endregion

    #region Service
    private void OnCoinPickUp()
    {
        _txtScore.text = _playerController.GetCoinsCount().ToString();
    }
    #endregion
}
