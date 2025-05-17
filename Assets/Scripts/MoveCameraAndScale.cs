using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveCameraAndScale : MonoBehaviour
{
    public Vector3 MousePosition = new Vector2();
    public Vector3 MousePositionOrigin = new Vector2();
    public Vector3 positionDifference = new Vector2();

    public Vector2 cameraPositionOrigin = new Vector2();
    public Vector2 speed = new Vector2(0.01f, 0.01f);
    public Transform clamp;
    public DetectPressing pressed;

    private float initialTouchDistance;
    private float initialCameraSize;

    public float minSize = 1f;
    public float maxSize = 40f;
    
    private void Start()
    {
        cameraPositionOrigin = transform.position;
        positionDifference = Vector2.zero;
    }
    private void Update()
    {
        touchControls();
        pinchZoom();
    }
    private void touchControls()
    {
        
        if (Input.GetMouseButton(0))
        {
            MousePosition = new Vector3(Input.mousePosition.x, Input.mousePosition.y);
            if (Input.GetMouseButtonDown(0))
            {
                MousePositionOrigin = new Vector3(Input.mousePosition.x, Input.mousePosition.y);
                cameraPositionOrigin = transform.position;
            }
            positionDifference = MousePositionOrigin - MousePosition;
        } 
        else if (Input.touchCount > 0)
        {
            Touch touch = Input.GetTouch(0);
            if (touch.phase == TouchPhase.Began)
            {
                MousePositionOrigin = new Vector3(touch.position.x, touch.position.y, 5);
                cameraPositionOrigin = transform.position;
            }
            MousePosition = new Vector3(touch.position.x, touch.position.y, 5);
            positionDifference = MousePositionOrigin - MousePosition;
        }
        if (pressed.ButtonpressedRecently)
        {
            transform.position = new Vector3(Mathf.Clamp(cameraPositionOrigin.x + (positionDifference.x * speed.x), clamp.position.x, -clamp.position.x), Mathf.Clamp(cameraPositionOrigin.y + (positionDifference.y * speed.y), clamp.position.y, -clamp.position.y), transform.position.z);
        }
        
    }

    private void pinchZoom()
    {
        if (Input.touchCount == 2)
        {
            Touch touchZero = Input.GetTouch(0);
            Touch touchOne = Input.GetTouch(1);

            if (touchOne.phase == TouchPhase.Began)
            {
                initialTouchDistance = Vector2.Distance(touchZero.position, touchOne.position);
                initialCameraSize = GetComponent<Camera>().orthographicSize;
            }
            else if (touchZero.phase == TouchPhase.Moved || touchOne.phase == TouchPhase.Moved)
            {
                float currentTouchDistance = Vector2.Distance(touchZero.position, touchOne.position);
                float distanceDifference = initialTouchDistance - currentTouchDistance;

                Camera cam = GetComponent<Camera>();
                cam.orthographicSize = Mathf.Clamp(initialCameraSize + distanceDifference * 0.01f, minSize, maxSize);
            }
        }
    }
}
