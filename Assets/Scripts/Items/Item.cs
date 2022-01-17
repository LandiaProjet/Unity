using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[CreateAssetMenu(fileName = "Item", menuName = "Inventory/Item")]
public class Item : ScriptableObject
{
    public Sprite icon;
    public string name;
    public string description;
    public int count;

    public Item Clone()
    {
        return (Item) MemberwiseClone();
    }
}
