using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class AlienSpawner : MonoBehaviour
{
    public PlayerStats ps;

    // Start is called before the first frame update
    void Start()
    {
        ps = GetComponent<PlayerStats>();
    }

    private void resetTimer()
    {
        int numberOfPlacedDecorations = ps.PlacematDecorations.Count;
        ps.TimeTillCanSpawnAnAlien = UnixTime.GetUnixTime(DateTime.Now.AddMinutes(30 
            / (ps.TimeScale * (Mathf.Clamp(numberOfPlacedDecorations * 0.375f, 1, Mathf.Infinity)))));
    }

    public void SpawnAnyAlien(GameObject decor)
    {
        Debug.Log("SpawningAlien");
        if (ps.Cows.Count > 0)
        {
            if (decor != null)
            {
                DecorData data = decor.GetComponent<DecorData>();
                if (data.AlienAttached == null)
                {
                    List<GameObject> FilteredListWithPlanet = new List<GameObject>();

                    PlanetManager pm = GameObject.Find("SaveState").GetComponent<PlanetManager>();
                    for (int i = 0; i < data.PossibleAliensToSpawn.Count; i++)
                    {
                        if (data.PossibleAliensToSpawn[i].GetComponentInChildren<AlienData>().FavoritePlanet == pm.currentPlanet || data.PossibleAliensToSpawn[i].GetComponentInChildren<AlienData>().FavoritePlanet == Planets.Planet.Space)
                        {
                            FilteredListWithPlanet.Add(data.PossibleAliensToSpawn[i]);
                        }
                    }
                    int randAlien = UnityEngine.Random.Range(0, FilteredListWithPlanet.Count);
                        
                    
                    if (GameObject.Find(FilteredListWithPlanet[randAlien].name) == null)
                    {
                        Debug.Log("Spawned Aliens");

                        Invoke("resetTimer", 0.111f);
                        GameObject createdAlien = Instantiate(FilteredListWithPlanet[randAlien],
                            decor.transform.position,
                            decor.transform.rotation, decor.transform);

                        createdAlien.name = FilteredListWithPlanet[randAlien].name;


                        data.AlienAttached = createdAlien;
                        int rand = UnityEngine.Random.Range(10, 30);
                        long timeToGo = UnixTime.GetUnixTime(DateTime.Now.AddMinutes(rand / ps.TimeScale));
                        Tuple<long, string, string> alien = new Tuple<long, string, string>(timeToGo, decor.name, createdAlien.name);
                        ps.Aliens.Add(alien);
                        AlienData adata = createdAlien.GetComponentInChildren<AlienData>();
                        adata.decorAttachedTo = data;
                    }
                }
            }
        }
    }
}
