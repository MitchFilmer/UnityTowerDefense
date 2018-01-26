using UnityEngine;
using System.Collections;
using System.Collections.Generic;

using SGF;

public class TestDataSystem : MonoBehaviour
{
    public string DataID;
    private DataLoader _dataLoader;
    private JsonDataSource _jsonDataSource;

    private void Awake()
    {
        GameLoader.CallOnComplete(Init);
    }

    private void Init()
    {
        _dataLoader = ServiceLocator.Get<DataLoader>();
        if (_dataLoader.LoadedDataSources.ContainsKey(DataID))
        {
            _jsonDataSource = _dataLoader.LoadedDataSources[DataID] as JsonDataSource;
            foreach (KeyValuePair<string, object> kvp in _jsonDataSource.DataDictionary)
            {
                if (kvp.Key != null && kvp.Value != null)
                {
                    SGF.Debug.Log(string.Format("{0} - {1}", kvp.Key, kvp.Value.ToString()));
                }
            }
        }
    }
}
