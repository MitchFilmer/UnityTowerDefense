using UnityEngine;
using System.Collections;

public static class GameDefs
{
    public class InventoryKeys
    {
        public static readonly string InventoryKey = "MT_INVENTORY_KEY";
        public static readonly string DefaultInventoryData = "DefaultPlayerInventory";
        public static readonly string UnitInventoryKey = "Units"; // maybe we want to look at all lower/upper case
    }

    public class PathInfo
    {
        public static readonly string ItemDefinitions = "ItemData/ItemDefinitions";
    }

    public class ItemDefTypeNames
    {
        public const string MeleeItemDefTypeName = "DefaultMeleeNPC";
        public const string RangedItemDefTypeName = "DefaultRangedNPC";
    }

    public class WaveDefTypeNames
    {
        public const string BasicSpawnerWaveDefinition = "BasicSpawnerWaveDefinition";
    }

    public class EventId
    {
        // following string format of existing SteamVR events
        public const string ControllerModelSet = "controller_model_set"; 
        public const string TrackedObjectDeviceIndexSet = "tracked_object_device_index_set";
        public const string TrackedObjectDeviceRoleChanged = "tracked_object_device_role_changed";
    }
}
