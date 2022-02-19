using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Obstacle_2 : MonoBehaviour
{
    public float speedAttack;
    public float speedRest;
    public bool isAttack;

    public Transform point;

    private Vector3 originPoint;
    private Vector3 targetPosition;

    private void Start()
    {
        originPoint = transform.position;
        targetPosition = point.position;
        isAttack = true;
    }

    void Update()
    {
        Vector3 dir = targetPosition - transform.position;
        transform.Translate(dir.normalized * (isAttack ? speedAttack : speedRest) * Time.deltaTime, Space.World);

        if (Vector3.Distance(transform.position, targetPosition) < 0.3f)
        {
            if (targetPosition == originPoint)
            {
                targetPosition = point.position;
                isAttack = true;
            }
            else
            {
                targetPosition = originPoint;
                isAttack = false;
            }
        }
    }
}
