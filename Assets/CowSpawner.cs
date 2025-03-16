using System.Collections;
using System.Collections.Generic;
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
                    GameObject spawnedDecoration = Instantiate(Resources.Load<GameObject>(playerStats.Cows[i]), transform.position, transform.rotation, transform);
                    spawnedDecoration.name = playerStats.Cows[i];
                    
                }
                once = false;
            }
            if (placementManager != null && placementManager.CurrentlyPlacingCow)
            {
                GameObject spawnedCow = Instantiate(placementManager.ObjectToPlace, transform.position, transform.rotation, transform);
                spawnedCow.name = placementManager.ObjectToPlace.name;
                playerStats.Cows.Add(spawnedCow.name);
                once = false;
                placementManager.CurrentlyPlacingCow = false;
            }
        }
    }
}
