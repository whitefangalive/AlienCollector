using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class AlienSpawner : MonoBehaviour
{
    public PlayerStats ps;
    public float currentTime;
    private GameObject createdAlien;
    // Start is called before the first frame update
    void Start()
    {
        ps = GetComponent<PlayerStats>();
    }

    // Update is called once per frame
    void Update()
    {
        currentTime = DateTime.Now.Ticks;
        //DespawnDeadAliens();
    }

    private void DespawnDeadAliens()
    {
        foreach (Tuple<float, string, string> alien in ps.Aliens)
        {
            if (alien.Item1 >= currentTime)
            {
                Destroy(createdAlien);
            }
        }
    }
    public void SpawnAnyAlien()
    {
        
        if (ps.Cows.Count > 0)
        {
            if (ps.PlacematDecorations.Count > 0)
            {
                Debug.Log("Spawned Aliens");
                int rand = UnityEngine.Random.Range(0, ps.PlacematDecorations.Count);
                GameObject decor = GameObject.Find(ps.PlacematDecorations.ElementAt(rand).Value);
                
                if (decor != null)
                {
                    DecorData data = decor.GetComponent<DecorData>();
                    if (false/**is by the moon*/)
                    {

                    }
                    else
                    {
                        int randAlien = UnityEngine.Random.Range(0, data.PossibleAliensToSpawn.Count);

                        createdAlien = Instantiate(data.PossibleAliensToSpawn[randAlien],
                            decor.transform.position,
                            decor.transform.rotation, decor.transform);

                        createdAlien.name = data.PossibleAliensToSpawn[randAlien].name;
                        string scientificNotationString = "3.6e+12";
                        float floatValue = float.Parse(scientificNotationString);

                        Tuple<float, string, string> alien = new Tuple<float, string, string>(DateTime.Now.Ticks +
                            floatValue + UnityEngine.Random.Range(-10000000, 10000000), decor.name, createdAlien.name);
                        ps.Aliens.Add(alien);
                    }
                } else
                {
                    Debug.Log("Unable to find " + ps.PlacematDecorations.ElementAt(rand).Value);
                }
            }
        }
    }
}
