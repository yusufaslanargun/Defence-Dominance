using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    // ========================================= //
    [Header("Utility Values")]

    [Tooltip("Camera Speed")]
    [SerializeField]
    float panSpeed = 30f;

    [Tooltip("Scroll Speed")]
    [SerializeField]
    float scrollSpeed = 5f;
    // ========================================= //

    bool doMovement = true;
    readonly float minY = 10f;
    readonly float maxY = 80f;
    readonly float minX = -2f;
    readonly float maxX = 78f;
    readonly float minZ = -70f;
    readonly float maxZ = 10f;


    void Update()
    {
        if (!MovementLock())
        {
            return;
        }
        CameraMove();
    }

    bool MovementLock()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            doMovement = !doMovement;
        }

        return doMovement;
    }

    void CameraMove()
    {
        if (!doMovement)
        {
            return;
        }

        if (Input.GetKey(KeyCode.W))
        {
            transform.Translate(panSpeed * Time.deltaTime * Vector3.forward, Space.World);
        }
        if (Input.GetKey(KeyCode.S))
        {
            transform.Translate(panSpeed * Time.deltaTime * Vector3.back, Space.World);
        }
        if (Input.GetKey(KeyCode.D))
        {
            transform.Translate(panSpeed * Time.deltaTime * Vector3.right, Space.World);
        }
        if (Input.GetKey(KeyCode.A))
        {
            transform.Translate(panSpeed * Time.deltaTime * Vector3.left, Space.World);
        }

        float scroll = Input.GetAxis("Mouse ScrollWheel");

        Vector3 pos = transform.position;

        pos.y -= scroll * scrollSpeed * Time.deltaTime * 1000;
        pos.y = Mathf.Clamp(pos.y, minY, maxY);
        pos.x = Mathf.Clamp(pos.x, minX, maxX);
        pos.z = Mathf.Clamp(pos.z, minZ, maxZ);

        transform.position = pos;
    }
}
