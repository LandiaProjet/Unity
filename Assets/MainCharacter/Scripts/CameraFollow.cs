using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    public GameObject player;
    public float timeOffset;
    public Vector3 posOffset;

    private Vector3 velocity;

    public Vector3 minValues, maxValue;

    void Update()
    {
        Vector3 boundPosition = new Vector3(
            Mathf.Clamp(player.transform.position.x, minValues.x, maxValue.x),
            Mathf.Clamp(player.transform.position.y, minValues.y, maxValue.y),
            Mathf.Clamp(player.transform.position.z, minValues.z, maxValue.z));

        transform.position = Vector3.SmoothDamp(transform.position, player.transform.position + posOffset, ref velocity, timeOffset); 
    }
}