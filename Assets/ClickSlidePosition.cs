using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClickSlidePosition : MonoBehaviour
{
    public Vector2 speeds = new Vector2(0, 1);
    

    private Vector3 MousePosition = new Vector2();
    private Vector3 MousePositionOrigin = new Vector2();
    private Vector3 positionDifference = new Vector2();

    private Vector2 fullPositionOrigin = new Vector2();

    private Vector2 myPositionOrigin = new Vector2();
    
    public Vector2 clampHorizontal = new Vector2();
    public Vector2 clampVertical = new Vector2();
    public Transform ClampObject;

    public int AllowedBoxesToSee = 10;
    private void Start()
    {
        fullPositionOrigin = transform.localPosition;
        myPositionOrigin = transform.localPosition;
        positionDifference = Vector2.zero;
    }
    private void Update()
    {
        touchControls();
    }
    private void touchControls()
    {

        if (Input.GetMouseButton(0))
        {
            MousePosition = new Vector3(Input.mousePosition.x, Input.mousePosition.y);
            if (Input.GetMouseButtonDown(0))
            {
                MousePositionOrigin = new Vector3(Input.mousePosition.x, Input.mousePosition.y);
                myPositionOrigin = transform.localPosition;
            }
            positionDifference = MousePositionOrigin - MousePosition;
        }
        else if (Input.touchCount > 0)
        {
            Touch touch = Input.GetTouch(0);
            if (touch.phase == TouchPhase.Began)
            {
                MousePositionOrigin = new Vector3(touch.position.x, touch.position.y, 5);
                myPositionOrigin = transform.localPosition;
            }
            MousePosition = new Vector3(touch.position.x, touch.position.y, 5);
            positionDifference = MousePositionOrigin - MousePosition;
        }
        if (ClampObject != null)
        {
            if (speeds.y != 0)
            {
                clampVertical = new Vector2(fullPositionOrigin.y, ClampObject.localPosition.y);
            }
            if (speeds.x != 0)
            {
                clampHorizontal = new Vector2(fullPositionOrigin.x, ClampObject.localPosition.x);
            }
        }
        transform.localPosition = new Vector3(Mathf.Clamp(myPositionOrigin.x + ((positionDifference.x / 100) * speeds.x), clampHorizontal.x, clampHorizontal.y), 
                                         Mathf.Clamp(myPositionOrigin.y + ((positionDifference.y / 100) * speeds.y), clampVertical.x, clampVertical.y),
                                         transform.position.z);
    }
}
