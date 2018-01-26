using UnityEngine;
using UnityEngine.UI;
using UnityEngine.AI;

public class Store : MonoBehaviour
{
	TowerManager _towerManager;
    private SpawnManager _spawnManager;


    private void Start()
	{
        if (_towerManager == null)
		    _towerManager = TowerManager.instance;

        _spawnManager = FindObjectOfType<SpawnManager>();

    }

    public void Update()
    {
        if (Input.GetKeyDown(KeyCode.Alpha1))
            BuyTower(1);

        else if (Input.GetKeyDown(KeyCode.Alpha2))
            BuyTower(2);

        else if (Input.GetKeyDown(KeyCode.Alpha3))
            BuyTower(3);

        else if (Input.GetKeyDown(KeyCode.Alpha4))
            BuyTower(4);

        else if (Input.GetKeyDown(KeyCode.Alpha5))
            BuyTower(5);
    }   

    public void BuyTower(int towerNumber)
    {
        // Disables building while game is running
        if (_spawnManager.isRunning)
            return;
        
        switch (towerNumber)
        {
            case 1:
                BuyStone();
                break;
            case 2:
                BuyFire();
                break;
            case 3:
                BuyIce();
                break;
            case 4:
                BuyLightning();
                break;
            case 5:
                BuyWind();
                break;
        }
    }


	private void BuyStone()
	{
    
		if (Player.money >= _towerManager.RockCost && _towerManager.PlacingTower == false)
		{
			_towerManager.PlacingTower = true;
			_towerManager.SetTurretToBuild(_towerManager.RockPrefab);
            Player.money -= _towerManager.RockCost;
		}
		else
		Debug.Log("HAHA, can't even buy a rock!");
	}

    private void BuyFire()
	{
        // Add pilot test before buying, false = message and return
        if (Player.money >= _towerManager.FireCost && _towerManager.PlacingTower == false)
        {
            _towerManager.PlacingTower = true;
            _towerManager.SetTurretToBuild(_towerManager.FirePrefab);
            Player.money -= _towerManager.FireCost;
        }
		else
        Debug.Log("U poor sucka!");
    }

    private void BuyIce()
	{
        // Add pilot test before buying, false = message and return
        if (Player.money >= _towerManager.IceCost && _towerManager.PlacingTower == false)
        {
            _towerManager.PlacingTower = true;
            _towerManager.SetTurretToBuild(_towerManager.IcePrefab);
            Player.money -= _towerManager.IceCost;
        }
		else
			Debug.Log("U poor sucka!");
    }

    private void BuyLightning()
	{
        // Add pilot test before buying, false = message and return
        if (Player.money >= _towerManager.LightningCost && _towerManager.PlacingTower == false)
        {
            _towerManager.PlacingTower = true;
            _towerManager.SetTurretToBuild(_towerManager.LightningPrefab);
            Player.money -= _towerManager.LightningCost;
        }
		else
			Debug.Log("U poor sucka!");
    }

    private void BuyWind()
	{
        // Add pilot test before buying, false = message and return
        if (Player.money >= _towerManager.WindCost && _towerManager.PlacingTower == false)
        {
            _towerManager.PlacingTower = true;
            _towerManager.SetTurretToBuild(_towerManager.WindPrefab);
            Player.money -= _towerManager.WindCost;
        }
		else
			Debug.Log("U poor sucka!");
    }
}
