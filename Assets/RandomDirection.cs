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
        float rand = Random.Range(-2, 1);
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
        vel = rb.velocity;
        if (Mathf.Abs(rb.velocity.x) < 0.1)
        {
            rb.velocity = new Vector2(Random.Range(1, 3), rb.velocity.y);
        } 
        else if (Mathf.Abs(rb.velocity.y) < 0.1)
        {
            rb.velocity = new Vector2(rb.velocity.x, Random.Range(1, 3));
        }
    }
}
