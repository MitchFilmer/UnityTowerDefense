using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;
using System.Collections.Generic;

using SGF;
using Debug = SGF.Debug;
public class GameLoader : AsyncLoader
{
    public int sceneIndexToLoad = 1;
    public List<Component> GameModules;

    private static int _sceneIndex = 1;
    private static GameLoader _instance;
    
	protected override void Awake()
	{
		Debug.Log("GameLoader Starting");

		// Saftey check
		if (_instance != null && _instance != this)
		{
			Debug.Log("A duplicate instance of the GameLoader was found, and will be ignored. Only one instance is permitted");
			Destroy(gameObject);
			return;
		}

		// Set reference to this instance
		_instance = this;

		// Make persistent
		DontDestroyOnLoad(gameObject);

        // Scene Index Check
        if (sceneIndexToLoad < 0 || sceneIndexToLoad >= SceneManager.sceneCountInBuildSettings)
        {
            Debug.Log(string.Format("Invalid Scene Index {0} ... using default value of {1}", sceneIndexToLoad, _sceneIndex));
        }
        else
        {
            _sceneIndex = sceneIndexToLoad;
        }

		// Setup System GameObject
		GameObject systemsGO = new GameObject("[Services]");
        systemsGO.tag = "Services";
		Transform systemsParent = systemsGO.transform;
		DontDestroyOnLoad(systemsGO);

        // Queue up loading routines
        Enqueue(IntializeCoreSystems(systemsParent), 1);
		Enqueue(InitializeModularSystems(systemsParent), 2);
        //Enqueue(LoadInitialScene(_sceneIndex), 3);

		// Set completion callback
		CallOnComplete(OnComplete);
	}

	private IEnumerator IntializeCoreSystems(Transform systemsParent)
	{
        // Setup Core Systems
        // ItemDatabase
        Debug.Log("GameLoader -> ItemDatabase");
        SGF.ServiceLocator.Register<ItemDatabase>(new ItemDatabase().Initialize());

		yield return null;
	}

	private IEnumerator InitializeModularSystems(Transform systemsParent)
	{
        // Setup Additional Systems as needed
        Debug.Log("Loading Modular Systems");
        foreach (Component c in GameModules)
        {
            if (c is IGameModule)
            {
                IGameModule module = (IGameModule)c;
                yield return module.LoadModule();
            }
        }

		yield return null;
	}

    private IEnumerator LoadInitialScene(int index)
    {
        Debug.Log("GameLoader -> Starting Scene Load");        
        yield return SceneManager.LoadSceneAsync(index);
    }

	// AsyncLoader completion callback
	private void OnComplete()
	{
		Debug.Log("GameLoader Finished Initializing");
        StartCoroutine(LoadInitialScene(_sceneIndex));
    }
}