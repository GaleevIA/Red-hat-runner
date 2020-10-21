using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using UnityEngine.Events;
using UnityEngine.EventSystems;

public class PlatformsManager: MonoBehaviour
{
    #region Fields
    public static PlatformsManager instance;
    
    [SerializeField]
    private GameObject[] _platformsPref;
    [SerializeField]
    private GameObject[] _envrironmentPref;
    [SerializeField]
    private GameObject _player;
    [SerializeField]
    private GameObject _startPlatform;
    [SerializeField]
    private int _activePlatformsCount;
    [SerializeField]
    private int _platformsPoolLength; 
    [SerializeField]
    private float _moveSpeed;
    [SerializeField]
    private GameObject _obstaclePrefab;
    [SerializeField]
    private GameObject _coinPrefab;
    
    private Vector3 currentPos;
    private float _step = 7;
    private GameObject[] _platformsPool;
    private PlayerController _playerController;
    #endregion

    #region MonoBehaviourEvents
    private void Awake()
    {
        if (instance != null && instance != this)
            Destroy(gameObject);
        else
            instance = this;
    }

    private void Start()
    {
        _platformsPool = new GameObject[_platformsPoolLength];
        currentPos = new Vector3(0,0,_step);

        _playerController = _player.GetComponent<PlayerController>();
        _playerController.OnDeath += OnPlayerDeath;

        _platformsPool[0] = _startPlatform;
        _platformsPool[0].SetActive(true);

        for (int i = 1; i < _platformsPoolLength; i++)
        {
            int index = Random.Range(0, _platformsPref.Length);

            GameObject newPlatform = Instantiate(_platformsPref[index], currentPos, Quaternion.identity);
            newPlatform.SetActive(false);

            _platformsPool[i] = newPlatform;            

            GenerateEnvironment(newPlatform);
            GenerateObstacles(newPlatform);           
        }
     
        for (int i = 1; i <= _activePlatformsCount; i++)
        {
            ActivateNextPlatform();
        }
    }

    private void Update()
    {   
        currentPos -= _player.transform.forward * Time.deltaTime * _moveSpeed;

        MoveActivePlatforms();
    }
    #endregion

    #region Service
    private void MoveActivePlatforms()
    {
        GameObject[] activePlatforms = _platformsPool.Where(e => e.activeInHierarchy).ToArray();

        for (int i = 0; i < activePlatforms.Length; i++)
        {
            activePlatforms[i].transform.position -= _player.transform.forward * Time.deltaTime * _moveSpeed;

            if (activePlatforms[i].transform.position.z < -10)
            {
                activePlatforms[i].SetActive(false);

                ActivateNextPlatform();
            }
        }
    }

    private void GenerateEnvironment(GameObject platform)
    {
        for (int i = 1; i < 30; i++)
        {
            int index = Random.Range(0, _envrironmentPref.Length);

            AddEnvironment(_envrironmentPref[index], platform.transform, 2.5f, 1.8f, -3f, 3f);           
        }
        
        for (int i = 1; i < 30; i++)
        {
            int index = Random.Range(0, _envrironmentPref.Length);

            AddEnvironment(_envrironmentPref[index], platform.transform, -1.8f, - 2.5f, -3f, 3f);
        }
    }

    private void AddEnvironment(GameObject environmentPref, Transform parrentTransorm, float xMin, float xMax, float zMin, float zMax)
    {
        float x = Random.Range(xMin, xMax);
        float z = Random.Range(parrentTransorm.position.z + zMin, parrentTransorm.position.z + zMax);

        GameObject forestObject = Instantiate(environmentPref, new Vector3(x, 0.5f, z), Quaternion.identity, parrentTransorm);       
    }

    private void ActivateNextPlatform()
    {
        GameObject[] inactivePlatforms = _platformsPool.Where(e => !e.activeInHierarchy).ToArray();

        int index = Random.Range(0, inactivePlatforms.Length);
      
        inactivePlatforms[index].transform.position = currentPos;
        inactivePlatforms[index].SetActive(true);

        currentPos = new Vector3(currentPos.x, currentPos.y, currentPos.z + _step);

        GenerateCoins(inactivePlatforms[index]);
    }

    private void GenerateObstacles(GameObject platform)
    {
        PlatformData platformData = platform.GetComponent<PlatformController>().platformData;
        
        for (int i = 0; i < platformData.obstaclesMaxCount; i++)
        {
            int index = Random.Range(0, platformData.obstacleSpawnPoints.Count());

            GameObject obstacle = Instantiate(_obstaclePrefab, platform.transform.localPosition + platformData.obstacleSpawnPoints[index], Quaternion.identity, platform.transform);
        }
    }

    private void GenerateCoins(GameObject platform)
    {
        PlatformData platformData = platform.GetComponent<PlatformController>().platformData;

        for (int i = 0; i < platformData.coinsMaxCount; i++)
        {
            int index = Random.Range(0, platformData.coinSpawnPoints.Count());

            GameObject coin = Instantiate(_coinPrefab, platform.transform.localPosition + platformData.coinSpawnPoints[index], Quaternion.identity, platform.transform);
        }
    }

    private void OnPlayerDeath()
    {
        _moveSpeed = 0;
    }
    #endregion
}
