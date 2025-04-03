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
                    int randAlien = -1;
                    if (false/**is by the moon*/)
                    {
#pragma warning disable CS0162 // Unreachable code detected
                        randAlien = UnityEngine.Random.Range(0, data.PossibleAliensToSpawnMoon.Count);
#pragma warning restore CS0162 // Unreachable code detected
                    }
                    else
                    {
                        randAlien = UnityEngine.Random.Range(0, data.PossibleAliensToSpawn.Count);
                    }
                    if (GameObject.Find(data.PossibleAliensToSpawn[randAlien].name) == null)
                    {
                        Debug.Log("Spawned Aliens");
                        ps.TimeTillCanSpawnAnAlien = UnixTime.GetUnixTime(DateTime.Now.AddMinutes(14));
                        GameObject createdAlien = Instantiate(data.PossibleAliensToSpawn[randAlien],
                            decor.transform.position,
                            decor.transform.rotation, decor.transform);

                        createdAlien.name = data.PossibleAliensToSpawn[randAlien].name;


                        data.AlienAttached = createdAlien;
                        long timeToGo = UnixTime.GetUnixTime(DateTime.Now.AddMinutes(20));
                        Tuple<long, string, string> alien = new Tuple<long, string, string>(timeToGo, decor.name, createdAlien.name);
                        ps.Aliens.Add(alien);
                        AlienData adata = createdAlien.GetComponent<AlienData>();
                        adata.decorAttachedTo = data;
                    }
                }
            }
        }
    }
}
