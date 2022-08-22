using UnityEngine;
using System.Collections;
using System;

public class InteractionScript : MonoBehaviour
{
    public enum Action
     {
         Teleport, 
         Menu, 
         Lever
     };
    public Action isMenu;
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
                interactManager = InteractManager.instance;
                InteractManager.instance.applyChange();
            }
            if (inside == false)
            {
                inside = true;
                InteractManager.instance.entry.callback.AddListener((data) => { Execute(); });
                InteractManager.instance.trigger.triggers.Add(InteractManager.instance.entry);
                InteractManager.instance.image.sprite = sprite;
                InteractManager.instance.InteractButton.SetActive(true);
                if (hover)
                    hover.SetActive(true);
            }
        } else
        {
            if (inside == true)
            {
                inside = false;
                InteractManager.instance.trigger.triggers.RemoveRange(0, InteractManager.instance.trigger.triggers.Count);
                InteractManager.instance.entry.callback.RemoveAllListeners();
                InteractManager.instance.InteractButton.SetActive(false);
                if (hover)
                    hover.SetActive(false);
            }
        }
    }

    private void Execute()
    {
        SoundManager.instance.PlayEffectSound(0);
        if (isMenu.Equals(Action.Menu)){
            MenuManager.instance.OpenMenu(menu, zindex);
        } if (isMenu.Equals(Action.Teleport)){
            StartCoroutine(executeTeleport());
        } else {
            GetComponent<LeverScript>().onDie();
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
