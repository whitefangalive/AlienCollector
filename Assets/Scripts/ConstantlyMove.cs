using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class ConstantlyMove : MonoBehaviour
{
    public Vector3 Direction;

    // Update is called once per frame
    void FixedUpdate()
    {
        transform.position += (Direction / 60);
    }
}
