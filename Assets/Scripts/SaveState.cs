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
    public long timeLeftGame = 0;
    public List<string> discoveredAliens = new List<string>();
    public int tutorialState;
    public long gameStartTime;

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
        timeLeftGame = ps.TimeLeftGame;
        discoveredAliens = ps.DiscoveredAliens;
        tutorialState = ps.TutorialState;
        gameStartTime = ps.GameStartTime;
    }
}
