using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class DoorTeleporter : MonoBehaviour
{
    public string SceneName;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {
            if (isPlaying.instance.key)
            {
                TransitionManager.instance.fadeTransition.EnableFadeTransition(2f);
                SceneManager.LoadScene(SceneName);
            }
        }
    }
}
