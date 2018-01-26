using UnityEngine;
using System.Collections.Generic;

public class ItemDefinition : ScriptableObject, IQuantityRestrictedItem
{
    #region Interface IQuantityRestrictedItem
    public int MaxQuantity { get { return maxQuantity; } set { maxQuantity = value; } }
    public int MinQuantity { get { return minQuantity; } set { minQuantity = value; } }
    public bool HasMaxRestriction { get { return hasMaxRestriction; } set { hasMaxRestriction = value; } }
    public bool HasMinRestriction { get { return hasMinRestriction; } set { hasMinRestriction = value; } }
    #endregion

    public List<string> tags;

    public bool hasMaxRestriction = false;
    public int maxQuantity;
    public bool hasMinRestriction = false;
    public int minQuantity;

    protected int AssetID { get { return GetAssetID(); } }
    private int? _assetID = null;

    protected virtual void Awake()
    {
        if (_assetID == null)
        {
            _assetID = GetInstanceID();
        }
    }

    public virtual void OnCreate() { }
    public virtual void OnLoad() { }

    private int GetAssetID()
    {
        if (_assetID == null)
        {
            SGF.Debug.Log("Asset Instance ID Not Found");
            return -1;
        }

        return System.Convert.ToInt32(_assetID);
    }
}