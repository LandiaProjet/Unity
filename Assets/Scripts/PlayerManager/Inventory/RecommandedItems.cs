using UnityEngine;
using UnityEngine.UI;

public class RecommandedItems : MonoBehaviour
{
    public GameObject prefabs;
    public GameObject contents;

    private void OnEnable()
    {
        ItemRecommanded[] itemsList = Levels.instance.levels[isPlaying.instance.idLevel].itemsRecommanded;

        foreach (Transform child in contents.transform)
        {
            Destroy(child.gameObject);
        }
        if (itemsList == null)
            return;
        foreach (ItemRecommanded iditem in itemsList)
        {
            GameObject item = Instantiate(prefabs, contents.transform);
            Image icon = item.transform.Find("ItemIcon").GetComponentInChildren<Image>();
            TMPro.TextMeshProUGUI text = item.GetComponentInChildren<TMPro.TextMeshProUGUI>();
            icon.sprite = Items.instance.items[iditem.id].icon;
            text.text = iditem.count.ToString();
        }
    }
}
