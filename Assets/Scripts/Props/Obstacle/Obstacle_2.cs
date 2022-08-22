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
        var step =  (isAttack ? speedAttack : speedRest) * Time.deltaTime;
        transform.position = Vector3.MoveTowards(transform.position, targetPosition, step);

        if (Vector3.Distance(transform.position, targetPosition) < 0.01f)
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
