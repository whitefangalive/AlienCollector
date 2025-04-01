using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClickSlidePosition : MonoBehaviour
{
    public Vector2 speeds = new Vector2(0, 1);
    

    private Vector3 MousePosition = new Vector2();
    private Vector3 MousePositionOrigin = new Vector2();
    private Vector3 positionDifference = new Vector2();

    private Vector2 myPositionOrigin = new Vector2();
    
    public Vector2 clampHorizontal = new Vector2();
    public Vector2 clampVertical = new Vector2();
    public GameObject ClampObject;
    private void Start()
    {
        myPositionOrigin = transform.position;
        positionDifference = Vector2.zero;
        if (ClampObject != null)
        {

        }
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
                myPositionOrigin = transform.position;
            }
            positionDifference = MousePositionOrigin - MousePosition;
        }
        else if (Input.touchCount > 0)
        {
            Touch touch = Input.GetTouch(0);
            if (touch.phase == TouchPhase.Began)
            {
                MousePositionOrigin = new Vector3(touch.position.x, touch.position.y, 5);
                myPositionOrigin = transform.position;
            }
            MousePosition = new Vector3(touch.position.x, touch.position.y, 5);
            positionDifference = MousePositionOrigin - MousePosition;
        }
        transform.position = new Vector3(Mathf.Clamp(myPositionOrigin.x + ((positionDifference.x / 100) * speeds.x), clampHorizontal.x, clampHorizontal.y), 
                                         Mathf.Clamp(myPositionOrigin.y + ((positionDifference.y / 100) * speeds.y), clampVertical.x, clampVertical.y),
                                         transform.position.z);
    }
}
