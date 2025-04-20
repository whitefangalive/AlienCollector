using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlanetScalePeak : MonoBehaviour
{
    public int peakTime = 4320;
    private Vector3 originalScale;
    private PlayerStats ps;
    public float scaleAmount = 0.01f;
    public float totalMinutes;
    // Start is called before the first frame update
    void Start()
    {
        originalScale = transform.localScale;
    }

    // Update is called once per frame
    void Update()
    {
        if (ps == null)
        {
            ps = GameObject.Find("SaveState").GetComponent<PlayerStats>();
        }
        DateTime eta = getEta(UnixTime.GetUnixTimeMinutes(peakTime));
        TimeSpan Difference = eta - DateTime.Now;
        totalMinutes = (float)Difference.TotalMinutes;
        float multiplier = Mathf.Clamp(1 - Mathf.Abs((float)Difference.TotalMinutes * scaleAmount), 0, Mathf.Infinity);
        transform.localScale = new Vector3(originalScale.x * multiplier, originalScale.y * multiplier, originalScale.z);
    }
    public DateTime getEta(long offset)
    {
        return UnixTime.GetDateTime(ps.GameStartTime + offset);
    }
}
