using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    [SerializeField] Camera cam;
    Vector2 previousMousePos;

    private void Update()
    {
        if (!TimeManager.instance.absoluteRunning) return;
        if (Input.GetMouseButtonDown(2))
        {
            previousMousePos = cam.ScreenToWorldPoint(Input.mousePosition);
        }
        else if (Input.GetMouseButton(2))
        {
            Vector2 currentMousePos = cam.ScreenToWorldPoint(Input.mousePosition);
            Vector2 diff = previousMousePos - currentMousePos;

            cam.transform.position += (Vector3)diff;

            previousMousePos = cam.ScreenToWorldPoint(Input.mousePosition);
        }
        cam.orthographicSize = Mathf.Clamp(cam.orthographicSize - Input.mouseScrollDelta.y, 5, 40);
    }
}
