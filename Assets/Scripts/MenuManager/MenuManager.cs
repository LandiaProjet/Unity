using UnityEngine;

public class MenuManager : MonoBehaviour
{
    public GameObject canvas;

    public static MenuManager instance;

    private void Awake()
    {
        if (instance != null)
        {
            Debug.LogWarning("Il y a plus d'une instance de MenuManager dans la scène");
            return;
        }
        instance = this;
    }

    public bool OpenMenu(string name, int zIndex = 0)
    {
        foreach (Transform child in canvas.transform)
        {
            if (child.gameObject.name == name)
            {
                if (child.gameObject.activeSelf == true)
                    return false;
                child.gameObject.SetActive(true);
                child.SetSiblingIndex(zIndex);
                return true;
            }
        }
        return false;
    }

    public bool OpenMenu(GameObject game, int zIndex = 0)
    {
        if (game.activeSelf == false)
        {
            game.SetActive(true);
            game.transform.SetSiblingIndex(zIndex);
            return true;
        }
        return false;
    }

    public bool CloseMenu(string name)
    {
        foreach (Transform child in canvas.transform)
        {
            if (child.gameObject.name == name)
            {
                if (child.gameObject.activeSelf == false)
                    return false;
                child.gameObject.SetActive(false);
                return true;
            }
        }
        return false;
    }

    public bool CloseMenu(GameObject game, Transform parent)
    {
        if (game.activeSelf == true)
        {
            game.SetActive(false);
            return true;
        }
        return false;
    }
}
