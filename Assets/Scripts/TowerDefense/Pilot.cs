using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Pilot : MonoBehaviour {
    
    public NavMeshPath path;
    public GameObject start;
    public GameObject end;

    void Start()
    {
        path = new NavMeshPath();
    }

   public bool CalculatePath()
    {
        NavMesh.CalculatePath(start.transform.position, end.transform.position, NavMesh.AllAreas, path);
        
        if (path.status != NavMeshPathStatus.PathComplete)
            return false;
        else
            return true;
    }

}
