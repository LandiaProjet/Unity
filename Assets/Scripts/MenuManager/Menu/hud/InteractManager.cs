using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;

public class InteractManager : MonoBehaviour
{
    public static InteractManager instance;

    public GameObject InteractButton;
    public GameObject Icon;

    public Image image;
    public EventTrigger trigger;
    public EventTrigger.Entry entry;

    private string sceneName;

    void Start()
    {
        if (instance != null)
        {
            Debug.LogWarning("Il y a plus d'une instance de InteractManager dans la scène");
            return;
        }
        instance = this;
    }

    void Update()
    {
        if (sceneName != SceneManager.GetActiveScene().name)
        {
            StartCoroutine(waitforApply());
            sceneName = SceneManager.GetActiveScene().name;
        }
    }

    private void OnEnable()
    {
        applyChange();
    }

    IEnumerator waitforApply()
    {
        yield return new WaitForSeconds(2f);
        applyChange();
    }

    public void applyChange()
    {
        InteractionScript[] components = Resources.FindObjectsOfTypeAll<InteractionScript>();

        image = Icon.GetComponentInChildren<Image>();
        trigger = InteractButton.GetComponent<EventTrigger>();
        entry = new EventTrigger.Entry
        {
            eventID = EventTriggerType.PointerClick
        };
        foreach (InteractionScript component in components)
        {
            component.interactManager = this;
            component.inited = true;
        }
    }
}
