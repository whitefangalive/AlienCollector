using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;
using static UnityEngine.GraphicsBuffer;

public class UpgradedBehavior : MonoBehaviour
{
    private clickMovable cm;
    public GameObject linePrefab;
    private GameObject linePrefabTemp;
    private Vector2 startingSize;
    public GameObject SelectionPrefab;
    private GameObject SelectionPrefabTemp;
    private PlayerStats ps;
    public float PlanetLockingThreshold = 0.5f;
    public Transform SpaceshipDesiredPos;

    private float originalLerpSpeed;

    public float shipSpeed = 0.00001f;
    private long ETA = 0;
    public LaunchItemsContainer LIC;

    private Tuple<string, long, long, Tuple<float, float>, Tuple<float, float>> TempTravelLocation;
    private Tuple<float, float> tempTargetPositon;
    // Start is called before the first frame update
    void Start()
    {
        cm = gameObject.GetComponent<clickMovable>();
        startingSize = linePrefab.GetComponent<RectTransform>().sizeDelta;
        ps = FindObjectOfType<PlayerStats>();
        originalLerpSpeed = GetComponent<LerpToPosition>().speed;

        linePrefabTemp = Instantiate(linePrefab, linePrefab.transform.position, linePrefab.transform.rotation, transform);
        linePrefabTemp.GetComponent<Image>().color = new Color32(255, 255, 255, 125);

        SelectionPrefabTemp = Instantiate(SelectionPrefab, linePrefab.transform.position, linePrefab.transform.rotation, transform);;
        SelectionPrefabTemp.GetComponent<Image>().color = new Color32(255, 255, 255, 125);
    }

    // Update is called once per frame
    void Update()
    {
        
        if (ps.UpgradedShip)
        {
            

            if (ps.TravelLocation == null)
            {
                ps.TravelLocation = new Tuple<string, long, long, Tuple<float, float>, Tuple<float, float>>("", 0, UnixTime.Now(), ConvertVectorToTuple(SpaceshipDesiredPos.position), ConvertVectorToTuple(SpaceshipDesiredPos.position));
            }
            if (TempTravelLocation == null)
            {
                TempTravelLocation = new Tuple<string, long, long, Tuple<float, float>, Tuple<float, float>>("", 0, UnixTime.Now(), ConvertVectorToTuple(SpaceshipDesiredPos.position), ConvertVectorToTuple(SpaceshipDesiredPos.position));
            }


            //double if statements here because it needs to work
            //for both travel location after and temp postion before launch
            //its just the easiest way to see it
            if (ps.TargetPosition != null && ps.TravelLocation.Item1 != "" && ConvertTupleToVector3(ps.TargetPosition) != SpaceshipDesiredPos.position &&
            !(ps.TravelLocation.Item1 != "unknown" && ps.TravelLocation.Item1 != "" && Vector3.Distance(ConvertTupleToVector3(ps.TargetPosition), SpaceshipDesiredPos.position) < (PlanetLockingThreshold + 0.2f))
            && !(Vector3.Distance(ConvertTupleToVector3(ps.TargetPosition), SpaceshipDesiredPos.position) < 0.1))
            {
                linePrefab.SetActive(true);
                SelectionPrefab.SetActive(true);
            }
            else
            {
                linePrefab.SetActive(false);
                SelectionPrefab.SetActive(false);
            }


                if (tempTargetPositon != null && TempTravelLocation.Item1 != "" && ConvertTupleToVector3(tempTargetPositon) != SpaceshipDesiredPos.position &&
                !(TempTravelLocation.Item1 != "unknown" && TempTravelLocation.Item1 != "" && Vector3.Distance(ConvertTupleToVector3(tempTargetPositon), SpaceshipDesiredPos.position) < (PlanetLockingThreshold + 0.2f))
                && !(Vector3.Distance(ConvertTupleToVector3(tempTargetPositon), SpaceshipDesiredPos.position) < 0.1))
                {
                    linePrefabTemp.SetActive(true);
                    SelectionPrefabTemp.SetActive(true);
                }
                else
                {
                    linePrefabTemp.SetActive(false);
                    SelectionPrefabTemp.SetActive(false);
                }
            GetComponent<CustomButton>().enabled = false;

            if (cm.clicking)
            {
                //make sure ship is at your exact desired position
                GetComponent<LerpToPosition>().speed = 0;
                tempTargetPositon = ConvertVectorToTuple(transform.position);

                
                detectAndLockOnPlanet();
            }
            else
            {
                //if not clicking

                //set lerp to original that way ship flies back
                GetComponent<LerpToPosition>().speed = originalLerpSpeed;

                
                
                //special Case if you're watching and you hit your location land there
                if (ps.TravelLocation.Item1 != "unknown" && ps.TravelLocation.Item1 != "" && Vector3.Distance(ConvertTupleToVector3(ps.TargetPosition), SpaceshipDesiredPos.position) < PlanetLockingThreshold)
                {
                    ps.TravelLocation = new Tuple<string, long, long, Tuple<float, float>, Tuple<float, float>>(ps.TravelLocation.Item1, UnixTime.Now(), ps.TravelLocation.Item3, ps.TravelLocation.Item4, ps.TravelLocation.Item5);

                }
            }

            //lock desired onto its TempPos planet
            if (TempTravelLocation.Item1 != "" && TempTravelLocation.Item1 != "unknown")
            {
                if (GameObject.Find(TempTravelLocation.Item1) != null)
                    tempTargetPositon = ConvertVectorToTuple(GameObject.Find(TempTravelLocation.Item1).transform.position);
            }
            //lock desired onto its planet
            if (ps.TravelLocation.Item1 != "" && ps.TravelLocation.Item1 != "unknown")
            {
                if (GameObject.Find(ps.TravelLocation.Item1) != null)
                    ps.TargetPosition = ConvertVectorToTuple(GameObject.Find(ps.TravelLocation.Item1).transform.position);
            }

            //draw both lines
            if (TempTravelLocation.Item1 != "")
            {

                if (tempTargetPositon != null)
                    ETA = drawLine(tempTargetPositon, linePrefabTemp, SelectionPrefabTemp);
            }
            if (ps.TargetPosition != null)
                drawLine(ps.TargetPosition, linePrefab, SelectionPrefab);
        }

        //control if launchButton Appears
        if (TempTravelLocation != null && TempTravelLocation.Item1 != "")
        {
            LIC.button.SetActive(true);
            LIC.planetText.text = TempTravelLocation.Item1;
        }
        else
        {
            LIC.button.SetActive(false);
        }
    }

    private long drawLine(Tuple<float, float> currentTarget, GameObject linePref, GameObject SelPref)
    {
        //do polar coordinates math here
        Vector2 differnece = ConvertTupleToVector3(currentTarget) - SpaceshipDesiredPos.position;
        Vector2 polarCords = CartesianToPolar(differnece);

        // Calculate the angle in degrees
        float angle = Mathf.Atan2(differnece.y, differnece.x) * Mathf.Rad2Deg;

        // Create the target rotation
        Quaternion targetRotation = Quaternion.AngleAxis(angle + 90, Vector3.forward);

        linePref.transform.SetPositionAndRotation((Vector2)ConvertTupleToVector3(currentTarget) - (differnece / 2), targetRotation);
        float distance = polarCords.x * startingSize.y;
        linePref.GetComponent<RectTransform>().sizeDelta = new Vector2(startingSize.x, distance);
        

        //lock selection onto target
        if (currentTarget != null)
        {
            SelPref.transform.position = ConvertTupleToVector3(currentTarget);
        }
        return (long)(distance / shipSpeed) + UnixTime.Now();
    }
    private void detectAndLockOnPlanet()
    {
        //detect if near a planet
        //lowercase because lowercase u looks better in coffeecake font

        
        TempTravelLocation = new Tuple<string, long, long, Tuple<float, float>, Tuple<float, float>>("unknown", ETA, UnixTime.Now(), tempTargetPositon, ConvertVectorToTuple(SpaceshipDesiredPos.position));
        List<CircleCollider2D> circleCollider2Ds = new List<CircleCollider2D>(FindObjectsOfType<CircleCollider2D>());
        foreach (CircleCollider2D collider in circleCollider2Ds)
        {
            GameObject obj = collider.gameObject;
            if (Vector3.Distance(obj.transform.position, ConvertTupleToVector3(tempTargetPositon)) < PlanetLockingThreshold)
            {
                TempTravelLocation = new Tuple<string, long, long, Tuple<float, float>, Tuple<float, float>>(obj.name, ETA, UnixTime.Now(), tempTargetPositon, ConvertVectorToTuple(SpaceshipDesiredPos.position));
            }
        }
        //astriods edge case
        if (Vector3.Distance(ConvertTupleToVector3(tempTargetPositon), Vector3.zero) < 10 && Vector3.Distance(ConvertTupleToVector3(tempTargetPositon), Vector3.zero) > 9)
        {
            TempTravelLocation = new Tuple<string, long, long, Tuple<float, float>, Tuple<float, float>>("Asteroids", ETA, UnixTime.Now(), tempTargetPositon, ConvertVectorToTuple(SpaceshipDesiredPos.position));
        }
    }

    public void Launch()
    {
        ps.TravelLocation = new Tuple<string, long, long, Tuple<float, float>, Tuple<float, float>>(TempTravelLocation.Item1, ETA, UnixTime.Now(), tempTargetPositon, ConvertVectorToTuple(SpaceshipDesiredPos.position));
        ps.TargetPosition = tempTargetPositon;
        TempTravelLocation = new Tuple<string, long, long, Tuple<float, float>, Tuple<float, float>>("", 0, 0, ConvertVectorToTuple(Vector2.zero), ConvertVectorToTuple(SpaceshipDesiredPos.position));
    }
    
    private Vector2 CartesianToPolar(Vector2 cart)
    {
        float a = cart.x;
        float b = cart.y;
        float r = Mathf.Sqrt(Mathf.Pow(a, 2) + Mathf.Pow(b, 2));
        float theta = Mathf.Atan(b / a);
        return new Vector2(r, theta);
    }

    private Tuple<float, float> ConvertVectorToTuple(Vector2 vector)
    {
        return new Tuple<float, float>(vector.x, vector.y);
    }

    private Vector3 ConvertTupleToVector3(Tuple<float, float> tuple)
    {
        return new Vector3(tuple.Item1, tuple.Item2, 0);
    }
}
