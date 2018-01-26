using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.UI;

public class SpawnManager : MonoBehaviour
{
    public List<UnitSpawner> Spawners;
    private WaypointManager _waypointManager;
    private Action _enemyUnitKilled;

    public bool isRunning { get; set; }
    public int numberOfWaves;
    private int _currentWave = 0;

    public Pilot testPilot;
    public NavMeshPath path;
    public GameObject start;
    public GameObject end;
    public Text pathBlockedText;

    public static SpawnManager instance;

    public int GetCurrWave() { return _currentWave; }

    public void Start()
    {
        path = new NavMeshPath();
    }

    public void Update()
    {
        if(TestPath())
            pathBlockedText.text = "";

        isRunning = (UnitSpawner.unitsAlive == 0) ? false : true;
        
    }

    public void Initialize(WaypointManager wpMgr, Action onEnemyUnitKilled)
    {
        isRunning = false;
        _enemyUnitKilled = onEnemyUnitKilled;
        _waypointManager = wpMgr;
        foreach (UnitSpawner spawner in Spawners)
        {
            List<WaypointManager.Path> paths = new List<WaypointManager.Path>();
            paths.Add(_waypointManager.GetPath(spawner.pathId));
            paths.Add(_waypointManager.GetPath(spawner.pathId + 1));
            spawner.Init(paths, _enemyUnitKilled);
        }
    }

    // This is called when "Spawn button is pressed"
    public void StartWave()
    {
        if (isRunning)
            return;

        if (!TestPath())
        {
            pathBlockedText.text = "Enemy path blocked. Must leave one path.";
            return;
        }

        if (_currentWave < numberOfWaves)
        {
            UnitSpawner.unitsAlive = 0;

            isRunning = true;
            _currentWave++;

            foreach (UnitSpawner spawner in Spawners)
            {
                spawner.StartSpawner();
            }

        }
    }

    public bool TestPath()
    {
        NavMesh.CalculatePath(start.transform.position, end.transform.position, NavMesh.AllAreas, path);

        if (path.status != NavMeshPathStatus.PathComplete)
            return false;

        else
            return true;
    }
}


