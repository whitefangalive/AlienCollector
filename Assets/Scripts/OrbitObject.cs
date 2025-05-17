using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(RectTransform))]
public class OrbitObject : MonoBehaviour
{
    public GameObject objectToOrbit;
    public float speed;
    public float lerpAmount = 100;
    private float distance;
    private float theta;
    private Vector3 mposition;
    private Vector3 oposition;

    public bool savePosition = true;
    private PlayerStats ps;
    private string objectName;
    // Start is called before the first frame update
    void Start()
    {
        distance = 0.5f;
        if (objectToOrbit != null)
        {
            mposition = GetComponent<RectTransform>().position;
            oposition = objectToOrbit.GetComponent<RectTransform>().position;
            distance = Vector3.Distance(mposition, oposition);
            theta = Mathf.Deg2Rad * Vector3.Angle(oposition, mposition);
        }
        if (ps == null)
        {
            ps = GameObject.Find("SaveState").GetComponent<PlayerStats>();
        }
        objectName = gameObject.name;
        
        if (savePosition)
        {
            if (!ps.PlanetTheta.ContainsKey(objectName))
            {
                ps.PlanetTheta.Add(objectName, theta);
            }
        }
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (ps == null)
        {
            ps = GameObject.Find("SaveState").GetComponent<PlayerStats>();
        }
        if (objectToOrbit != null)
        {
            mposition = GetComponent<RectTransform>().position;
            oposition = objectToOrbit.GetComponent<RectTransform>().position;
            if (savePosition && ps.PlanetTheta.ContainsKey(objectName))
            {
                ps.PlanetTheta[objectName] += ((3.14f * 4) / 100) / speed;
                transform.position = Vector3.Lerp(mposition, new Vector3(oposition.x + distance * Mathf.Cos(ps.PlanetTheta[objectName]), oposition.y + distance * Mathf.Sin(ps.PlanetTheta[objectName]), transform.position.z), lerpAmount);
            } else
            {
                theta += ((3.14f * 4) / 100) / speed;
                transform.position = Vector3.Lerp(mposition, new Vector3(oposition.x + distance * Mathf.Cos(theta), oposition.y + distance * Mathf.Sin(theta), transform.position.z), lerpAmount);
            }
        }
    }
}
