using UnityEngine;
using System.Collections;

public interface IQuantityRestrictedItem
{
    bool HasMinRestriction { get; set; }
    bool HasMaxRestriction { get; set; }
    int MaxQuantity { get; set; }
    int MinQuantity { get; set; }
}