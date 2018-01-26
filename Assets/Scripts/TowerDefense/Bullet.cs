using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using SGF;
using Debug = SGF.Debug;

public class Bullet : MonoBehaviour
{
    public float speed = 40f;
	private int _damage = 0;

	public bool splashes = false;
	public float explosionRadius;

    public bool canSlow = false;
	public float slowRate;

	public GameObject impactEffect;

    private Transform target;
	private Enemy _enemy;
	GameObject effect;

	public void Seek(Transform _target, int damage)
    {
        target = _target;
		_damage = damage;
		_enemy = _target.GetComponent<Enemy>();
    }
	
	void Update ()
    {
		if(target == null)
        {
            Destroy(gameObject);
            return;
        }

        Vector3 direction = target.position - transform.position;
        float distanceThisFrame = speed * Time.deltaTime;

        if(direction.magnitude <= distanceThisFrame)
        {
            HitTarget(_damage);
            return;
        }

        transform.Translate(direction.normalized * distanceThisFrame, Space.World);

	}

	public void SetSplash(float aoe)
	{
		splashes = true;
		explosionRadius = aoe;
	}

	public void SetSlow(float rate)
	{
		canSlow = true;
		slowRate = (1 - (rate / 100f));
	}

    void HitTarget(int damage)
    {
		_enemy.TakeDamage(damage);
		Destroy(gameObject);

        if (canSlow)
            _enemy.SlowEnemy(slowRate);        

        if (impactEffect != null && splashes)
			Explode(damage);

    }
	public void ExplodeOnSelf(float aoe, int dmg)
	{
		splashes = true;
		explosionRadius = aoe;
		Explode(dmg);
	}

	private void Explode(int damage)
	{
		GameObject effect = (GameObject)Instantiate(impactEffect, transform.position, transform.rotation);
		Collider[] colliders = Physics.OverlapSphere(transform.position, explosionRadius);
		foreach (Collider collider in colliders)
		{
			if (collider.tag == "Enemy")
			{
				Enemy enemyInExplosion = collider.GetComponent<Enemy>();
				enemyInExplosion.TakeDamage(damage);
			}
		}
	}

	public void DestoryEnemy(GameObject enemy)
	{
		enemy.SetActive(false);
	}
}
