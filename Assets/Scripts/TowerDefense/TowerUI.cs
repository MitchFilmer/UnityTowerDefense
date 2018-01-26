using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TowerUI : MonoBehaviour
{
	public GameObject ui;

	private Builder target;

	public void SetTarget(Builder _target)
	{
		target = _target;
		transform.position = target.transform.position + new Vector3(0, 10, 0);

		ui.SetActive(true);
	}
	public void Hide()
	{
		ui.SetActive(false);
	}

	public void SellTower()
	{
		Player.money += 50;
		Destroy(target.tower);
		Hide();
	}

	public void UpgradeTower()
	{
		target.Upgrade();
	}
}
