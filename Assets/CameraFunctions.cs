using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFunctions : MonoBehaviour
{
    private Camera _camera;
    private float originalSize;
    public float scaleAmount = 8f;
    // Start is called before the first frame update
    void Start()
    {
        _camera = GetComponent<Camera>();
        originalSize = _camera.orthographicSize;
    }

    public void SetCamearaSize(int size)
    {
        _camera.orthographicSize = size;
    }
    public void ResetToOriginalSize()
    {
        _camera.orthographicSize = originalSize;
    }
    public void ScaleCameraSizeByObject(Transform obj)
    {
        _camera.orthographicSize = originalSize * (obj.lossyScale.x * scaleAmount);
    }
}
