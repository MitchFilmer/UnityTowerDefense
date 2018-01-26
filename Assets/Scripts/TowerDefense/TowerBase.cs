using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TowerBase : MonoBehaviour
{
    protected float fireCountDown = 0f;

	public float attackRange;
	public float attackSpeed;
	public int attackDamage;
	public Renderer render;

	public int currentLevel = 1;
	public int maxUpgrades = 2;

    protected virtual void Update()
    {
        fireCountDown -= Time.deltaTime;
    }

	public virtual bool Upgrade()
	{
		if (currentLevel <= maxUpgrades && Player.money >= 100)
		{
			attackRange *= 1.3f;
			attackDamage = (int)(attackDamage * 1.5f);
			currentLevel++;
			Player.money -= 100;
			return true;
		}
		else
		{
			Debug.Log("Cannot upgrade futher.");
			return false;
		}
	}
}
