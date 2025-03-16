using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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
    public Dictionary<string, string> PlacematDecorations = new Dictionary<string, string>();
    public int Scrap;
    public List<string> Cows = new List<string>();
    // time when to despawn, Object attached to, alien name
    public List<Tuple<long, string, string>> Aliens = new List<Tuple<long, string, string>>();
    // DateTime.Now in unix; the alien will have a time when it is supposed to despawn, say it will leave at 12
    public long TimeTillCanSpawnAnAlien = 0;

    void OnApplicationPause(bool pauseStatus)
    {
        SaveManager.saveData(this);
    }

    private void OnApplicationQuit()
    {
        SaveManager.saveData(this);
    }


    

}
