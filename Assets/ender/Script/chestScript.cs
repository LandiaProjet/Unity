using System.Collections;
using UnityEngine;

public class chestScript : MonoBehaviour
{
    public Animator animator;
    public Collider2D collision;

    private void Start()
    {
        animator = transform.GetComponent<Animator>();
        collision = transform.GetComponent<Collider2D>();
    }
    void Update()
    {
        // Condition à supprimer c'est pour les tests
        if (Input.GetKeyDown(KeyCode.C))
        {
            openChest();
        }
    }

    public void openChest()
    {
        StartCoroutine(onOpenChest());
    }

    private IEnumerator onOpenChest()
    {
        animator.SetTrigger("Open");
        yield return new WaitForSeconds(0.2f);
        collision.enabled = false;
    }
}
