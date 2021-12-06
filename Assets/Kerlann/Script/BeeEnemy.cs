using System;
using System.Collections;
using Pathfinding;
using UnityEngine;

public class BeeEnemy : MonoBehaviour
{
    public AIPath path;

    private void Update()
    {
        if (path.desiredVelocity.x >= 0.01f)
        {
            transform.localScale = new Vector3(-1f,1f,1f);
        } else if (path.desiredVelocity.x <= -0.01f)
        {
            transform.localScale = new Vector3(1f,1f,1f);
        }
    }
}