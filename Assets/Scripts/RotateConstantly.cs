using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class RotateConstantly : MonoBehaviour
{
    public Vector3 Speeds = new Vector3(); // rotation speeds in degrees per second

    void FixedUpdate()
    {
        // How much we should rotate this frame
        Quaternion deltaRotation = Quaternion.Euler(Speeds * Time.fixedDeltaTime);

        // Apply the rotation properly
        transform.rotation = transform.rotation * deltaRotation;
    }
}
