using UnityEngine;

[CreateAssetMenu(fileName ="NewPlatformData", menuName = "MyAssets/PlatformData")]
public class PlatformData : ScriptableObject
{
    public Vector3[] obstacleSpawnPoints;
    public Vector3[] coinSpawnPoints;
    public int obstaclesMaxCount;
    public int coinsMaxCount;
}
