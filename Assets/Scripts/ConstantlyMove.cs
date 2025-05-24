using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class ConstantlyMove : MonoBehaviour
{
    public Vector3 Direction;
    public bool lerping = false;
    private Vector3 mposition;
    private Vector3 desiredPostion;
    public float lerpForce = 1f;
    // Update is called once per frame
    private void Start()
    {
        if (lerping)
        {
            mposition = GetComponent<RectTransform>().position;
            desiredPostion = GetComponent<RectTransform>().position;
        }
    }
    void FixedUpdate()
    {
        if (lerping)
        {
            desiredPostion += (Direction / 60);
            mposition = GetComponent<RectTransform>().position;
            transform.position = Vector3.Lerp(mposition, desiredPostion, lerpForce);
        } else
        {
            transform.position += (Direction / 60);
        }
    }
}
