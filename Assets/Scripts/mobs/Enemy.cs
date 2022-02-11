using UnityEngine;
using System.Collections;

public class Enemy : MonoBehaviour
{
    public float health;

    private SpriteRenderer sprite;
    private Color BloodColor = new Color(0, 0, 0, 1);

    public virtual void ReceiveDommage(float damage){
        this.health -= damage;
        if (health <= 0)
            onDie();
        else
            HitAnimation();
    }

    public virtual void onDie(){}

    public virtual void HitAnimation()
    {
        if (sprite == null)
            sprite = GetComponent<SpriteRenderer>();
        StartCoroutine(TransitionColor(1f, BloodColor));
        StartCoroutine(TransitionColor(1f, Color.white));
    }

    private IEnumerator TransitionColor(float time, Color color)
    {
        float fadeTime = 2;
        float fadeStart = 0;

        while (fadeStart < fadeTime)
        {
            fadeStart += time;
            sprite.color = Color.Lerp(sprite.color, color, fadeStart);
            yield return new WaitForSeconds(time);
        }
    }
}