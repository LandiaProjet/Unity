using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Coin : MonoBehaviour
{
    public GameObject EffectCoin;

    private void OnTriggerEnter2D(Collider2D other) {
        if(other.CompareTag("Player")){
            isPlaying.instance.credit++;
            Instantiate(EffectCoin, transform.position, transform.rotation);
            Destroy(gameObject);
        } 
    }
}
