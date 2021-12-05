using UnityEngine;
using System.Collections;

public class DestroyScript : MonoBehaviour
{
    public Animator animator;
    public Collider2D collision;

    void Start()
    {
        animator = transform.GetComponent<Animator>();
        collision = transform.GetComponent<Collider2D>();
    }

    void Update()
    {
        // Condition à supprimer c'est pour les tests
        if (Input.GetKeyDown(KeyCode.H))
        {
            DestroyBarrel();
        }
    }

    public void DestroyBarrel()
    {
        StartCoroutine(onDestroyBarrel());
    }

    private IEnumerator onDestroyBarrel()
    {
        collision.enabled = false;
        animator.SetTrigger("Destroy");
        yield return new WaitForSeconds(1f);
        Destroy(gameObject);
    }
}
