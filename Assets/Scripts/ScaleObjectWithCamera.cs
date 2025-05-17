using UnityEngine;

public class ScaleObjectWithCamera : MonoBehaviour
{
    public Camera cam;
    private float OrthoGraphicOrigin;
    private Vector3 ObjectLocalScaleOrigin;
    public float slightChange = 1;
    // Start is called before the first frame update
    void Start()
    {
        OrthoGraphicOrigin = cam.orthographicSize;
        ObjectLocalScaleOrigin = transform.localScale;
    }

    // Update is called once per frame
    void Update()
    {
        transform.localScale = ObjectLocalScaleOrigin * (cam.orthographicSize / OrthoGraphicOrigin) * slightChange;
    }
}
