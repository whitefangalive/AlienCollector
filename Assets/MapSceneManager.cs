using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class MapSceneManager : MonoBehaviour
{
    public TMP_Text nextPlanetText;
    public TMP_Text ETA;
    public PlanetManager pm;
    public Transform spaceShip;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (pm == null)
        {
            pm = GameObject.Find("SaveState").GetComponent<PlanetManager>();
        }
        nextPlanetText.text = "At: " + pm.currentPlanet.ToString() + "\nNext: " + pm.nextPlanet.Item1.ToString();

        TimeSpan Difference = pm.getEta(pm.nextPlanet.Item2) - DateTime.Now;
        ETA.text = $"{Difference.Days}:{Difference.Hours}:{Difference.Minutes}:{Difference.Seconds}";

        if (pm.currentPlanet != Planets.Planet.Space)
        {
            spaceShip.gameObject.GetComponent<OrbitObject>().objectToOrbit 
                            = GameObject.Find(pm.currentPlanet.ToString());
        } else
        {
            spaceShip.gameObject.GetComponent<OrbitObject>().objectToOrbit
                            = null;
            transform.localPosition = new Vector3(0, UnixTime.GetUnixTime(DateTime.Now) - pm.gameStartTime * (3.25f / 559199.9808f), 0);
        }
    }
}
