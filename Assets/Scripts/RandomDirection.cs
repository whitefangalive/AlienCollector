using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RandomDirection : MonoBehaviour
{
    private Rigidbody2D rb;
    public int currentCount = 0;
    public float speed = 0.1f;
    public Vector2 vel = new Vector2();
    public Vector3 tapperVelocity = new Vector3();
    public float velocityMultipler = 10;

    private Vector3 lastScreenPos;
    private float lastSampleTime;
    public Vector3 screenVelocity;
    private Vector3 worldVelocity;
    private Vector3 lastVelocityThatWasntZero = Vector3.zero;


    private Camera cam;

    private void go ()
    {
        float rand = Random.Range(-2.0f, 1.0f);
        rb.AddForce(new Vector2(100 * rand, -15));
    }
    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        Invoke("go", 1);
        cam = Camera.main;
    }

    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            lastScreenPos = Input.mousePosition;
            lastSampleTime = Time.time;
            screenVelocity = Vector3.zero;
        }

        if (Input.GetMouseButton(0))
        {
            Vector3 currentPos = Input.mousePosition;
            float now = Time.time;
            float dt = now - lastSampleTime;

            if (dt > Mathf.Epsilon)
            {
                Vector3 dp = currentPos - lastScreenPos;
                screenVelocity = dp / dt;
            }

            lastScreenPos = currentPos;
            lastSampleTime = now;
        }
    }

    
    public void SetCowVeloityToTapper()
    {
        rb.AddForce(tapperVelocity);
    }

    public void LaunchCow()
    {
        float worldZ = cam.WorldToScreenPoint(transform.position).z;

        //    convert a delta‐vector of (screenVelocity.x, screenVelocity.y, worldZ)
        //    back into world‐space displacement per second:
        Vector3 worldPointCurrent = cam.ScreenToWorldPoint(new Vector3(
            lastScreenPos.x + screenVelocity.x * Time.deltaTime,
            lastScreenPos.y + screenVelocity.y * Time.deltaTime,
            worldZ
        ));
        Vector3 worldPointLast = cam.ScreenToWorldPoint(new Vector3(
            lastScreenPos.x,
            lastScreenPos.y,
            worldZ
        ));

        worldVelocity = (worldPointCurrent - worldPointLast) / Time.deltaTime;
        if (worldVelocity != Vector3.zero)
        {
            lastVelocityThatWasntZero = worldVelocity;
        }

        rb.velocity = lastVelocityThatWasntZero * velocityMultipler;


        // or, for an impulse:
        // rb.AddForce(lastVelocityThatWasntZero * velocityMultiplier, ForceMode.VelocityChange);
    }
}
