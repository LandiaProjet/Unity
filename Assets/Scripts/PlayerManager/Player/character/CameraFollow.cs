using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    public GameObject player;
    public float timeOffset;

    private Vector3 velocity;

    public Vector3 minCameraPos;
    public Vector3 maxCameraPos;

    void Update()
    {
        if (player.activeSelf == true)
        {
            float posX = Mathf.SmoothDamp(transform.position.x, player.transform.position.x, ref velocity.x, timeOffset); 
            float posY = Mathf.SmoothDamp(transform.position.y, player.transform.position.y, ref velocity.y, timeOffset); 

            transform.position = new Vector3(posX, posY, transform.position.z);

            transform.position = new Vector3(Mathf.Clamp(transform.position.x, minCameraPos.x, maxCameraPos.x),
                                            Mathf.Clamp(transform.position.y, minCameraPos.y, maxCameraPos.y),
                                            Mathf.Clamp(transform.position.z, minCameraPos.z, maxCameraPos.z));
        }
    }
}