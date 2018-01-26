using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IceStats : TowerBase
{
    public float slowRate;

	public Transform target;
	public GameObject bulletPrefab;
	public Transform firePoint;


	List<GameObject> listOfEnemies = new List<GameObject>();

	public  void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.cyan;
        Gizmos.DrawWireSphere(transform.position, attackRange);
    }

	void Start()
	{
		// SphereCollider is doubled the size of range for some reason...
		GetComponent<SphereCollider>().radius = (GetComponent<IceStats>().attackRange) / 2;
		render = GetComponent<Renderer>();
	}

    void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Enemy")
        {
            Debug.Log("Enemy entered");
            GameObject availableEnemy = other.gameObject;
            availableEnemy.GetComponent<Enemy>().OnDeath += RemoveEnemyFromList;
            listOfEnemies.Add(availableEnemy);
        }
    }

	public override bool Upgrade()
	{
		if (base.Upgrade())
		{
			render.material.color -= new Color(0.3f, 0f, 0f);
			GetComponentInParent<SphereCollider>().radius = attackRange / 2;
			return true;
		}
		return false;
	}

	private void RemoveEnemyFromList()
    {
        List<GameObject> enemiesToRemove = new List<GameObject>();
        foreach (var enemy in listOfEnemies)
        {
            if (!enemy.activeInHierarchy)
            {
                enemiesToRemove.Add(enemy);
            }
        }

        foreach (var inactiveEnemy in enemiesToRemove)
        {
            Debug.Log(string.Format("Removing {0} from Slow Tower", inactiveEnemy.name));
            listOfEnemies.Remove(inactiveEnemy);
        }
    }

    void OnTriggerExit(Collider other)
    {
        if (other.tag == "Enemy")
        {
            Debug.Log("Enemy left");
            GameObject availableEnemy = other.gameObject;
            listOfEnemies.Remove(availableEnemy);

            if (listOfEnemies.Count <= 0)
            {
                target = null;
            }
        }
    }

    void UpdateTarget()
    {
        float shortestDistance = Mathf.Infinity;
        GameObject nearestEnemy = null;

        if (listOfEnemies != null)
        {
            foreach (GameObject enemy in listOfEnemies)
            {
                if (!enemy.activeInHierarchy)
                {
                    continue;
                }

                float distanceToEnemy = Vector3.Distance(transform.position, enemy.transform.position);

                if (distanceToEnemy < shortestDistance)
                {
                    shortestDistance = distanceToEnemy;
                    nearestEnemy = enemy;
                }
            }

            if (nearestEnemy != null && shortestDistance <= attackRange && nearestEnemy.GetComponent<Enemy>().canTarget)
            {
                target = nearestEnemy.transform;
            }
        }
    }

    protected override void Update()
    {
        base.Update();
        if (listOfEnemies.Count <= 0)
            return;

        UpdateTarget();

        if (base.fireCountDown <= 0f)
        {
            Shoot();
            target = null;
            base.fireCountDown = 1f / attackSpeed;
        }
    }

    void Shoot()
	{
		if (target == null)
		{
			Debug.Log("Tower: " + gameObject.name + " Tried to shoot null target");
			return;
		}

		GameObject bulletGO = (GameObject)Instantiate(bulletPrefab, firePoint.position, firePoint.rotation);
		Bullet bullet = bulletGO.GetComponent<Bullet>();
        if (bullet != null)
        {
			bullet.SetSlow(slowRate);
            bullet.Seek(target, attackDamage);
        }
    }

}