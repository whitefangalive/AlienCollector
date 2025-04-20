using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveCamera : MonoBehaviour
{
    public Vector3 MousePosition = new Vector2();
    public Vector3 MousePositionOrigin = new Vector2();
    public Vector3 positionDifference = new Vector2();

    public Vector2 cameraPositionOrigin = new Vector2();
    public float speed = 100;

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
        transform.position = new Vector3(Mathf.Clamp(cameraPositionOrigin.x + (positionDifference.x / speed), -6.7f, 0.22f), transform.position.y, transform.position.z);
    }
}
