using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ReferenceObjectLater : MonoBehaviour
{
    private PlayerStats ps;
    public string alienName;

    // Update is called once per frame
    void Update()
    {
        GameObject pso = GameObject.Find("SaveState");
        if (pso != null)
        {
            ps = pso.GetComponent<PlayerStats>();
        }

    }
    public void SaveGame()
    {
        ps.save();
    }
    public void loopStates()
    {
        if (ps.MusicState < ps.MusicStateCount - 1)
        { ps.MusicState++; }
        else { ps.MusicState = 0; }
    }
    public void ReloadWebPage()
    {
        BrowserUtils.GoBack();
    }
    public void SaveTime() { ps.TimeLeftGame = UnixTime.GetUnixTime(DateTime.Now); }
    public void runOpeningAliens()
    {
        ps.gameObject.GetComponent<AlienInfoOpener>().OpeningAlien = alienName;
    }
}
