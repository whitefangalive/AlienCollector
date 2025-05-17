using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GiftManager : MonoBehaviour
{
    private PlayerStats ps;
    public GameObject item1;
    public GameObject GiftsButton;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (ps == null)
        {
            ps = GameObject.Find("SaveState").GetComponent<PlayerStats>();
        }

        if (ps.AlienGifts.Count > 0)
        {
            item1.SetActive(true);
            GiftsButton.SetActive(true);
        } else
        {
            GiftsButton.GetComponent<MoveObjectToLocation>().GoToLocationIndexWithoutToggle(0);
            item1.SetActive(false);
            GiftsButton.SetActive(false);

        }
    }
}
