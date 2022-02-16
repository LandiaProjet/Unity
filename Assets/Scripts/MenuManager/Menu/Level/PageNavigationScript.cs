using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PageNavigationScript : MonoBehaviour
{
    public GameObject Parent;
    public GameObject PageNavigationL;
    public GameObject PageNavigation;

    public GameObject Point;

    private List<GameObject> NavigationList = new List<GameObject>();

    public void generateNavigation(int count)
    {
        GameObject pageNavigation;
        GameObject OrangePoint;

        for (int i = 0; i < count; i++)
        {
            if (i != count - 1)
                pageNavigation = Instantiate(PageNavigationL, Parent.transform);
            else
                pageNavigation = Instantiate(PageNavigation, Parent.transform);
            OrangePoint = pageNavigation.transform.GetChild(0).gameObject;
            if (i == 0)
                OrangePoint.SetActive(true);
            NavigationList.Add(OrangePoint);
        }
    }

    public void movePosition(int Page, int LastPage)
    {
        NavigationList[LastPage].SetActive(false);
        NavigationList[Page].SetActive(true);
    }
}
