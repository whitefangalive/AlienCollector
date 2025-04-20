using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlanetManager : MonoBehaviour
{
    public List<Tuple<Planets.Planet, long>> PlanetsAndEta = new List<Tuple<Planets.Planet, long>>();
    private PlayerStats ps;
    public Planets.Planet currentPlanet;
    public Tuple<Planets.Planet, long> nextPlanet;
    public long gameStartTime;
    // Start is called before the first frame update
    void Start()
    {
        PlanetsAndEta.Add(new Tuple<Planets.Planet, long>(Planets.Planet.Earth, 0));
        PlanetsAndEta.Add(new Tuple<Planets.Planet, long>(Planets.Planet.Moon, UnixTime.GetUnixTimeMinutes(4320)));
        PlanetsAndEta.Add(new Tuple<Planets.Planet, long>(Planets.Planet.Mars, UnixTime.GetUnixTimeMinutes(9320)));
    }

    private void Update()
    {
        if (ps == null)
        {
            ps = GameObject.Find("SaveState").GetComponent<PlayerStats>();
        }
        gameStartTime = ps.GameStartTime;
        currentPlanet = Planets.Planet.Space;
        for (int i = 0; i < PlanetsAndEta.Count; i++)
        {
            Tuple<Planets.Planet, long> tuple = PlanetsAndEta[i];
            if ((gameStartTime + tuple.Item2) - UnixTime.GetUnixTime(DateTime.Now) < 700)
            {
                currentPlanet = tuple.Item1;
                if (PlanetsAndEta.Count > i+1)
                {
                    nextPlanet = PlanetsAndEta[i + 1];
                } else
                {
                    nextPlanet = new Tuple<Planets.Planet, long>(Planets.Planet.Space, 0);
                }
            }
        }
    }

    public DateTime getEta(long offset)
    {
        return UnixTime.GetDateTime(gameStartTime + offset);
    }

}
