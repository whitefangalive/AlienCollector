using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStats : MonoBehaviour
{
    public bool InitialLoaded = false;
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

    void OnApplicationPause(bool pauseStatus)
    {
        SaveManager.saveData(this);
    }

    private void OnApplicationQuit()
    {
        SaveManager.saveData(this);
    }


}
