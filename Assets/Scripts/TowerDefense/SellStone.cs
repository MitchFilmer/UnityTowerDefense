using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class SellStone : MonoBehaviour
{
	public GameObject ui;
	public Transform pos;

	private void Awake()
	{
		if (ui == null)
		{
			Debug.Log("UI object is null");
		}
	}

	private void OnMouseDown()
	{
		if (EventSystem.current.IsPointerOverGameObject())
			return;

		if (!ui.activeInHierarchy)
		{
			ui.transform.position = pos.position;
			ui.SetActive(true);
			StartCoroutine("CloseTimer");
		}
		else
			ui.SetActive(false);

		return;
	}

	public void Remove()
	{
		ui.SetActive(false);
		Destroy(gameObject);
	}

	IEnumerator CloseTimer()
	{
		yield return new WaitForSeconds(1.5f);
		ui.SetActive(false);
	}
}
