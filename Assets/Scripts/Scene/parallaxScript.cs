using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class parallaxScript : MonoBehaviour
{
    public float speed;
    
    private GameObject[][] background = new GameObject[5][];
    private Transform cam;
    private GameObject player;
    private Vector3 velocity;
    private Vector2 positionPlayer;

    /*void Start()
    {
        Debug.Log("passe");
        player = GameObject.FindGameObjectWithTag("Player");
        cam = GameObject.FindGameObjectWithTag("MainCamera").transform;
        positionPlayer = new Vector2(0, 0);
        int children = transform.childCount;
        for (int i = 0; i < children; i++)
        {
            background[i] = new GameObject[2];
            background[i][0] = transform.GetChild(i).gameObject;
            background[i][0].transform.position = new Vector3(0, 0, 0);
            Debug.Log(background[i][0].transform.position);
            background[i][1] = Instantiate(background[i][0]);
            SpriteRenderer spriteRenderer = background[i][0].GetComponent<SpriteRenderer>();
            background[i][1].transform.position = background[i][1].transform.position + new Vector3(spriteRenderer.size.x, 0, 0);
        }
    }*/

    void FixedUpdate()
    {
        if (player == null)
        {
            player = GameObject.FindGameObjectWithTag("Player");
            if (player == null)
                return;
            cam = GameObject.FindGameObjectWithTag("MainCamera").transform;
            positionPlayer = player.transform.position;
            int children = transform.childCount;
            for (int i = 0; i < children; i++)
            {
                background[i] = new GameObject[2];
                background[i][0] = transform.GetChild(i).gameObject;
                background[i][1] = Instantiate(background[i][0], transform);
                SpriteRenderer spriteRenderer = background[i][0].GetComponent<SpriteRenderer>();
                background[i][1].transform.position = background[i][1].transform.position + new Vector3(spriteRenderer.size.x, 0, 0);
            }
            executeParallax();
        }
        if (Mathf.Abs(positionPlayer.x - player.transform.position.x) > 0.01)
        {
            executeParallax();
        }
    }

    void executeParallax()
    {
        for (int i = 0; i < background.Length; i++)
        {
            if (i == 0)
                continue;
            int position_reverse = (background.Length - 1) - i;
            float movement = (positionPlayer.x > player.transform.position.x) ? speed * position_reverse : -(speed * position_reverse);
            background[i][0].transform.position = Vector3.Lerp(background[i][0].transform.position, background[i][0].transform.position + new Vector3(movement, 0, 0), 1f * Time.fixedDeltaTime);
            background[i][1].transform.position = Vector3.Lerp(background[i][1].transform.position, background[i][1].transform.position + new Vector3(movement, 0, 0), 1f * Time.fixedDeltaTime);
        }
        for (int i = 0; i < background.Length; i++)
        {
            for (int o = 0; o < background[i].Length; o++)
            {
                SpriteRenderer spriteRenderer = background[i][o].GetComponent<SpriteRenderer>();
                Vector3 position = cam.position - background[i][o].transform.position;
                if (positionPlayer.x < player.transform.position.x)
                {
                    if (position.x >= spriteRenderer.size.x)
                    {
                        background[i][o].transform.position = background[i][o].transform.position + new Vector3(spriteRenderer.size.x * 2, 0, 0);
                    }
                }
                else
                {
                    if (position.x <= -spriteRenderer.size.x)
                    {
                        background[i][o].transform.position = background[i][o].transform.position - new Vector3(spriteRenderer.size.x * 2, 0, 0);
                    }
                }
            }
        }
        positionPlayer = player.transform.position;
    }
}
