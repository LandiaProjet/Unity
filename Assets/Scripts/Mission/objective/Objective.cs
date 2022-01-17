using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Objective", menuName = "Missions/Objective")]
public class Objective : ScriptableObject
{
    public string description;
    public string title;
    public Item Clone()
    {
        return (Item)MemberwiseClone();
    }
}
