using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class Accelerometer : MonoBehaviour
{
    private Rigidbody2D rb;
    public float multiplier = 1;
    public PhysicsMaterial2D bouncyMaterial;
    public PhysicsMaterial2D normalMaterial;
    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();

        if (bouncyMaterial != null)
        {
            if (PlayerPrefs.GetInt("accelerometer") != 0 && rb.sharedMaterial == bouncyMaterial)
            {
                rb.sharedMaterial = normalMaterial;
                if (GetComponent<Collider2D>() != null)
                {
                    GetComponent<Collider2D>().sharedMaterial = normalMaterial;
                }
            }
            else
            {
                rb.sharedMaterial = bouncyMaterial;
                if (GetComponent<Collider2D>() != null)
                {
                    GetComponent<Collider2D>().sharedMaterial = bouncyMaterial;
                }
            }
        }
        
    }

    // Update is called once per frame
    void Update()
    {
        if (PlayerPrefs.GetInt("accelerometer") != 0)
        {
            Debug.DrawRay(transform.position, new Vector3(Input.acceleration.x, Input.acceleration.y, 0), Color.red);
            rb.AddForce(new Vector3(Input.acceleration.x, Input.acceleration.y, 0) * multiplier, ForceMode2D.Impulse);
        }
        
    }
}
