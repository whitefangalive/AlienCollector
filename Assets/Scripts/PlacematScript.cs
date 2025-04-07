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
    private PlayerStats ps;
    private bool once = true;
    // Start is called before the first frame update
    void Start()
    {
        image = GetComponent<Image>();
        
    }

    // Update is called once per frame
    void Update()
    {
        if (ps == null)
        {
            ps = GameObject.Find("SaveState").GetComponent<PlayerStats>();
            

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
        if (ps.PlacematDecorations.ContainsKey(transform.gameObject.name))
        {
            string ResourceName = ps.PlacematDecorations[transform.gameObject.name];
            if (ResourceName != null && SpawnedDecoration == null && Resources.Load<GameObject>("Decor/" + ResourceName) != null)
            {
                Debug.Log("Respawning prop: " + ResourceName);
                SpawnedDecoration = Instantiate(Resources.Load<GameObject>("Decor/" + ResourceName), transform.position, transform.rotation, transform);
                SpawnedDecoration.name = ResourceName;

                RespawnAlien();

                //needs to be done after loading in placemat decor
                LoadInScene loader = GameObject.Find("LoadIn").GetComponent<LoadInScene>();
                int rand = UnityEngine.Random.Range(0, 2);
                if (UnixTime.GetUnixTime(DateTime.Now) > ps.TimeTillCanSpawnAnAlien)
                {
                    if (SpawnedDecoration != null && rand == 1)
                    {
                        Debug.Log("AlienTime");
                        if (loader.spawner != null)
                        {
                            Debug.Log(loader.spawner != null);
                            loader.spawner.SpawnAnyAlien(SpawnedDecoration);
                        }
                    }
                } else
                {
                    Debug.Log("Time Before Can spawn next alien: " + (ps.TimeTillCanSpawnAnAlien - UnixTime.GetUnixTime(DateTime.Now)).ToString());
                }
                
            }
        }
        if (ps.TimeLeftGame > 0 && once)
        {
            SpawnOutOfFocus();
            once = false;
        }
        

    }

    private void SpawnOutOfFocus()
    {
        Debug.Log("Time left game: " + UnixTime.GetDateTime(ps.TimeLeftGame) +
            ", Seconds passed " + (UnixTime.GetUnixTime(DateTime.Now) - ps.TimeLeftGame).ToString() +
            " Time since last logged on " + (UnixTime.GetUnixTime(DateTime.Now) - ps.TimeLeftGame) / UnixTime.GetUnixTimeMinutes(1) +
            " minutes, Unix 1 min = " + UnixTime.GetUnixTimeMinutes(1));
        int rand = UnityEngine.Random.Range(0, 2);
        if (SpawnedDecoration != null && rand == 1)
        {
            //spawn aliens if gone for more then 20 minutes, spawn one for each 20
            for (int i = 0; i < (UnixTime.GetUnixTime(DateTime.Now) - ps.TimeLeftGame) / UnixTime.GetUnixTimeMinutes(20); ++i)
            {
                if (ps.Cows.Count > 0)
                {
                    GameObject cow = null;
                    foreach (GameObject co in GameObject.FindGameObjectsWithTag("Cow"))
                    {
                        if (co.GetComponent<CowData>().risk < 100)
                        {
                            cow = co;
                        }
                    }
                    if (cow != null)
                    {
                        DecorData data = SpawnedDecoration.GetComponent<DecorData>();
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

                            AlienData alienData = data.PossibleAliensToSpawn[randAlien].GetComponent<AlienData>();


                            if (cow != null)
                            {
                                cow.GetComponent<CowData>().risk += (30 - cow.GetComponent<CowData>().defense);
                                for (int j = 0; j < ps.Cows.Count; j++)
                                {
                                    Tuple<string, int> thisCow = ps.Cows[j];
                                    if (thisCow.Item1 == cow.name)
                                    {
                                        ps.Cows[j] = new Tuple<string, int>(thisCow.Item1, cow.GetComponent<CowData>().risk);
                                    }
                                }
                            }
                            ps.AlienGifts.Add(new Tuple<string, int>(alienData.gameObject.name, UnityEngine.Random.Range(1 + alienData.wealth, alienData.wealth * 2)));
                        }
                    }
                    else
                    {
                        break;
                    }
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
                foreach (Tuple<long, string, string> alien in ps.Aliens)
                {
                    if (alien.Item2 == SpawnedDecoration.name)
                    {
                        GameObject alienObject = Resources.Load<GameObject>("Aliens/" + alien.Item3);
                        data.AlienAttached = Instantiate(alienObject,
                                SpawnedDecoration.transform.position,
                                SpawnedDecoration.transform.rotation, SpawnedDecoration.transform);

                        data.AlienAttached.name = alienObject.name;
                        if (data.AlienAttached.GetComponentInChildren<AlienData>() != null)
                        {
                            data.AlienAttached.GetComponentInChildren<AlienData>().decorAttachedTo = data;
                        }
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
            if (ps.PlacematDecorations.ContainsKey(transform.gameObject.name))
            {
                ps.PlacematDecorations[transform.gameObject.name] = placementManager.ObjectToPlace.name;
            } 
            else
            {
                ps.PlacematDecorations.Add(transform.gameObject.name, placementManager.ObjectToPlace.name);
            }
            
            string ResourceName = ps.PlacematDecorations[transform.gameObject.name];
            if (SpawnedDecoration != null)
            {
                Destroy(SpawnedDecoration);
            }

            List<PlacematScript> AlreadyExistantObject = new List<PlacematScript>(transform.parent.GetComponentsInChildren<PlacematScript>());

            foreach(PlacematScript pms in AlreadyExistantObject)
            {
                if (pms.SpawnedDecoration != null && pms.SpawnedDecoration.name == ResourceName)
                {
                    
                    GameObject obj = pms.SpawnedDecoration;
                    ps.PlacematDecorations.Remove(pms.gameObject.name);
                    pms.SpawnedDecoration = null;
                    Destroy(obj);
                }
            }
            
            SpawnedDecoration = Instantiate(Resources.Load<GameObject>("Decor/" + ResourceName), transform.position, transform.rotation, transform);
            SpawnedDecoration.name = ResourceName;
            placementManager.ObjectToPlace = null;
            placementManager.CurrentlyPlacing = false;
        }
        
    }
}
