using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AlienData : MonoBehaviour
{
    public string alienName;
    public int strength = 20;
    public int wealth = 5;
    public float despawnTime;
    public DecorData decorAttachedTo;
    public long currentTime;
    private PlayerStats ps;
    private Tuple<long, string, string> thisAlien;
    public long TimeToDespawnAt;
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
                for(int i = 0; i < ps.Cows.Count; i++)
                {
                    Tuple<string, int> thisCow = ps.Cows[i];
                    if (thisCow.Item1 == cow.name)
                    {
                        ps.Cows[i] = new Tuple<string, int> (thisCow.Item1, cow.GetComponent<CowData>().risk);
                    }
                }

                // give scrap
                ps.AlienGifts.Add(new Tuple<string, int>(thisAlien.Item3, UnityEngine.Random.Range(1 + wealth, wealth * 2)));
            }
            
            Destroy(gameObject);
        }
    }
}
