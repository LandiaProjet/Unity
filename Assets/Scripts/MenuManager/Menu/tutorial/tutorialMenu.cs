using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class tutorialMenu : MonoBehaviour
{
    public float timeBeforeClick;
    public bool setStats;

    private int count = 0;
    private float countTime = 0;

    void Update()
    {
        countTime += Time.deltaTime;
    }

    public void OnClick()
    {
        if (countTime < timeBeforeClick)
            return;
        gameObject.transform.GetChild(count).gameObject.SetActive(false);
        count++;
        countTime = 0;
        if (gameObject.transform.childCount <= count)
        {
            if (setStats)
                isPlaying.instance.stats = Stats.inGame;
            gameObject.SetActive(false);
            return;
        }
        gameObject.transform.GetChild(count).gameObject.SetActive(true);
    }
}
