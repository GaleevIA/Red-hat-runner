using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerModel
{
    #region Fields
    private int _coinsCount;
    #endregion

    #region Properties
    public int coinsCount
    {
        get
        {
            return _coinsCount;
        }
        set
        {
            _coinsCount = value;
        }
    }
    #endregion

    #region Constructors
    public PlayerModel()
    {
        _coinsCount = 0;
    }
    #endregion

    #region Service
    public void OnCoinPickUp()
    {
        _coinsCount++;
    }
    #endregion
}
