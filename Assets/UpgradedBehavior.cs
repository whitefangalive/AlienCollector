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
    private Vector2 startingSize;
    public GameObject SelectionPrefab;
    private PlayerStats ps;
    public float PlanetLockingThreshold = 0.5f;
    public Transform SpaceshipDesiredPos;

    private float originalLerpSpeed;

    public float shipSpeed = 0.00001f;
    private long ETA = 0;
    public LaunchItemsContainer LIC;

    private Tuple<string, long, long, Tuple<float, float>, Tuple<float, float>> TempPosition;
    private Tuple<float, float> tempTargetPositon;
    // Start is called before the first frame update
    void Start()
    {
        cm = gameObject.GetComponent<clickMovable>();
        startingSize = linePrefab.GetComponent<RectTransform>().sizeDelta;
        ps = FindObjectOfType<PlayerStats>();
        originalLerpSpeed = GetComponent<LerpToPosition>().speed;

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
            if (TempPosition == null)
            {
                TempPosition = new Tuple<string, long, long, Tuple<float, float>, Tuple<float, float>>("", 0, UnixTime.Now(), ConvertVectorToTuple(SpaceshipDesiredPos.position), ConvertVectorToTuple(SpaceshipDesiredPos.position));
            }

            if (TempPosition.Item1 == "")
            {
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
            } 
            else
            {
                if (tempTargetPositon != null && TempPosition.Item1 != "" && ConvertTupleToVector3(tempTargetPositon) != SpaceshipDesiredPos.position &&
                !(TempPosition.Item1 != "unknown" && TempPosition.Item1 != "" && Vector3.Distance(ConvertTupleToVector3(tempTargetPositon), SpaceshipDesiredPos.position) < (PlanetLockingThreshold + 0.2f))
                && !(Vector3.Distance(ConvertTupleToVector3(tempTargetPositon), SpaceshipDesiredPos.position) < 0.1))
                {
                    linePrefab.SetActive(true);
                    SelectionPrefab.SetActive(true);
                }
                else
                {
                    linePrefab.SetActive(false);
                    SelectionPrefab.SetActive(false);
                }
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

                //if you aren't at a locaation yet or put your target too close to
                //current pos then cancel
                if (ps.TravelLocation.Item1 == "" || Vector3.Distance(ConvertTupleToVector3(ps.TargetPosition), SpaceshipDesiredPos.position) < 0.1)
                {
                    RemoveLine();
                }
                
                //special Case if you're watching and you hit your location land there
                if (ps.TravelLocation.Item1 != "unknown" && ps.TravelLocation.Item1 != "" && Vector3.Distance(ConvertTupleToVector3(ps.TargetPosition), SpaceshipDesiredPos.position) < PlanetLockingThreshold)
                {
                    ps.TravelLocation = new Tuple<string, long, long, Tuple<float, float>, Tuple<float, float>>(ps.TravelLocation.Item1, UnixTime.Now(), ps.TravelLocation.Item3, ps.TravelLocation.Item4, ps.TravelLocation.Item5);

                }
            }

            //lock desired onto its TempPos planet
            if (TempPosition.Item1 != "" && TempPosition.Item1 != "unknown")
            {
                if (GameObject.Find(TempPosition.Item1) != null)
                    tempTargetPositon = ConvertVectorToTuple(GameObject.Find(TempPosition.Item1).transform.position);
            } else
            {
                //lock desired onto its planet
                if (ps.TravelLocation.Item1 != "" && ps.TravelLocation.Item1 != "unknown")
                {
                    if (GameObject.Find(ps.TravelLocation.Item1) != null)
                        ps.TargetPosition = ConvertVectorToTuple(GameObject.Find(ps.TravelLocation.Item1).transform.position);
                }
            }

            //draw both lines
            if (TempPosition.Item1 != "")
            {

                if (tempTargetPositon != null)
                    drawLine(tempTargetPositon);
            } else
            {
                if (ps.TargetPosition != null)
                    drawLine(ps.TargetPosition);
            } 
        }

        //control if launchButton Appears
        if (TempPosition != null && TempPosition.Item1 != "")
        {
            LIC.button.SetActive(true);
            LIC.planetText.text = TempPosition.Item1;
        }
        else
        {
            LIC.button.SetActive(false);
        }
    }

    private void drawLine(Tuple<float, float> currentTarget)
    {
        //do polar coordinates math here
        Vector2 differnece = ConvertTupleToVector3(currentTarget) - SpaceshipDesiredPos.position;
        Vector2 polarCords = CartesianToPolar(differnece);

        // Calculate the angle in degrees
        float angle = Mathf.Atan2(differnece.y, differnece.x) * Mathf.Rad2Deg;

        // Create the target rotation
        Quaternion targetRotation = Quaternion.AngleAxis(angle + 90, Vector3.forward);

        linePrefab.transform.SetPositionAndRotation((Vector2)ConvertTupleToVector3(currentTarget) - (differnece / 2), targetRotation);
        float distance = polarCords.x * startingSize.y;
        linePrefab.GetComponent<RectTransform>().sizeDelta = new Vector2(startingSize.x, distance);
        ETA = (long)(distance / shipSpeed) + UnixTime.Now();

        //lock selection onto target
        if (currentTarget != null)
        {
            SelectionPrefab.transform.position = ConvertTupleToVector3(currentTarget);
        }
    }
    private void detectAndLockOnPlanet()
    {
        //detect if near a planet
        //lowercase because lowercase u looks better in coffeecake font

        
        TempPosition = new Tuple<string, long, long, Tuple<float, float>, Tuple<float, float>>("unknown", ETA, UnixTime.Now(), tempTargetPositon, ConvertVectorToTuple(SpaceshipDesiredPos.position));
        List<CircleCollider2D> circleCollider2Ds = new List<CircleCollider2D>(FindObjectsOfType<CircleCollider2D>());
        foreach (CircleCollider2D collider in circleCollider2Ds)
        {
            GameObject obj = collider.gameObject;
            if (Vector3.Distance(obj.transform.position, ConvertTupleToVector3(tempTargetPositon)) < PlanetLockingThreshold)
            {
                TempPosition = new Tuple<string, long, long, Tuple<float, float>, Tuple<float, float>>(obj.name, ETA, UnixTime.Now(), tempTargetPositon, ConvertVectorToTuple(SpaceshipDesiredPos.position));
            }
        }
        //astriods edge case
        if (Vector3.Distance(ConvertTupleToVector3(tempTargetPositon), Vector3.zero) < 10 && Vector3.Distance(ConvertTupleToVector3(tempTargetPositon), Vector3.zero) > 9)
        {
            TempPosition = new Tuple<string, long, long, Tuple<float, float>, Tuple<float, float>>("Asteroids", ETA, UnixTime.Now(), tempTargetPositon, ConvertVectorToTuple(SpaceshipDesiredPos.position));
        }
    }

    public void Launch()
    {
        ps.TravelLocation = new Tuple<string, long, long, Tuple<float, float>, Tuple<float, float>>(TempPosition.Item1, ETA, UnixTime.Now(), tempTargetPositon, ConvertVectorToTuple(SpaceshipDesiredPos.position));
        ps.TargetPosition = tempTargetPositon;
        TempPosition = new Tuple<string, long, long, Tuple<float, float>, Tuple<float, float>>("", 0, 0, ConvertVectorToTuple(Vector2.zero), ConvertVectorToTuple(SpaceshipDesiredPos.position));
    }
    private void RemoveLine()
    {
        ps.TravelLocation = new Tuple<string, long, long, Tuple<float, float>, Tuple<float, float>>("", 0, 0, ConvertVectorToTuple(Vector2.zero), ConvertVectorToTuple(SpaceshipDesiredPos.position));
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
