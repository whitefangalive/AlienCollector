using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AlienData : MonoBehaviour
{
    public int strength = 20;
    public float despawnTime;
    public DecorData decorAttachedTo;
    public long currentTime;
    private PlayerStats ps;
    private Tuple<long, string, string> thisAlien;
    [HideInInspector]public long TimeToDespawnAt;
    // Start is called before the first frame update
    void Start()
    {
        ps = GameObject.Find("SaveState").GetComponent<PlayerStats>();
        foreach (Tuple<long, string, string> alien in ps.Aliens)
        {
            if (alien.Item3 == gameObject.name)
            {
                thisAlien = alien;
            }
        }
    }
    // Update is called once per frame
    void Update()
    {
        currentTime = UnixTime.GetUnixTime(DateTime.Now);
        DespawnDeadAliens();
    }
    private void DespawnDeadAliens()
    {
        TimeToDespawnAt = thisAlien.Item1;
        despawnTime = TimeToDespawnAt - currentTime;
        if (despawnTime < 0)
        {
            decorAttachedTo.AlienAttached = null;
            ps.Aliens.Remove(thisAlien);
            GameObject cow = GameObject.FindGameObjectWithTag("Cow");
            if (cow != null)
            {
                cow.GetComponent<CowData>().risk += (strength - cow.GetComponent<CowData>().defense);
            }
            
            Destroy(gameObject);
        }
    }
}
