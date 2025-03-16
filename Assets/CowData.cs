using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CowData : MonoBehaviour
{
    public int risk = 0;
    private PlayerStats ps;
    public int defense = 5;
    // Update is called once per frame
    void Update()
    {
        if (ps == null)
        {
            ps = GameObject.Find("SaveState").GetComponent<PlayerStats>();
        }
        if (risk == 100)
        {
            ps.Cows.Remove(gameObject.name);
            Destroy(gameObject);
        }
    }
}
