using System;
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
                //check for broken cows
                if (playerStats.Cows.Count > 0 && playerStats.Cows[0] == null)
                {
                    playerStats.Cows.Clear();
                }
                for (int i = 0; i < playerStats.Cows.Count; i++)
                {
                    if (playerStats.Cows[i] != null && 
                        Resources.Load<GameObject>(playerStats.Cows[i].Item1) != null) 
                    {
                        GameObject spawnedDecoration = Instantiate(Resources.Load<GameObject>(playerStats.Cows[i].Item1), transform.position, transform.rotation, transform);
                        spawnedDecoration.name = playerStats.Cows[i].Item1;
                        spawnedDecoration.GetComponent<CowData>().risk = playerStats.Cows[i].Item2;
                        var aType = playerStats.Cows[i].GetType();
                        var numberOfGenericParameters = aType.GetGenericArguments().Length;
                        if (playerStats.Cows[i].Item3 != null)
                        {
                            spawnedDecoration.GetComponent<CowData>().id = playerStats.Cows[i].Item3;
                        } else
                        {
                            //assign id
                            spawnedDecoration.GetComponent<CowData>().id = spawnedDecoration.GetInstanceID().ToString();
                        }
                        
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
                string id = spawnedCow.GetInstanceID().ToString();
                playerStats.Cows.Add(new Tuple<string, int, string>(spawnedCow.name, 0, id));
                spawnedCow.GetComponent<CowData>().id = id;
            }
        }
    }
}
