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
    public string Count;

    public object Clone()
    {
        return this.MemberwiseClone();
    }
}
