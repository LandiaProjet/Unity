using UnityEngine;

public class VictoryZone : MonoBehaviour
{
    private void Update()
    {
        if (PlayerMovement.instance != null && PlayerMovement.instance.transform.position.x > transform.position.x)
            isPlaying.instance.OnVictory();
    }
}
