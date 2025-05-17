using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class OfferingsContainer : MonoBehaviour
{
    public Image image;
    public TMP_Text nameText;
    public TMP_Text costText;
    public int number;
    public void AcceptOffering()
    {
        PlayerStats ps;
        GameObject pso = GameObject.Find("SaveState");
        if (pso != null)
        {
            ps = pso.GetComponent<PlayerStats>();
            ps.Scrap += int.Parse(costText.text);
            ps.AlienGifts.RemoveAt(number);
            GameObject.Find("OfferingsSpawner").GetComponent<OfferingsSpawner>().spawn();
        }
    }
}
