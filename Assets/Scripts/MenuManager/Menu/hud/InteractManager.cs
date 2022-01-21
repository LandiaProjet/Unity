using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class InteractManager : MonoBehaviour
{
    public static InteractManager instance;

    public GameObject InteractButton;
    public GameObject Icon;

    public Image image;
    public EventTrigger trigger;
    public EventTrigger.Entry entry;

    void Start()
    {
        if (instance != null)
        {
            Debug.LogWarning("Il y a plus d'une instance de InteractManager dans la scène");
            return;
        }
        instance = this;

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
