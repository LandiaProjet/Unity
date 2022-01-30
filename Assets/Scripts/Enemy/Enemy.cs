using UnityEngine;

public class Enemy : MonoBehaviour
{
    float health { get; set; }

    public void ReceiveDommage(float damage){
        this.health -= damage;
        if (health <= 0)
            onDie(); 
    }

    public void onDie(){}
}