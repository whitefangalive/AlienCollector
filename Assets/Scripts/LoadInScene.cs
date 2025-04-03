using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class LoadInScene : MonoBehaviour
{
    private PlayerStats ps;
    [HideInInspector]public AlienSpawner spawner;
    public bool needsSpawnAlien = false;
    
    public void setupScene()
    {
        GameObject pso = GameObject.Find("SaveState");
        if (pso != null)
        {
            ps = pso.GetComponent<PlayerStats>();
            spawner = pso.GetComponent<AlienSpawner>();
        }
        GameObject saveManager = GameObject.Find("SaveState");
        if (saveManager != null)
        {
            string path = Application.persistentDataPath + "/game.save";
            if (File.Exists(path))
            {
                unpackData(SaveManager.loadData());
            }
            ps.InitialLoaded = true;
            needsSpawnAlien = true;
        }
    }
    private void Awake()
    {
        
    }
    public void unpackData(SaveState save)
    {
        ps.PlacematDecorations = save.placematDecorations;
        ps.Scrap = save.scrap;
        ps.Cows = save.cows;
        ps.Aliens = save.aliens;
        ps.TimeTillCanSpawnAnAlien = save.timeTillCanSpawnAnAlien;
        ps.OwnedCows = save.ownedCows;
        ps.OwnedItems = save.ownedItems;
        ps.AlienGifts = save.alienGifts;
        ps.TimeLeftGame = save.timeLeftGame;
    }
}
