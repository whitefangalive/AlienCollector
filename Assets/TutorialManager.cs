using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TutorialManager : MonoBehaviour
{
    private PlayerStats ps;
    public int TS;
    public bool ButtonPressedTest;
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        GameObject pso = GameObject.Find("SaveState");
        if (pso != null)
        {
            ps = pso.GetComponent<PlayerStats>();
        }
        TS = ps.TutorialState;
        if (ps.TutorialState == 0 && ps.InitialLoaded)
        {
            
            //setupState
            ps.Scrap = 15;
            ps.GameStartTime = UnixTime.GetUnixTime(DateTime.Now);
            ps.TutorialState++;
            GameObject.Find("MenuButtonGlow").GetComponent<Image>().enabled = true;
        }

        if (ps.TutorialState == 1 && GameObject.Find("MenuButton").GetComponent<DetectPressing>().ButtonpressedRecently)
        {
            GameObject.Find("MenuButtonGlow").GetComponent<Image>().enabled = false;
            GameObject.Find("ShopButtonGlow").GetComponent<Image>().enabled = true;
            ps.TutorialState++;
        }
        if (ps.TutorialState == 2 && GameObject.Find("ShopButton").GetComponent<DetectPressing>().ButtonpressedRecently)
        {
            if (GameObject.Find("ShopButtonGlow") != null) GameObject.Find("ShopButtonGlow").GetComponent<Image>().enabled = false;
            if (GameObject.Find("CowButtonGlow") != null) GameObject.Find("CowButtonGlow").GetComponent<Image>().enabled = true;
            ps.TutorialState++;
        }
        if (ps.TutorialState == 3 && GameObject.Find("CowButton") != null && GameObject.Find("CowButton").GetComponent<DetectPressing>().ButtonpressedRecently)
        {
            if (GameObject.Find("CowButtonGlow") != null) GameObject.Find("CowButtonGlow").GetComponent<Image>().enabled = false;
            if (GameObject.Find("DecorationButtonGlow") != null) GameObject.Find("DecorationButtonGlow").GetComponent<Image>().enabled = true;
            ps.TutorialState++;
        }
        if (ps.TutorialState == 4 && GameObject.Find("DecorationButton").GetComponent<DetectPressing>().ButtonpressedRecently)
        {
            GameObject.Find("DecorationButtonGlow").GetComponent<Image>().enabled = false;
            GameObject.Find("MenuButtonGlow").GetComponent<Image>().enabled = true;
            ps.TutorialState++;
        }
        if (ps.TutorialState == 5 && GameObject.Find("MenuButton").GetComponent<DetectPressing>().ButtonpressedRecently)
        {
            GameObject.Find("MenuButtonGlow").GetComponent<Image>().enabled = false;
            GameObject.Find("DecorButtonGlow").GetComponent<Image>().enabled = true;
            ps.TutorialState++;
        }
        if (ps.TutorialState == 6 && GameObject.Find("DecorButton").GetComponent<DetectPressing>().ButtonpressedRecently)
        {
            GameObject.Find("DecorButtonGlow").GetComponent<Image>().enabled = false;
            
            
        }
    }
}
