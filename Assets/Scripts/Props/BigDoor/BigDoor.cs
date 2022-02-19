using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BigDoor : MonoBehaviour
{
    public float speed;

    public Transform point;
    public LeverScript[] levers;

    private Vector3 targetPosition;

    private bool Opening;
    private bool isOpened;

    private void Start()
    {
        targetPosition = point.position;
    }

    void Update()
    {
        if (Opening && !isOpened)
        {
            Vector3 dir = targetPosition - transform.position;
            transform.Translate(dir.normalized * speed * Time.deltaTime, Space.World);

            if (Vector3.Distance(transform.position, targetPosition) < 0.3f)
            {
                isOpened = true;
            }
        }
        else if (!Opening && !isOpened)
        {
            foreach (LeverScript lever in levers)
            {
                if (!lever.Enable)
                    return;
            }
            Opening = true;
        }
    }
}
