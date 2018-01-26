using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using SGF;
using Debug = SGF.Debug;
public class DataLoader : MonoBehaviour, IGameModule
{
    public List<UnityEngine.Object> DataSources;
    private string _loadError;
    public string LoadError
    {
        get { return _loadError; }
        set { _loadError = value; }
    }
    
    public event Action OnSourcesLoaded;

    public Dictionary<string, IDataSource> LoadedDataSources;

    private void Awake()
    {
        LoadedDataSources = new Dictionary<string, IDataSource>();
        LoadError = string.Empty;
        OnSourcesLoaded += PrintData;
        GameLoader.CallOnComplete(Init);
    }

    private void OnDisable()
    {
        OnSourcesLoaded -= PrintData;
    }

    private void Init()
    {
        ServiceLocator.Register<DataLoader>(this);
    }

    public IEnumerator LoadModule()
    {
        Debug.Log("<color=pink>Loading Module!!!</color>");
        foreach (UnityEngine.Object obj in DataSources)
        {
            if (obj is IDataSource)
            {
                IDataSource source = obj as IDataSource;
                yield return LoadAsync(source);
            }
        }

        // Wait a frame then call the loaded handler
        yield return new WaitForEndOfFrame();

        if (OnSourcesLoaded != null)
        {
            OnSourcesLoaded.Invoke();
        }
    }

    public virtual void Load(IDataSource source)
    {
        if (source.IsLoading)
        {
            return;
        }
        StartCoroutine(LoadAsync(source));
    }

    protected IEnumerator LoadAsync(IDataSource source)
    {
        if (!source.IsLoading)
        {
            Debug.Log("Loading Source: " + source.ID);
            source.IsLoading = true;
            yield return source.LoadAsync();
            source.IsLoaded = true;
            LoadedDataSources.Add(source.ID, source);
            LoadingCompleted(source);
        }
    }

    protected void LoadingCompleted(IDataSource source)
    {
        source.HandleOnLoaded();
    }

    public IDataSource GetDataSourceByName(string name)
    {
        if (LoadedDataSources.ContainsKey(name))
        {
            return LoadedDataSources[name];
        }
        return null;
    }

    private void PrintData()
    {
        foreach (KeyValuePair<string, IDataSource> kvp in LoadedDataSources)
        {
            Debug.Log(string.Format("<color=yellow> Key:{0} Value{1}</color>", kvp.Key, kvp.Value.ID));
        }
    }
}
