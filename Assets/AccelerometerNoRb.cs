using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AccelerometerNoRb : MonoBehaviour
{
    public float multiplier = 1;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (PlayerPrefs.GetInt("accelerometer") != 0)
        {
            Debug.DrawRay(transform.position, new Vector3(Input.acceleration.x, Input.acceleration.y, 0), Color.red);
            transform.position = transform.position + (new Vector3(Input.acceleration.x, Input.acceleration.y, 0) * multiplier);
        }
    }
}
