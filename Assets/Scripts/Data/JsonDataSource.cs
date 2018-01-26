using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JsonDataSource : ScriptableObject, IDataSource
{
    #region Interface Functions
    public string ID { get; set; }
    public bool IsLoading { get; set; }
    public bool IsLoaded { get; set; }
    public DateTime LoadedTime { get; set; }
    public void Load() { }
    public string LoadError { get; set; }
    #endregion

    public Action OnLoaded;
    public TextAsset JsonTextAsset;
    public Dictionary<string, object> DataDictionary;

    private void OnEnable()
    {
        LoadError = string.Empty;
        LoadedTime = DateTime.MinValue;
        DataDictionary = null;
        IsLoaded = false;
        IsLoading = false;
        ID = string.Empty;
    }

    public IEnumerator LoadAsync()
    {
        try
        {
            object deserializedObject = JsonFx.Json.JsonReader.Deserialize(JsonTextAsset.text);
            if (deserializedObject is Dictionary<string, object>)
            {
                DataDictionary = (Dictionary<string, object>)deserializedObject;
                LoadedTime = DateTime.UtcNow;
                ID = JsonTextAsset.name;
                IsLoaded = true;
            }
            else
            {
                LoadError = "TextAsset does not deserialize into Dictionary";
            }
        }
        catch(System.Exception e)
        {
            LoadError = string.Format("Could not parse string: {0}", e.Message);
        }

        yield return new WaitForEndOfFrame();
    }

    public void HandleOnLoaded()
    {
        if (OnLoaded != null)
        {
            OnLoaded.Invoke();
        }
    }
}
