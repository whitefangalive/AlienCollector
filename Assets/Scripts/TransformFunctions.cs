using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TransformFunctions : MonoBehaviour
{
    public void MoveToLocation(Transform pos)
    {
        transform.position = new Vector3(pos.position.x, pos.position.y, transform.position.z);
    }
}
