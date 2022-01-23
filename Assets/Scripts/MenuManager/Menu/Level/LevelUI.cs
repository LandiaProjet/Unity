using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelUI : MonoBehaviour
{
	public GameObject Pages;
	public GameObject Stage;
	public GameObject levelButton;

    public GameObject prevPageButton;
    public GameObject nextPageButton;

    public PageNavigationScript PageNavi;

    int countPage = 0;
    bool onAnimation = false;

    private List<GameObject> Stages = new List<GameObject>();
    private List<GameObject> Buttons = new List<GameObject>();
    private RectTransform rtPage;

    private void Start()
    {
        GameObject button;
        RectTransform rtPrev;
        RectTransform rtNext;

        rtPage = Pages.GetComponent<RectTransform>();
        Stages.Add(Instantiate(Stage, Pages.transform));
        rtPrev = Stages[0].GetComponent<RectTransform>();
        for (int i = 0, c = 0, p = 0; i < 100; i++, c++)
        {
            button = Instantiate(levelButton, Stages[p].transform);
            Buttons.Add(button);
            button.GetComponent<LevelButton>().InitButton(i);
            if (c == 17)
            {
                Stages.Add(Instantiate(Stage, Pages.transform));
                p++;
                rtNext = Stages[p].GetComponent<RectTransform>();
                rtNext.transform.localPosition = new Vector2(rtPrev.transform.localPosition.x + 3000, rtPrev.transform.localPosition.y);
                rtPrev = rtNext;
                c = -1;
            }
        }
        if (Stages.Count > 1)
            nextPageButton.SetActive(true);
        PageNavi.generateNavigation(Stages.Count);
    }

    public void nextPage()
    {
        if (onAnimation == true && Stages.Count - 1 >= countPage + 1)
            return;
        onAnimation = true;
        prevPageButton.SetActive(true);
        countPage++;
        PageNavi.movePosition(true);
        if (Stages.Count - 1 <= countPage)
            nextPageButton.SetActive(false);
        StartCoroutine(animationNextPage());
    }

    public void prevPage()
    {
        if (onAnimation == true && Stages.Count <= 0)
            return;
        onAnimation = true;
        nextPageButton.SetActive(true);
        countPage--;
        PageNavi.movePosition(false);
        if (countPage <= 0)
            prevPageButton.SetActive(false);
        StartCoroutine(animationPrevPage());
    }

    private IEnumerator animationNextPage()
    {
        int count = 0;

        while (count < 3000)
        {
            rtPage.transform.localPosition = new Vector2(rtPage.transform.localPosition.x - 100, rtPage.transform.localPosition.y);
            count += 100;
            yield return new WaitForSeconds(.005f);
        }
        onAnimation = false;
    }

    private IEnumerator animationPrevPage()
    {
        int count = 0;

        while (count < 3000)
        {
            rtPage.transform.localPosition = new Vector2(rtPage.transform.localPosition.x + 100, rtPage.transform.localPosition.y);
            count += 100;
            yield return new WaitForSeconds(.005f);
        }
        onAnimation = false;
    }
}
