using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class UnlockUpgrade : MonoBehaviour
{
    private MapSceneManager msm;
    public GameObject BuyMenu;
    private PlayerStats ps;
    public int cost;
    public TMP_Text costText;
    private void Start()
    {
        msm = FindObjectOfType<MapSceneManager>();
        costText.text = cost.ToString();
    }
    private void Update()
    {
        if (ps == null)
        {
            ps = GameObject.Find("SaveState").GetComponent<PlayerStats>();
        }
        if (msm.AtStation && !ps.UpgradedShip)
        {
            BuyMenu.SetActive(true);
        } 
        else
        {
            BuyMenu.SetActive(false);
        }
    }
    public void upgradeCondtional()
    {
        if (msm.AtStation)
        {
            if (ps.Scrap >= cost)
            {
                ps.Scrap -= cost;
                ps.UpgradedShip = true;
                ps.TravelLocation = new Tuple<string, long, long, Tuple<float, float>, Tuple<float, float>>("UpgradeStation", 0, UnixTime.Now(), ConvertVectorToTuple(transform.position), ConvertVectorToTuple(transform.position));
            }
        }
    }

    private Tuple<float, float> ConvertVectorToTuple(Vector2 vector)
    {
        return new Tuple<float, float>(vector.x, vector.y);
    }
}
