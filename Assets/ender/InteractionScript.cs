using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class InteractionScript : MonoBehaviour
{
    public GameObject ButtonInteraction;
    public GameObject Icon;

    public float Radius;
    public LayerMask collisionLayers;
    public Sprite sprite;

    private bool inside;

    private Image image;
    private EventTrigger trigger;
    private EventTrigger.Entry entry;

    void Start()
    {
        inside = false;
        image = Icon.GetComponentInChildren<Image>();
        trigger = ButtonInteraction.GetComponent<EventTrigger>();
        entry = new EventTrigger.Entry
        {
            eventID = EventTriggerType.PointerClick
        };
    }

    private void FixedUpdate()
    {
        Collider2D[] CircleResult = Physics2D.OverlapCircleAll(transform.position, Radius, collisionLayers);

        if (CircleResult != null && CircleResult.Length >= 1)
        {
            if (inside == false)
            {
                inside = true;
                entry.callback.AddListener((data) => { Execute(); });
                trigger.triggers.Add(entry);
                image.sprite = sprite;
                ButtonInteraction.SetActive(true);
            }
        } else
        {
            if (inside == true)
            {
                inside = false;
                trigger.triggers.RemoveRange(0, trigger.triggers.Count);
                entry.callback.RemoveAllListeners();
                ButtonInteraction.SetActive(false);
            }
        }
    }

    private void Execute()
    {
        Debug.Log("yes");
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.green;
        Gizmos.DrawWireSphere(transform.position, Radius);
    }
}
