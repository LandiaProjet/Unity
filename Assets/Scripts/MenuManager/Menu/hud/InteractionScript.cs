using UnityEngine;

public class InteractionScript : MonoBehaviour
{
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
            }
        } else
        {
            if (inside == true)
            {
                inside = false;
                interactManager.trigger.triggers.RemoveRange(0, interactManager.trigger.triggers.Count);
                interactManager.entry.callback.RemoveAllListeners();
                interactManager.InteractButton.SetActive(false);
            }
        }
    }

    private void Execute()
    {
        interactManager.InteractButton.SetActive(false);
        MenuManager.instance.OpenMenu("Level", 10);
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.green;
        Gizmos.DrawWireSphere(transform.position, Radius);
    }
}
