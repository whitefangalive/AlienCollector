using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class SaveState
{
    public Dictionary<string, string> placematDecorations = new Dictionary<string, string>();
    public int scrap;
    public List<string> cows = new List<string>();


    public SaveState(PlayerStats ps) 
    {
        placematDecorations = ps.PlacematDecorations;
        scrap = ps.Scrap;
        cows = ps.Cows;
    }
}
