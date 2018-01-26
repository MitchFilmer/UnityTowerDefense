using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Enemy : MonoBehaviour 
{
	public int health;
    public int moneyOnDeath = 20;
	public bool canTarget = true;
	public bool kill = false;
    public bool isFlyer { get; set; }

    private int _currentWaypoint = 0;
    private bool _isSlowed = false;
    private float _slowTime = 4f;
    private float _regularSpeed = 4f;

    public List<WaypointManager.Path> _paths;
    private NavMeshAgent _agent;

    public event Action OnDeath;

    private void Awake()
    {
		_agent = gameObject.GetComponent<NavMeshAgent>();

		if (_agent == null)
			_agent = gameObject.AddComponent<NavMeshAgent>();
	}

    void Update ()
	{
        if (_paths == null || _paths[0].Waypoints == null || _paths[0].Waypoints.Count <= _currentWaypoint)
            return;
       
        // FLYING UNITS
        if (isFlyer)
        {
            // disable nav mesh agent
             _agent.enabled = false;

            Transform destination = _paths[1].Waypoints[_currentWaypoint];

            Vector3 transDir = (destination.position - transform.position);
            transDir.Normalize();
            transDir.y = 0.0f;
            transDir *= 0.1f;
            gameObject.transform.Translate(transDir);

            if (Vector3.Distance(transform.position, destination.position) < 1.75f)
            {
                _currentWaypoint++;
                if (_currentWaypoint >= _paths[1].Waypoints.Count)
                {
                    Player.lives--;
                    ResetUnit(this);
                    return;
                }
            }
        }
        
        // GROUND UNITS
        else
        {
            Transform destination = _paths[0].Waypoints[_currentWaypoint];
            _agent.SetDestination(destination.position);

            if (Vector3.Distance(transform.position, destination.position) < 2.0f)
            {
                _currentWaypoint++;
		    	if (_currentWaypoint >= _paths[0].Waypoints.Count)
		    	{
                    Player.lives--;
		    		ResetUnit(this);
                    return;
		    	}
            }
        }
    }

    public void Initialize(List<WaypointManager.Path> paths, Action onUnitDeath)
    {
        _paths = paths;
        OnDeath = onUnitDeath;
    }

	public void TakeDamage(int damage)
	{
		health -= damage;
		if (health <= 0)
			Kill();
	}

    public void SlowEnemy(float slowRate)
    {
        if (!_isSlowed)
        {
            _regularSpeed = _agent.speed;
            _isSlowed = true;
            _agent.speed *= slowRate;
            StartCoroutine("SlowTimer");
        }
    }

    public IEnumerator SlowTimer()
    {
        if (_isSlowed)
        {
            yield return new WaitForSeconds(_slowTime);
            _agent.speed = _regularSpeed;
            _isSlowed = false;
        }
    }

	private void Kill()
    {
        Debug.Log("Killing " + gameObject.name);
        _agent.speed = 0.0f;
		if (OnDeath != null)
            OnDeath();
        
		canTarget = false;

		ResetUnit(this);
        Player.money += moneyOnDeath;
    }

	private void ResetUnit(Enemy enemy)
	{
		gameObject.SetActive(false);

        Debug.Log("Unit Killed... Units Active: " + UnitSpawner.unitsAlive);
        UnitSpawner.unitsAlive --;

        _agent.enabled = true;
		transform.rotation = Quaternion.identity;
		SGF.ServiceLocator.Get<ObjectPoolManager>().RecycleObject(gameObject);
		_currentWaypoint = 0;
		kill = false;
		canTarget = true;
		_agent.speed = _regularSpeed;
        _agent.baseOffset = 1;
        isFlyer = false;
    }
}
