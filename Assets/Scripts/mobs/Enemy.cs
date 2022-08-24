using UnityEngine;
using System.Collections;

public class Enemy : MonoBehaviour
{
    public float health;

    private SpriteRenderer sprite;

    public virtual void ReceiveDommage(float damage){
        this.health -= damage;
        if (health <= 0)
        {
            onDie();
        } else {
            HitAnimation();
        }
    }

    public virtual void onDie(){
        MissionScript.instance.addValueMission(0, 1);
        Debug.Log("SPAWNER");
        CoinSpawner.instance.SpawnCoins(10, this.transform);
    }

    bool HitAnimationIsEnable;

    public void HitAnimation()
    {
        if (HitAnimationIsEnable)
            return;
        if (sprite == null)
            sprite = GetComponent<SpriteRenderer>();
        HitAnimationIsEnable = true;
        StartCoroutine(TransitionColor());
    }

    private IEnumerator TransitionColor()
    {
        Material before = sprite.material;
        sprite.material = PlayerMovement.instance.impact;
        yield return new WaitForSeconds(0.15f);
        sprite.material = before;
        HitAnimationIsEnable = false;
    }
}