using UnityEngine;
using System.Collections;
using System;

public class InteractionScript : MonoBehaviour
{
    public bool isMenu;

    public string menu;
    public int zindex;
    public Transform position;

    public GameObject hover;

    public float Radius;
    public LayerMask collisionLayers;
    public Sprite sprite;

    public InteractManager interactManager;
    public bool inited = false;

    private bool inside = false;

    private void FixedUpdate()
    {
        Collider2D[] CircleResult = Physics2D.OverlapCircleAll(transform.position, Radius, collisionLayers);

        if (CircleResult != null && CircleResult.Length >= 1)
        {
            if (inited == false)
            {
                InteractManager.instance.applyChange();
            }
            if (inside == false)
            {
                inside = true;
                interactManager.entry.callback.AddListener((data) => { Execute(); });
                interactManager.trigger.triggers.Add(interactManager.entry);
                interactManager.image.sprite = sprite;
                interactManager.InteractButton.SetActive(true);
                if (hover)
                    hover.SetActive(true);
            }
        } else
        {
            if (inside == true)
            {
                inside = false;
                interactManager.trigger.triggers.RemoveRange(0, interactManager.trigger.triggers.Count);
                interactManager.entry.callback.RemoveAllListeners();
                interactManager.InteractButton.SetActive(false);
                if (hover)
                    hover.SetActive(false);
            }
        }
    }

    private void Execute()
    {
        if (isMenu)
            MenuManager.instance.OpenMenu(menu, zindex);
        else
        {
            StartCoroutine(executeTeleport());
        }
    }

    IEnumerator executeTeleport()
    {
        TransitionManager.instance.fadeTransition.EnableFadeTransition(2f);
        yield return new WaitForSeconds(1f);
        PlayerMovement.instance.transform.position = position.position;
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.green;
        Gizmos.DrawWireSphere(transform.position, Radius);
    }
}
