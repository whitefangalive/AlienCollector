using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class CowSpawner : MonoBehaviour
{
    private PlacementManager placementManager;
    private PlayerStats playerStats;

    private bool once = true;
    // Start is called before the first frame update
    void Start()
    {
        GameObject pmObject = GameObject.Find("PlacementManager");
        if (pmObject != null )
        {
            placementManager = pmObject.GetComponent<PlacementManager>();
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (playerStats == null)
        {
            playerStats = GameObject.Find("SaveState").GetComponent<PlayerStats>();
        } 
        else
        {
            if (once)
            {
                for (int i = 0; i < playerStats.Cows.Count; i++)
                {
                    if (Resources.Load<GameObject>(playerStats.Cows[i].Item1) != null) 
                    {
                        GameObject spawnedDecoration = Instantiate(Resources.Load<GameObject>(playerStats.Cows[i].Item1), transform.position, transform.rotation, transform);
                        spawnedDecoration.name = playerStats.Cows[i].Item1;
                        spawnedDecoration.GetComponent<CowData>().risk = playerStats.Cows[i].Item2;
                    } else
                    {
                        Debug.Log("Could not load in resource: " + playerStats.Cows[i].Item1);
                    }
                    
                }
                once = false;
            }
            if (placementManager != null && placementManager.CurrentlyPlacingCow)
            {
                once = false;
                placementManager.CurrentlyPlacingCow = false;
                GameObject spawnedCow = Instantiate(placementManager.ObjectToPlace, transform.position, transform.rotation, transform);
                spawnedCow.name = placementManager.ObjectToPlace.name;
                playerStats.Cows.Add(new System.Tuple<string, int>(spawnedCow.name, 0));
            }
        }
    }
}
