using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RandomDirection : MonoBehaviour
{
    private Rigidbody2D rb;
    public int currentCount = 0;
    public float speed = 0.1f;
    public Vector2 vel = new Vector2();
    private void go ()
    {
        float rand = Random.Range(-2.0f, 1.0f);
        rb.AddForce(new Vector2(100 * rand, -15));
    }
    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        Invoke("go", 2);
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        rb.velocity = new Vector3(Mathf.Clamp(rb.velocity.x, -1.5f, 1.5f), Mathf.Clamp(rb.velocity.y, -1.5f, 1.5f), 0);
        vel = rb.velocity;
        if (Mathf.Abs(rb.velocity.x) < 0.1)
        {
            rb.velocity = new Vector2(Random.Range(1.2f, 1.3f), rb.velocity.y);
        } 
        else if (Mathf.Abs(rb.velocity.y) < 0.1)
        {
            rb.velocity = new Vector2(rb.velocity.x, Random.Range(1.2f, 1.3f));
        }
    }
}
