using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class ItemRecommanded
{
    public int count;
    public int id;
}

[CreateAssetMenu(fileName = "Level", menuName = "Levels/level")]
public class Level : ScriptableObject
{
    public int idScene; // id la sc�ne pour pouvoir l'ouvrir
    public int secondTimeMax; // temps max que le joueur aura le droit pour terminer le level
    public int[] timeStar; // doit avoir 3 Elements � l'int�rieur, le premier element repr�sente la premi�re �toile et ainsi de suite
    public ItemRecommanded[] itemsRecommanded; // la liste des items recommander
}
