using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowObject : MonoBehaviour
{
    public Transform ObjectToFollow;
    // Update is called once per frame
    void Update()
    {
        transform.position = ObjectToFollow.position;
    }
}
