using UnityEngine;

public class Enemy : MonoBehaviour
{
    public float health;

    public virtual void ReceiveDommage(float damage){
        this.health -= damage;
        if (health <= 0)
            onDie(); 
    }

    public virtual void onDie(){}
}