using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotationAnimation : MonoBehaviour
{
    public float speed;
    public float Angle;

    private Vector3 eulerAngles;

    private void OnEnable()
    {
        eulerAngles = transform.eulerAngles;
    }

    void Update()
    {
        Vector3 dir = new Vector3(0, 0, Angle);
        transform.Rotate(dir.normalized * speed * Time.deltaTime, Space.World);

        Vector3 finalPosition = eulerAngles + dir;
        if (Mathf.Abs(Quaternion.Angle(transform.rotation, Quaternion.Euler(finalPosition.x, finalPosition.y, finalPosition.z))) < 5.0f)
        {
            Angle *= -1;
        }
    }
}
