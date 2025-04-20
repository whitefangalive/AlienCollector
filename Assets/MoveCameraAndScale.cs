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
    private void Start()
    {
        cameraPositionOrigin = transform.position;
        positionDifference = Vector2.zero;
    }
    private void Update()
    {
        touchControls();
    }
    private void touchControls()
    {
        // CHANGE THIS BEFORE MOBILE RELEASE
        
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
            transform.position = new Vector3(Mathf.Clamp(cameraPositionOrigin.x + (positionDifference.x * speed.x), clamp.position.x, 0), Mathf.Clamp(cameraPositionOrigin.y + (positionDifference.y * speed.y), clamp.position.y, 0), transform.position.z);
        }
        
    }
}
