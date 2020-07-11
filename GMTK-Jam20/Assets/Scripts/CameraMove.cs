using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMove : MonoBehaviour
{
    [SerializeField, Range(0f, 1f)] float awayFromPlayer;

    Transform player;
    Camera mainCamera;

    float initialZ;

    void Start()
    {
        initialZ = transform.position.z;

        mainCamera = GetComponent<Camera>();
        player = GameObject.FindObjectOfType<PlayerMovement>().transform;    
    }

    void Update()
    {
        // Gets mouse pos
        Vector2 mousePos = mainCamera.ScreenToWorldPoint(Input.mousePosition);

        Vector3 playerToMouse = (Vector3)mousePos - player.position;
        // Gets the magnitude of the vector bewteen player and mouse
        float magnitude = playerToMouse.magnitude;
        Vector3 dir = playerToMouse.normalized;

        Vector3 target = player.position + (magnitude * awayFromPlayer) * dir;
        target.z = initialZ;
        transform.position = target;
    }
}
