using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

using SGF;
using Debug = SGF.Debug;


public class UiManager : MonoBehaviour
{
	public Text moneyText;
	public Text waveText;
    public Text infoText;
    public Text livesLeft;

	private bool isPaused;

	TowerManager _towerManager;
	private Store _store;
	private SpawnManager _spawnManager;
    private Player _player;
    public Animator gameOverController;
	public GameObject gameOverPanel;
	public GameObject pausePanel;
	public Text victoryText;


	private void Start()
	{
		if (_towerManager != null)
			return;

		_towerManager = TowerManager.instance;
        infoText.text = "";
		infoText.enabled = false;
		pausePanel.SetActive(false);
        gameOverPanel.SetActive(false);
	}

	public void Initialize(SpawnManager spawn, Store store, Player player)
	{
		_spawnManager = spawn;
		_store = store;
        _player = player;
	}

	private void Update()
	{
		moneyText.text = Player.money.ToString();
		waveText.text = string.Format("{0}/{1}", _spawnManager.GetCurrWave(), _spawnManager.numberOfWaves);
        livesLeft.text = Player.lives.ToString();

        // // GameOver/Victory testing
        //if (Input.GetKeyDown(KeyCode.Q))
        //    GameOver();
        //else if (Input.GetKeyDown(KeyCode.Space))
        //	Victory();

		if (Input.GetKeyDown(KeyCode.Tab))
		{
			if (!isPaused)
			{
				isPaused = true;
				Time.timeScale = 0;
				pausePanel.SetActive (true);
			}
			else if (isPaused) 
			{
				isPaused = false;
				Time.timeScale = 1;
				pausePanel.SetActive (false);
			}
		}

        if (Player.lives <= 0)
            GameOver();
        else if (_spawnManager.GetCurrWave() >= _spawnManager.numberOfWaves && UnitSpawner.unitsAlive == 0)
            Victory();

	}

    public void DisplayTowerInfo(string type)
    {
		infoText.enabled = true;

        switch(type)
        {
            case "Rock":
                infoText.text = "It's a rock!";
                break;

            case "Fire":
                infoText.text = "A fire tower that has an area of effect hit!";
                break;

            case "Ice":
                infoText.text = "An ice tower that slows enemies!";
                break;

            case "Lightning":
                infoText.text = "A lightning tower that attacks extremely fast!";
                break;

            case "Wind":
                infoText.text = "A wind tower that does constant close-range damage!";
                break;
        }
    }

    public void ClearText()
    {
        infoText.text = "";
		infoText.enabled = false;
    }

    public void GameOver()
    {
		gameOverPanel.SetActive(true);
		gameOverController.Play("GameOver");
    }

	public void Victory()
	{
		gameOverPanel.SetActive(true);
		victoryText.text = "A Winner is You!";
		victoryText.color = Color.yellow;
		gameOverController.Play("GameOver");
	}

    public void Restart()
    {
        SceneManager.LoadScene(2);
    }

    public void LoadScene(int sceneNum)
    {
        SceneManager.LoadScene(sceneNum);
    }

    public void Quit()
    {
        Application.Quit();
    }
}
