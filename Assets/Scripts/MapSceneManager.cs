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
    public float LocationMultipler = (3.25f / 55919f);
    public bool AtStation = false;
    private PlayerStats ps;
    // Start is called before the first frame update
    void Start()
    {
        ps = FindObjectOfType<PlayerStats>();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (pm == null)
        {
            pm = GameObject.Find("SaveState").GetComponent<PlanetManager>();
        }

        if (ps.UpgradedShip)
        {
            UpgradedBehaviorFunc();
        }
        else 
        {
            regularBehavior();
        }
    }

    private void UpgradedBehaviorFunc()
    {
        if (ps.TravelLocation != null)
        {
        nextPlanetText.text = "At: " + pm.currentPlanet.ToString() + "\nNext: " + ps.TravelLocation.Item1;

        TimeSpan Difference = UnixTime.GetDateTime(ps.TravelLocation.Item2) - UnixTime.GetDateTime(UnixTime.Now());
        // for some reason its always 5 hours and 14 seconds off
        
        ETA.text = $"{Difference.Days}:{Difference.Hours}:{Difference.Minutes}:{Difference.Seconds}";

            if (ps.TravelLocation.Item1 != "")
            {

                //time i arrive - the time i left is the time spent traveling to my destination at the end of my trip
                // now - the time i left is the time spent travelling so far
                //so far taveled / full time to travel = % travelled
                float amountAlongLine = (ps.TravelLocation.Item3 - UnixTime.Now()) / Mathf.Clamp(Mathf.Abs(ps.TravelLocation.Item2 - ps.TravelLocation.Item3), Mathf.Epsilon, Mathf.Infinity);

                Vector2 destination = Vector2.zero;
                if (ps.TravelLocation.Item1 != "unknown" && ps.TravelLocation.Item1 != "Asteroids")
                {
                    //orbit planet if is one
                    if (Difference < TimeSpan.FromSeconds(30))
                    {
                        spaceShip.gameObject.GetComponent<OrbitObject>().objectToOrbit
                                    = GameObject.Find(ps.TravelLocation.Item1);
                    }
                    //destination is a planet
                    destination = GameObject.Find(ps.TravelLocation.Item1).transform.position;
                }
                else
                {
                    //travel along the line or sit at location
                    spaceShip.gameObject.GetComponent<OrbitObject>().objectToOrbit = null;

                    //destination is a point
                    destination = ConvertTupleToVector3(ps.TravelLocation.Item4);

                }
                //postion between two points at a % =
                //positon between points pointa - pointb
                // * %
                if (Difference > TimeSpan.FromTicks(0))
                {
                    spaceShip.transform.position = ConvertTupleToVector2(ps.TravelLocation.Item5) + ((ConvertTupleToVector2(ps.TravelLocation.Item5) - destination) * amountAlongLine);
                } else
                {
                    if (ps.TravelLocation.Item1 == "unknown" || ps.TravelLocation.Item1 == "Asteroids")
                    spaceShip.transform.position = destination;
                }
            }
        }
    }

    private void regularBehavior()
    {

        
        nextPlanetText.text = "At: " + pm.currentPlanet.ToString() + "\nNext: " + pm.nextPlanet.Item1.ToString();

        TimeSpan Difference = pm.getEta(pm.nextPlanet.Item2) - DateTime.Now;
        ETA.text = $"{Difference.Days}:{Difference.Hours}:{Difference.Minutes}:{Difference.Seconds}";

        if (pm.currentPlanet != Planets.Planet.Space && pm.currentPlanet != Planets.Planet.Asteroids)
        {
            spaceShip.gameObject.GetComponent<OrbitObject>().objectToOrbit
                            = GameObject.Find(pm.currentPlanet.ToString());
        }
        else
        {
            spaceShip.gameObject.GetComponent<OrbitObject>().objectToOrbit = null;
            spaceShip.transform.localPosition = new Vector3(0, (UnixTime.GetUnixTime(DateTime.Now) - pm.gameStartTime) * LocationMultipler, 0);
        }
        //edge case of past neptune
        //frick it neptunes is 33120 i can get it but im too lazy
        if (UnixTime.GetUnixTime(DateTime.Now) - pm.gameStartTime > UnixTime.GetUnixTimeMinutes(33120) && pm.currentPlanet == Planets.Planet.Space)
        {
            //hard coded because im so tired arrggghh
            //not a real fix
            spaceShip.transform.localPosition = new Vector3(0, (UnixTime.GetUnixTime(DateTime.Now) - pm.gameStartTime) * (8.311978e-06f), 0);
        }

        //edge case for space station
        if (UnixTime.GetUnixTime(DateTime.Now) - pm.gameStartTime > UnixTime.GetUnixTimeMinutes(35120))
        {
            spaceShip.gameObject.GetComponent<OrbitObject>().objectToOrbit = null;
            spaceShip.transform.localPosition = new Vector3(0, 17.903f, 0);
            AtStation = true;
        }
    }
    private Tuple<float, float> ConvertVectorToTuple(Vector2 vector)
    {
        return new Tuple<float, float>(vector.x, vector.y);
    }

    private Vector3 ConvertTupleToVector3(Tuple<float, float> tuple)
    {
        return new Vector3(tuple.Item1, tuple.Item2, 0);
    }
    private Vector2 ConvertTupleToVector2(Tuple<float, float> tuple)
    {
        return new Vector2(tuple.Item1, tuple.Item2);
    }
}
