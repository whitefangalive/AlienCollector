using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlacematScript : MonoBehaviour
{
    public GameObject SpawnedDecoration;
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
        //Spawn in yellow mats
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

                RespawnAlien();

                //needs to be done after loading in placemat decor
                LoadInScene loader = GameObject.Find("LoadIn").GetComponent<LoadInScene>();
                int rand = UnityEngine.Random.Range(0, 2);
                if (UnixTime.GetUnixTime(DateTime.Now) > playerStats.TimeTillCanSpawnAnAlien)
                {
                    if (SpawnedDecoration != null && rand == 1)
                    {
                        Debug.Log("AlienTime");
                        if (loader.spawner != null)
                        {
                            loader.spawner.SpawnAnyAlien(SpawnedDecoration);
                        }
                    }
                } else
                {
                    Debug.Log("Time Before Can spawn next alien: " + (playerStats.TimeTillCanSpawnAnAlien - UnixTime.GetUnixTime(DateTime.Now)).ToString());
                }
                
            }
        }
    }

    //when the alien exists on one of the decorations but isn't spawned on it
    private void RespawnAlien()
    {
        if (SpawnedDecoration != null)
        {
            DecorData data = SpawnedDecoration.GetComponent<DecorData>();
            if (data.AlienAttached == null)
            {
                foreach (Tuple<long, string, string> alien in playerStats.Aliens)
                {
                    if (alien.Item2 == SpawnedDecoration.name)
                    {
                        GameObject alienObject = Resources.Load<GameObject>("Aliens/" + alien.Item3);
                        data.AlienAttached = Instantiate(alienObject,
                                SpawnedDecoration.transform.position,
                                SpawnedDecoration.transform.rotation, SpawnedDecoration.transform);

                        data.AlienAttached.name = alienObject.name;
                        data.AlienAttached.GetComponent<AlienData>().decorAttachedTo = data;
                    }
                }
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
            SpawnedDecoration.name = ResourceName;
            placementManager.ObjectToPlace = null;
            placementManager.CurrentlyPlacing = false;
        }
        
    }
}
