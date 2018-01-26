using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaypointManager : MonoBehaviour
{
    private SpawnManager _spawnManager;

    public void Start()
    {
        _spawnManager = GetComponent<SpawnManager>();
    }

    [Serializable]
    public class Path
    {
        public int Id;
        public List<Transform> Waypoints;
    }
    public List<Path> Paths;

    public Path GetPath(int id)
    {
        return Paths[id];
    }

    public void SetAirWaypoints()
    {
        for(int i = 0; i < Paths[0].Waypoints.Count; i++)
        {
        Paths[0].Waypoints[i].localPosition = new Vector3(0.0f, 6.25f, 0.0f);
        }
     
    }

    public void SetGroundWaypoints()
    {
        for (int i = 0; i < Paths[0].Waypoints.Count; i++)
        {
            Paths[0].Waypoints[i].localPosition = new Vector3(0.0f, 2.0f, 0.0f);
        }

    }

}
