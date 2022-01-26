using UnityEngine;

[CreateAssetMenu(fileName = "Item", menuName = "Inventory/Item")]
public class Item : ScriptableObject
{
    public int id;
    public Sprite icon;
    public string name;
    public string description;
    public int count;

    public Item Clone()
    {
        return (Item) MemberwiseClone();
    }
}
