using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[CreateAssetMenu(fileName = "Item", menuName = "Inventory/Item")]
public class Item : ScriptableObject
{
    public Sprite Icon;
    public string Name;
    public string Description;
    public int Count;

    public Item Clone()
    {
        return (Item) MemberwiseClone();
    }
}
