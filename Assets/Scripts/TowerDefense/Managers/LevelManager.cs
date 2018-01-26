using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using SGF;
using Debug = SGF.Debug;

public class LevelManager : MonoBehaviour
{
    public TowerManager towerManager;
    public WaypointManager waypointManager;
    public SpawnManager spawnManager;
    public UiManager uiManager;
    public AudioManager audioManger;
    public Store store;
    public Player player;

    public event Action EnemyUnitKilled;

    private JsonDataSource _dataSource;
    private int _enemyUnitKilledCounter = 0;

    private void Awake()
    {
        GameLoader.CallOnComplete(Init);
    }

    public void Init()
    {
        EnemyUnitKilled += OnEnemyUnitKilled;
        DataLoader dl = ServiceLocator.Get<DataLoader>();
        IDataSource source = dl.GetDataSourceByName("EnemyAttributes");
        _dataSource = source as JsonDataSource;
        if (_dataSource != null)
        {
            Debug.Log("DataSource Loaded " + _dataSource.ID);

            foreach (KeyValuePair<string, object> kvp in _dataSource.DataDictionary)
            {
                Debug.Log(string.Format("<color=cyan>Key: {0}  Value: {1}</color>", kvp.Key, kvp.Value.ToString()));
            }
        }
        if(uiManager != null)
        {
            uiManager.Initialize(spawnManager, store, player);
        }
        else
        {
            Debug.Log("UiManager is NULL");
        }

        if (towerManager != null)
        {
            towerManager.SetupTowers();
        }
        else
        {
            Debug.Log("TowerManager is NULL");
        }

        if (spawnManager != null)
        {
            spawnManager.Initialize(waypointManager, EnemyUnitKilled);
        }
        else
        {
            Debug.Log("SpawnManager is NULL");
        }
    }

    private void OnEnemyUnitKilled()
    {
        _enemyUnitKilledCounter++;
    }
}
