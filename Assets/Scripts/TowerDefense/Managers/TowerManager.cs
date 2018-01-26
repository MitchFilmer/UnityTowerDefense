using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TowerManager : MonoBehaviour
{
	[Header("Tower Types")]
    public GameObject FirePrefab;
	public GameObject IcePrefab;
	public GameObject LightningPrefab;
	public GameObject WindPrefab;
	public GameObject RockPrefab;

	public TowerUI towerUI;

	private int fireCost;
	private int iceCost;
	private int lightningCost;
	private int windCost;
	private int rockCost;

	public bool PlacingTower { get; set; }

	private GameObject turretToMake = null;
    JsonDataSource dataSource;

    private List<GameObject> _towers = new List<GameObject>();
	private Builder selectedSpot;

	public static TowerManager instance;
    void Awake()
    {
        if (instance != null)
        {
            Debug.Log("fq oFf, i'M AlReAdy HeRE1");
            return;
        }
		PlacingTower = false;
        instance = this;
    }

    public int FireCost { get { return fireCost; } }
    public int IceCost { get { return iceCost; } }
    public int LightningCost { get { return lightningCost; } }
    public int WindCost { get { return windCost; } }
	public int RockCost { get { return rockCost; } }

	public JsonDataSource GetJsonSource()
    {
        return dataSource;
    }

    public GameObject GetTurretToBuild()
    {
        return turretToMake;
    }

	public void SelectSpot(Builder spot)
	{
		if (selectedSpot == spot)
		{
			DeselectSpot();
			return;
		}

		selectedSpot = spot;
		turretToMake = null;

		towerUI.SetTarget(spot);
	}

	public void DeselectSpot()
	{
		selectedSpot = null;
		towerUI.Hide();
	}

	public void SetTurretToBuild(GameObject tower)
	{
		turretToMake = tower;
		selectedSpot = null;

		towerUI.Hide();
	}

    public void SetupTowers()
    {
        if (FirePrefab == null || IcePrefab == null
			|| WindPrefab == null || LightningPrefab == null
			|| RockPrefab == null)
        {
            Debug.LogError("Can't Make Stuff From Nothing");
            return;
        }
        DataLoader dataLoader = SGF.ServiceLocator.Get<DataLoader>();
        IDataSource source = dataLoader.GetDataSourceByName("TowerAttributes");
		dataSource = source as JsonDataSource;
		if (dataSource == null)
			Debug.LogError("Failed To Load Data Source");

		IDataSource prices = dataLoader.GetDataSourceByName("TowerPrices");
		JsonDataSource priceSource = prices as JsonDataSource;
		if (priceSource == null)
			Debug.LogError("Failed To Load Prices Source");

		Dictionary<string, object> towerCosts = priceSource.DataDictionary as Dictionary<string, object>;

		fireCost = System.Convert.ToInt32(towerCosts["Fire"]);
		iceCost = System.Convert.ToInt32(towerCosts["Ice"]);
		lightningCost = System.Convert.ToInt32(towerCosts["Lightning"]);
		windCost = System.Convert.ToInt32(towerCosts["Wind"]);
		rockCost = System.Convert.ToInt32(towerCosts["Rock"]);

	}
}
