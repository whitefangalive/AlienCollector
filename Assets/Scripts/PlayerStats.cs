using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class PlayerStats : MonoBehaviour
{
    public bool InitialLoaded = false;
    //I need to calculate time since you closed the app, gotten when app opens

    private void Update()
    {
        if (InitialLoaded == false)
        {
            GameObject.Find("LoadIn").GetComponent<LoadInScene>().setupScene();
        }
    }
    //to be saved
    //placemat decoration Dictionary<ThePlaceMat, TheObject>
    public Dictionary<string, string> PlacematDecorations = new Dictionary<string, string>();
    public int Scrap;
    public List<Tuple<string, int>> Cows = new List<Tuple<string, int>>();
    // time when to despawn, Object attached to, alien name
    public List<Tuple<long, string, string>> Aliens = new List<Tuple<long, string, string>>();
    // DateTime.Now in unix; the alien will have a time when it is supposed to despawn, say it will leave at 12
    public long TimeTillCanSpawnAnAlien = 0;
    //The time it at which you closed the game last, used to determmine how
    public long TimeLeftGame;
    //id, amount of cows owned;
    public Dictionary<string, int> OwnedCows = new Dictionary<string, int>();
    public List<string> OwnedItems = new List<string>();

    public List<Tuple<string, int>> AlienGifts = new List<Tuple<string, int>>();
    public List<string> DiscoveredAliens = new List<string>();
    public long GameStartTime;
    //timer for spawning scrap and cows outside the window
    public float SpawningTimer;

    //planet name and theta
    public Dictionary<string, float> PlanetTheta = new Dictionary<string, float>();

    public int TutorialState;

    public float TimeScale = 1;

    void OnApplicationPause(bool pauseStatus)
    {
        if (pauseStatus ==  true)
        {
            TimeLeftGame = UnixTime.GetUnixTime(DateTime.Now);
        }
        SaveManager.saveData(this);
    }

    private void OnApplicationQuit()
    {
        TimeLeftGame = UnixTime.GetUnixTime(DateTime.Now);
        SaveManager.saveData(this);
    }
}
