using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using SGF;
using Debug = SGF.Debug;

public class ObjectPoolManager : MonoBehaviour, IGameModule
{
	[Serializable]
	public class PoolObject
	{
		public string name;
		public GameObject prefab;
		public int poolSize;
	}
	public List<PoolObject> objectsToPool = new List<PoolObject>();

	public bool IsInitialized { get { return _isInitialized; } }
	private bool _isInitialized;

	private Dictionary<string, List<GameObject>> _objectPoolByName = new Dictionary<string, List<GameObject>>();

	// IGameModule function
	public IEnumerator LoadModule()
	{
		Debug.Log("Loading Object Pool");
		InitializePool();
		yield return new WaitUntil(() => { return IsInitialized; });

		ServiceLocator.Register<ObjectPoolManager>(this);
		yield return null;
	}

	public GameObject GetObjectFromPool(string poolName)
	{
		GameObject returnObj = null;
		if(_objectPoolByName.ContainsKey(poolName))
		{
			returnObj = GetNextObject(poolName);
		}
		else
		{
			Debug.Log("No Pool Exists with Name:" + poolName);
		}
		return returnObj;
	}

	private GameObject GetNextObject(string poolName)
	{
		List<GameObject> poolObjects = _objectPoolByName[poolName];
		foreach(GameObject go in poolObjects)
		{
			if(go.activeInHierarchy == false)
			{
				return go;
			}
		}
		Debug.Log("Object pool depeleted: Construct additional pylons!");
		return null;
	}

	private void InitializePool()
	{
		GameObject PoolManagerGO = new GameObject("Object Pool");
		PoolManagerGO.transform.SetParent(GameObject.FindWithTag("Services").transform);
		foreach(PoolObject poolObj in objectsToPool)
		{
			if(!_objectPoolByName.ContainsKey(poolObj.name))
			{
				Debug.Log(string.Format("Create Pool: {0} Size: {1}", poolObj.name, poolObj.poolSize));
				GameObject poolGO = new GameObject(poolObj.name);
				poolGO.transform.SetParent(PoolManagerGO.transform);
				_objectPoolByName.Add(poolObj.name, new List<GameObject>());
				for (int i = 0; i < poolObj.poolSize; ++i)
				{
					GameObject go = Instantiate(poolObj.prefab);
					go.name = string.Format("{0}_{1:000}", poolObj.name, _objectPoolByName[poolObj.name].Count);
					go.transform.SetParent(poolGO.transform);
					go.SetActive(false);
					_objectPoolByName[poolObj.name].Add(go);
				}
			}
			else
			{
				Debug.Log("WARNING: Attempting to create multiple pool of the same name: " + poolObj.name);
			}
		}

		_isInitialized = true;
	}

    public void RecycleObject(GameObject go)
    {
        go.SetActive(false);
    }
}
