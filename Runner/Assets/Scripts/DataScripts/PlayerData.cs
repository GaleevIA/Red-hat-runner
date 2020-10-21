using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "NewPlayerData", menuName = "MyAssets/PlayerData")]
public class PlayerData : ScriptableObject
{
    public AudioClip deathClip;
    public AudioClip coinPickUpClip;
    public AudioClip jumpClip;
}
