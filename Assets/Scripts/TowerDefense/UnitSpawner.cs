using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using SGF;
using Debug = SGF.Debug;

public class UnitSpawner : MonoBehaviour
{
    public GameObject UnitPrefab;
    
    public int unitsPerWave;
    public int flyingRound;
    public static int unitsAlive = 0;
    private int _currentUnit = 0;
    
    public float secondsBetweenUnits;
    public int pathId;

	private JsonDataSource enemySource;
    private List<WaypointManager.Path> _paths;
    private Action _enemyUnitKilled;
    private SpawnManager _spawnManager;

    private bool isSpawning = false;

    public void Update()
    {
        if (unitsAlive < 0)
            unitsAlive = 0;

        if(Input.GetKeyDown(KeyCode.L))
        {
            Debug.Log("UNITS ALIVE: " + unitsAlive);
            print("UNITS ALIVE: " + unitsAlive);
        }
    }


    private void Awake()
    {
        if (UnitPrefab == null)
        {
            Debug.Log("UnitSpawner Disabled: Unit Prefab is NULL");
            gameObject.SetActive(false);
            return;
        }

        unitsAlive = 0;
        _spawnManager = GetComponentInParent<SpawnManager>();
    }

    public void Init(List<WaypointManager.Path> paths, Action onEnemyUnitKilled)
    {
        _paths = paths;
        _enemyUnitKilled = onEnemyUnitKilled;

		DataLoader dataLoader = SGF.ServiceLocator.Get<DataLoader>();
		IDataSource source = dataLoader.GetDataSourceByName("EnemyAttributes");
		enemySource = source as JsonDataSource;
		if (enemySource == null)
			Debug.Log("Failed To Load EnemyAttributes Source");
	
	}


    // Called first from Spawn Manager on new wave
    public void StartSpawner()
    {
        if (IsFlyingRound())
            pathId = 1;
        else
            pathId = 0;

        if (!isSpawning)
        {
            isSpawning = true;
            StartCoroutine("BeginWaveSpawn");
        }
    }

    private IEnumerator BeginWaveSpawn()
    {
        while (_currentUnit < unitsPerWave)
        {
            Debug.Log("SPAWNING...");
            SpawnWave();
			_currentUnit++;
            yield return new WaitForSeconds(secondsBetweenUnits);
        }
        Debug.Log("Spawn mode: OFF");
        isSpawning = false;
        _currentUnit = 0;
        _spawnManager.isRunning = false;
    }

    private void SpawnWave()
    {        
		ObjectPoolManager opm = ServiceLocator.Get<ObjectPoolManager>();
		GameObject enemy  = opm.GetObjectFromPool("Enemy");
		if(enemy == null)
		{
			return;
		}


		enemy.gameObject.GetComponent<NavMeshAgent>().enabled = false;

		Dictionary<string, object> enemyStats = enemySource.DataDictionary as Dictionary<string, object>;
        enemy.GetComponent<Enemy>().moneyOnDeath = System.Convert.ToInt32(enemyStats["Money"]);
        int hp = System.Convert.ToInt32(enemyStats["Health"]);
        enemy.GetComponent<Enemy>().health = (hp * GetComponentInParent<SpawnManager>().GetCurrWave());

        enemy.SetActive(true);
		enemy.transform.position = transform.position;
		enemy.transform.rotation = Quaternion.identity;
		enemy.gameObject.GetComponent<NavMeshAgent>().enabled = true;
		enemy.GetComponent<Enemy>().Initialize(_paths, _enemyUnitKilled);


        // Check if flying wave
        if (IsFlyingRound())
        {
            enemy.GetComponent<Enemy>().isFlyer = true;         
            enemy.gameObject.GetComponent<NavMeshAgent>().baseOffset = 4;
        }
        else
        {
            enemy.GetComponent<Enemy>().isFlyer = false;
            enemy.gameObject.GetComponent<NavMeshAgent>().baseOffset = 1;
        }

        unitsAlive++;
	}

    private bool IsFlyingRound()
    {
        if (_spawnManager.GetCurrWave() % flyingRound == 0)
            return true;
        else
            return false;
    }

}