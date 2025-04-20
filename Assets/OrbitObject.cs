using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OrbitObject : MonoBehaviour
{
    public GameObject objectToOrbit;
    public float speed;
    public float lerpAmount = 100;
    private float distance;
    private float theta;
    private Vector3 mposition;
    private Vector3 oposition;
    // Start is called before the first frame update
    void Start()
    {
        distance = 0.5f;
        if (objectToOrbit != null)
        {
            mposition = GetComponent<RectTransform>().position;
            oposition = objectToOrbit.GetComponent<RectTransform>().position;
            distance = Vector3.Distance(mposition, oposition);
            theta = Vector3.Angle(oposition, mposition);
        }
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (objectToOrbit != null)
        {
            mposition = GetComponent<RectTransform>().position;
            oposition = objectToOrbit.GetComponent<RectTransform>().position;
            theta += speed;
            transform.position = Vector3.Lerp(mposition, new Vector3(oposition.x + distance * Mathf.Cos(theta), oposition.y + distance * Mathf.Sin(theta), transform.position.z), lerpAmount);
        }
    }
}
