using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{

    public Transform target;
    private bool camLocked = true;

    private float camSpeed = 10f;
    private float zoom = 16f;
    private float minZoom = 5f;
    private float maxZoom = 30f;
    private float zoomSpeed = 10f;

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Y))
            camLocked = !camLocked;
        if (!camLocked)
            HandleUnlockedMovement();
        HandleZoom();
    }
    void LateUpdate()
    {
        if (Input.GetKey(KeyCode.Space) || camLocked)
            HandleLockedMovement();
    }
    private void HandleZoom()
    {
        if (Input.mouseScrollDelta.y > 0)
            zoom -= zoomSpeed * Time.deltaTime;
        if (Input.mouseScrollDelta.y < 0)
            zoom += zoomSpeed * Time.deltaTime;
        zoom = Mathf.Clamp(zoom, minZoom, maxZoom);
        transform.position = new Vector3(transform.position.x, zoom, transform.position.z);
    }
    private void HandleLockedMovement()
    {
        transform.position = new Vector3(target.transform.position.x, zoom, target.transform.position.z-2);
    }
    private void HandleUnlockedMovement()
    {
        float xPos = Input.mousePosition.x;
        float yPos = Input.mousePosition.y;
        Vector3 moveVector = Vector3.zero;
        if (xPos >= Screen.width - 10)
        {
            moveVector += Vector3.right;
        }
        else if (xPos <= 10)
        {
            moveVector += Vector3.left;
        }
        if (yPos >= Screen.height - 10)
        {
            moveVector += Vector3.forward;
        }
        else if (yPos <= 10)
        {
            moveVector += Vector3.back;
        }
        transform.position += moveVector * Time.deltaTime * camSpeed;
    }
}
