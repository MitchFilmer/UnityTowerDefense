using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using UnityEngine.AI;

public class Builder : MonoBehaviour
{
    public Color hoverColour;
    public GameObject tower;
    public GameObject tempTower;

	private TowerBase _towerBase;
    private Renderer rend;
    private Color initialColour;
    private JsonDataSource dataSource;

	TowerManager _towerManager;
   
    Dictionary<string, object> fireTowerStats;
    Dictionary<string, object> iceTowerStats;
    Dictionary<string, object> lightningTowerStats;
    Dictionary<string, object> windTowerStats;

    void Start()
    {
		if (_towerManager == null)
			_towerManager = TowerManager.instance;

        if (dataSource == null)

            dataSource = TowerManager.instance.GetJsonSource();

		if(dataSource == null)
			Debug.Log("No [DATA SOURCE] in builder script.");
        
		
        rend = GetComponent<Renderer>();
        initialColour = rend.material.color;
    }

	public void Upgrade()
	{
		_towerBase.Upgrade();
	}

	void OnMouseEnter()
    {
		if (EventSystem.current.IsPointerOverGameObject())
			return;
		if (_towerManager.GetTurretToBuild() == null)
			return;

		rend.material.color = hoverColour;
    }

    void OnMouseExit()
    {
        rend.material.color = initialColour;
    }

    void OnMouseDown()
    {
        // Return if clicking on another object
		if (EventSystem.current.IsPointerOverGameObject())
			return;

        // Select a tower for upgrade/sell
		if (tower != null && !_towerManager.PlacingTower)
        {
			_towerManager.SelectSpot(this);
            return;
        }

        // Keep selected tower if clicking on an existing tower
		if (_towerManager.GetTurretToBuild() != null && tower != null)
		{
			_towerManager.SetTurretToBuild(_towerManager.GetTurretToBuild());
			return;
		}
		
        // Return if nothing to build
		if (_towerManager.GetTurretToBuild() == null)
			return;

        // Builds a tower in selected tile
        GameObject towerToMake = _towerManager.GetTurretToBuild();
        tower = Instantiate(towerToMake, transform.position, Quaternion.identity);
        tower.transform.Translate(new Vector3(0.0f, 1.25f, 0.0f));

        // Saves a reference to different tower type dictionaries
        fireTowerStats = dataSource.DataDictionary["Fire"] as Dictionary<string, object>;
        iceTowerStats = dataSource.DataDictionary["Ice"] as Dictionary<string, object>;
        lightningTowerStats = dataSource.DataDictionary["Lightning"] as Dictionary<string, object>;
        windTowerStats = dataSource.DataDictionary["Wind"] as Dictionary<string, object>;

        // Checks to see which tower is made then pulls stats from JSON file
        FireStats fireTower = tower.GetComponent<FireStats>();
        if (fireTower != null)
        {
            fireTower.attackRange = System.Convert.ToSingle(fireTowerStats["attackRange"]);
            fireTower.attackDamage = System.Convert.ToInt32(fireTowerStats["attackDamage"]);
            fireTower.attackSpeed = System.Convert.ToSingle(fireTowerStats["attackSpeed"]);
            fireTower.splashArea = System.Convert.ToSingle(fireTowerStats["splashArea"]);
			_towerBase = fireTower;
        }
        IceStats iceTower = tower.GetComponent<IceStats>();
        if (iceTower != null)
        {
            iceTower.attackRange = System.Convert.ToSingle(iceTowerStats["attackRange"]);
            iceTower.attackDamage = System.Convert.ToInt32(iceTowerStats["attackDamage"]);
            iceTower.attackSpeed = System.Convert.ToSingle(iceTowerStats["attackSpeed"]);
            iceTower.slowRate = System.Convert.ToSingle(iceTowerStats["slowRate"]);
			_towerBase = iceTower;
		}
        WindStats windTower = tower.GetComponent<WindStats>();
        if (windTower != null)
        {
            windTower.attackRange = System.Convert.ToSingle(windTowerStats["attackRange"]);
            windTower.attackDamage = System.Convert.ToInt32(windTowerStats["attackDamage"]);
            windTower.attackSpeed = System.Convert.ToSingle(windTowerStats["attackSpeed"]);
			_towerBase = windTower;
        }
        LightningStats lightningTower = tower.GetComponent<LightningStats>();
        if (lightningTower != null)
        {
            lightningTower.attackRange = System.Convert.ToSingle(lightningTowerStats["attackRange"]);
            lightningTower.attackDamage = System.Convert.ToInt32(lightningTowerStats["attackDamage"]);
            lightningTower.attackSpeed = System.Convert.ToSingle(lightningTowerStats["attackSpeed"]);
			_towerBase = lightningTower;
        }

        // After placing tower, resets variables
        _towerManager.PlacingTower = false;
		_towerManager.SetTurretToBuild(null);
    }
}