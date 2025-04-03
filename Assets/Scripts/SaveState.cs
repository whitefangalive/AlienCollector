using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class SaveState
{
    public Dictionary<string, string> placematDecorations = new Dictionary<string, string>();
    public int scrap;
    public List<Tuple<string, int>> cows = new List<Tuple<string, int>>();
    public List<Tuple<long, string, string>> aliens = new List<Tuple<long, string, string>>();
    public long timeTillCanSpawnAnAlien;
    public Dictionary<string, int> ownedCows = new Dictionary<string, int>();
    public List<string> ownedItems = new List<string>();
    public List<Tuple<string, int>> alienGifts = new List<Tuple<string, int>>();

    public SaveState(PlayerStats ps) 
    {
        placematDecorations = ps.PlacematDecorations;
        scrap = ps.Scrap;
        cows = ps.Cows;
        aliens = ps.Aliens;
        timeTillCanSpawnAnAlien = ps.TimeTillCanSpawnAnAlien;
        ownedCows = ps.OwnedCows;
        ownedItems = ps.OwnedItems;
        alienGifts = ps.AlienGifts;

    }
}
