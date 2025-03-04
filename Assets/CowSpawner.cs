using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CowSpawner : MonoBehaviour
{
    public PlacementManager placementManager;
    private PlayerStats playerStats;
    // Start is called before the first frame update
    void Start()
    {
        GameObject pmObject = GameObject.Find("PlacementManager");
        if (pmObject != null )
        {
            placementManager = pmObject.GetComponent<PlacementManager>();
        }

        for (int i = 0; i < playerStats.Cows.Count; i++)
        {
            GameObject spawnedDecoration = Instantiate(Resources.Load<GameObject>(playerStats.Cows[i]), transform.position, transform.rotation, transform);
            spawnedDecoration.name = placementManager.ObjectToPlace.name;
        }


    }

    // Update is called once per frame
    void Update()
    {
        if (playerStats == null)
        {
            playerStats = GameObject.Find("SaveState").GetComponent<PlayerStats>();
        }

        if (placementManager != null && placementManager.CurrentlyPlacingCow)
        {
            GameObject spawnedDecoration = Instantiate(placementManager.ObjectToPlace, transform.position, transform.rotation, transform);
            spawnedDecoration.name = placementManager.ObjectToPlace.name;
            playerStats.Cows.Add(spawnedDecoration.name);
            placementManager.CurrentlyPlacingCow = false;
        } else
        {


        }
    }
}
