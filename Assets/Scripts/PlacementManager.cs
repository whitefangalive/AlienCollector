using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlacementManager : MonoBehaviour
{
    public bool CurrentlyPlacing = false;
    public GameObject ObjectToPlace;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void touchControls()
    {
        if (Input.touchCount > 0)
        {
            Touch touch = Input.GetTouch(0);

            if (touch.phase == TouchPhase.Began)
            {
                transform.position = Camera.main.ScreenToWorldPoint(new Vector3(touch.position.x, touch.position.y, 5));
            }
        }
    }
}
