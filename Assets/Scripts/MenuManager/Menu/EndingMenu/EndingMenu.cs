using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EndingMenu : MonoBehaviour
{
    public TMPro.TextMeshProUGUI Cointext;
    public GameObject StarList;
    public TMPro.TextMeshProUGUI Scoretext;

    private void OnEnable()
    {
        UpgraderCount anim = new UpgraderCount(0.5f, isPlaying.instance.credit, 0.001f, Cointext);
        StartCoroutine(anim.StartAnimation());
        anim = new UpgraderCount(0.5f, isPlaying.instance.exp, 0.001f, Scoretext);
        StartCoroutine(anim.StartAnimation());
        if (StarList != null)
        {
            for (int i = 1; i <= 3; i++)
                StarList.transform.GetChild(1).gameObject.SetActive(false);
            for (int i = 1; i <= isPlaying.instance.star; i++)
            {
                StarList.transform.GetChild(i).gameObject.SetActive(true);
            }
        }
    }
}
