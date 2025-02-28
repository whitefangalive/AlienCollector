using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LoadInScene : MonoBehaviour
{
    private PlayerStats ps;
    
    public void setupScene()
    {
        GameObject pso = GameObject.Find("SaveState");
        if (pso != null)
        {
            ps = pso.GetComponent<PlayerStats>();
        }
        GameObject saveManager = GameObject.Find("SaveState");
        if (saveManager != null)
        {

            unpackData(SaveManager.loadData());
            ps.InitialLoaded = true;
        }
    }
    private void Awake()
    {
        
    }
    public void unpackData(SaveState save)
    {
        ps.PlacematDecorations = save.placematDecorations;
        ps.Scrap = save.scrap;
    }
}
