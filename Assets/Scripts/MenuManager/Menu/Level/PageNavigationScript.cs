using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PageNavigationScript : MonoBehaviour
{
    public GameObject Parent;
    public GameObject PageNavigationL;
    public GameObject PageNavigation;

    public GameObject Point;
    private RectTransform PointRect;

    private List<GameObject> NavigationList = new List<GameObject>();

    public void generateNavigation(int count)
    {
        GameObject pageNavigation;
        RectTransform rt;
        Vector2 position;

        PointRect = Point.GetComponent<RectTransform>();
        for (int i = 0; i < count; i++)
        {
            if (i != count - 1)
                pageNavigation = Instantiate(PageNavigationL, Parent.transform);
            else
                pageNavigation = Instantiate(PageNavigation, Parent.transform);
            NavigationList.Add(pageNavigation);
            rt = pageNavigation.GetComponent<RectTransform>();
            position = rt.position;
            position.x += i * 66.5f;
            rt.position = position;
        }
    }

    public void movePosition(bool isNexting)
    {
        Vector2 position = PointRect.localPosition;

        if (isNexting)
            position.x += 50;
        else
            position.x -= 50;
        PointRect.localPosition = position;
    }
}
