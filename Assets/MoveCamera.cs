using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveCamera : MonoBehaviour
{
    public Vector3 _screenPosition = new Vector2();
    public Vector3 _screenPositionOrigin = new Vector2();
    public Vector3 positionDifference = new Vector2();

    public Vector2 cameraPositionOrigin = new Vector2();
    public float speed = 100;

    private void Start()
    {
        cameraPositionOrigin = transform.position;
        positionDifference = Vector2.zero;
    }
    private void FixedUpdate()
    {
        touchControls();
    }
    private void touchControls()
    {

        if (Input.GetMouseButton(0))
        {
            _screenPosition = new Vector3(Input.mousePosition.x, Input.mousePosition.y);
            if (Input.GetMouseButtonDown(0))
            {
                _screenPositionOrigin = new Vector3(Input.mousePosition.x, Input.mousePosition.y);
                cameraPositionOrigin = transform.position;
            }
            positionDifference = _screenPositionOrigin - _screenPosition;
        } 
        else if (Input.touchCount > 0)
        {
            Touch touch = Input.GetTouch(0);
            if (touch.phase == TouchPhase.Began)
            {
                _screenPositionOrigin = new Vector3(touch.position.x, touch.position.y, 5);
                cameraPositionOrigin = transform.position;
            }
            _screenPosition = new Vector3(touch.position.x, touch.position.y, 5);
            positionDifference = _screenPositionOrigin - _screenPosition;
        }
        transform.position = new Vector3(Mathf.Clamp(cameraPositionOrigin.x + (positionDifference.x / speed), -6.45f, 0), transform.position.y, transform.position.z);
    }
}
