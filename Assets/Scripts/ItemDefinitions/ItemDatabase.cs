using UnityEngine;
using System;
using System.Collections.Generic;

public class ItemDatabase
{
    public readonly List<ItemDefinition> allItems = new List<ItemDefinition>();
    public readonly List<Type> allTypes = new List<Type>();

    public readonly Dictionary<Type, List<ItemDefinition>> itemsByType = new Dictionary<Type, List<ItemDefinition>>();
    public readonly Dictionary<string, List<ItemDefinition>> itemsByTags = new Dictionary<string, List<ItemDefinition>>();
    public readonly Dictionary<Type, Dictionary<string, ItemDefinition>> itemsByNameAndType = new Dictionary<Type, Dictionary<string, ItemDefinition>>();
    public readonly Dictionary<string, ItemDefinition> itemsByName = new Dictionary<string, ItemDefinition>();
    public readonly Dictionary<string, Type> typesByName = new Dictionary<string, Type>(StringComparer.Ordinal);

    // Called in GameLoader, Registered as a Service.
    public ItemDatabase Initialize()
    {
        ItemDefinition[] itemDefinitionResources = Resources.LoadAll<ItemDefinition>(GameDefs.PathInfo.ItemDefinitions);
        SGF.Debug.Log(string.Format("Found {0} ItemDefinitions", itemDefinitionResources.Length));

        ProcessItemDefinitions(itemDefinitionResources);
        return this;
    }

    // This is kinda heavy handed ... 
    private void ProcessItemDefinitions(ItemDefinition[] items)
    {
        for (int i = 0; i < items.Length; ++i)
        {
            // Add to list of all items
            allItems.Add(items[i]);

            // Add type to list of all types
            allTypes.Add(items[i].GetType());

            // Update itemsByType dictionary
            if (!itemsByType.ContainsKey(items[i].GetType()))
            {
                itemsByType.Add(items[i].GetType(), new List<ItemDefinition>());
            }
            itemsByType[items[i].GetType()].Add(items[i]); // Shouldn't be any duplicates here ... right???? 

            // TODO - Add tags to base item def, update itemsByTags

            // Update itemsByNameAndType
            if (!itemsByNameAndType.ContainsKey(items[i].GetType()))
            {
                itemsByNameAndType.Add(items[i].GetType(), new Dictionary<string, ItemDefinition>());
            }

            if (!itemsByNameAndType[items[i].GetType()].ContainsKey(items[i].name))
            {
                itemsByNameAndType[items[i].GetType()].Add(items[i].name, items[i]);
            }

            // Update itemsByName
            if (!itemsByName.ContainsKey(items[i].name))
            {
                itemsByName.Add(items[i].name, items[i]);
            }

            // Update typesByName
            if (!typesByName.ContainsKey(items[i].name))
            {
                typesByName.Add(items[i].name, items[i].GetType());
            }
        }
    }

    public ItemDefinition GetItemByName(string name)
    {
        ItemDefinition result = null;
        itemsByName.TryGetValue(name, out result);
        return result;
    }

    public List<ItemDefinition> GetItemsByType(Type t)
    {
        List<ItemDefinition> result = null;
        itemsByType.TryGetValue(t, out result);
        return result;
    }

    public ItemDefinition GetItemByTypeAndName(Type t, string name)
    {
        ItemDefinition result = null;
        Dictionary<string, ItemDefinition> defByName;
        itemsByNameAndType.TryGetValue(t, out defByName);
        if (defByName != null)
        {
            defByName.TryGetValue(name, out result);
        }

        return result;
    }
}
