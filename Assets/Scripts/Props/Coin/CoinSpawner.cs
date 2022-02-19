using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CoinSpawner : MonoBehaviour
{
    public static CoinSpawner instance;

    public GameObject coins;
    
    private void Awake() {
        instance = this;
    }

    public void SpawnCoins(int rdm_amount, Transform transform){
        Vector2 trajectory = UnityEngine.Random.insideUnitCircle * 10f;
        for (int i = 0; i <= rdm_amount; i++)
        {
            GameObject coin = Instantiate(coins, transform.position + new Vector3(0,0.5F,0), Quaternion.identity);
            coin.GetComponent<Rigidbody2D>().AddForce(new Vector2(Random.Range(-10f,10f) + trajectory.x, 0.5f + trajectory.y), ForceMode2D.Impulse);
        }
    }

}
