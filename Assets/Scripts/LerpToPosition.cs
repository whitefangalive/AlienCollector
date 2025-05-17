using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LerpToPosition : MonoBehaviour
{
    public Transform startPostion;
    private Vector3 origin;
    public float speed;
    // Start is called before the first frame update
    void Start()
    {
        origin = new Vector3(transform.position.x, transform.position.y, transform.position.z);
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (startPostion != null)
        {
            transform.position = Vector3.Lerp(transform.position, startPostion.position, speed);
        } else
        {
            transform.position = Vector3.Lerp(transform.position, origin, speed);
        }
    }
}
