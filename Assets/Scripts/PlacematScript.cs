using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlacematScript : MonoBehaviour
{
    public GameObject SpawnedDecoration;
    public GameObject AlienAttached;
    private PlacementManager placementManager;
    private Image image;
    private PlayerStats playerStats;
    // Start is called before the first frame update
    void Start()
    {
        
        
        image = GetComponent<Image>();
    }

    // Update is called once per frame
    void Update()
    {
        if (playerStats == null)
        {
            playerStats = GameObject.Find("SaveState").GetComponent<PlayerStats>();
        }
        if (placementManager == null)
        {
            GameObject placeOjb = GameObject.Find("PlacementManager");
            if (placeOjb != null)
            {
                placementManager = placeOjb.GetComponent<PlacementManager>();
            }
        }
        if (placementManager != null)
        {
            if (placementManager.CurrentlyPlacing)
            {
                image.color = new Color(image.color.r, image.color.g, image.color.b, 50);
            }
            else
            {
                image.color = new Color(image.color.r, image.color.g, image.color.b, 0);
            }
        } else
        {
            image.color = new Color(image.color.r, image.color.g, image.color.b, 0);
        }
        //when the object exists in playerstats but is not spawned, then spawn it
        if (playerStats.PlacematDecorations.ContainsKey(transform.gameObject.name))
        {
            string ResourceName = playerStats.PlacematDecorations[transform.gameObject.name];
            if (ResourceName != null && SpawnedDecoration == null && Resources.Load<GameObject>("Decor/" + ResourceName) != null)
            {
                SpawnedDecoration = Instantiate(Resources.Load<GameObject>("Decor/" + ResourceName), transform.position, transform.rotation, transform);
                SpawnedDecoration.name = ResourceName;
            }
            //needs to be done after loading in placemat decor
            LoadInScene loader = GameObject.Find("LoadIn").GetComponent<LoadInScene>();
            if (loader.needsSpawnAlien)
            {
                loader.spawner.SpawnAnyAlien();
                loader.needsSpawnAlien = false;
            }
        }
    }

    public void placeHere()
    {
        if (placementManager != null && placementManager.CurrentlyPlacing)
        {
            
            //detach current alien on this placemat
            if (playerStats.PlacematDecorations.ContainsKey(transform.gameObject.name))
            {
                playerStats.PlacematDecorations[transform.gameObject.name] = placementManager.ObjectToPlace.name;
            } else
            {
                playerStats.PlacematDecorations.Add(transform.gameObject.name, placementManager.ObjectToPlace.name);
            }
            
            string ResourceName = playerStats.PlacematDecorations[transform.gameObject.name];
            if (SpawnedDecoration != null)
            {
                Destroy(SpawnedDecoration);
            }
            SpawnedDecoration = Instantiate(Resources.Load<GameObject>("Decor/" + ResourceName), transform.position, transform.rotation, transform);
            placementManager.ObjectToPlace = null;
            placementManager.CurrentlyPlacing = false;
        }
        
    }
}
