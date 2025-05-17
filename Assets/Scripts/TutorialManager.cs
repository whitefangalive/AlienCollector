using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class TutorialManager : MonoBehaviour
{
    public PlayerStats ps;
    public int TS;
    public bool ButtonPressedTest;
    public DialogueManager dialogueManager;
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
            
            GameObject.Find("MenuButtonGlow").GetComponent<Image>().enabled = true;
            dialogueManager.showBox("Welcome to Alien Collector! In this game you collect " +
                "aliens throughout the Solar System. To get started open the menu.");
            ps.TutorialState++;
        }

        if (ps.TutorialState == 1 && GameObject.Find("MenuButton").GetComponent<DetectPressing>().ButtonpressedRecently)
        {
            GameObject.Find("MenuButtonGlow").GetComponent<Image>().enabled = false;
            GameObject.Find("ShopButtonGlow").GetComponent<Image>().enabled = true;
            dialogueManager.showBox("Next go to the Fabricator.");
            ps.TutorialState++;
        }
        if (ps.TutorialState == 2 && GameObject.Find("ShopButton").GetComponent<DetectPressing>().ButtonpressedRecently)
        {
            if (GameObject.Find("ShopButtonGlow") != null) GameObject.Find("ShopButtonGlow").GetComponent<Image>().enabled = false;
            GameObject.Find("CowButtonGlow").GetComponent<Image>().enabled = true;
            dialogueManager.showBox("To collect aliens you need alien bait AKA cows. Buy a cow.");
            ps.TutorialState++;
        }
        if (ps.TutorialState == 3 && ps.OwnedCows.Count > 0)
        {
            if (GameObject.Find("CowButtonGlow") != null) GameObject.Find("CowButtonGlow").GetComponent<Image>().enabled = false;
            if (GameObject.Find("DecorationButtonGlow") != null) GameObject.Find("DecorationButtonGlow").GetComponent<Image>().enabled = true;
            dialogueManager.showBox("You also need a decoration for the alien to attach to. Buy a spacesuit.");
            ps.TutorialState++;
        }
        if (ps.TutorialState == 4 && ps.OwnedItems.Count > 0)
        {
            GameObject.Find("DecorationButtonGlow").GetComponent<Image>().enabled = false;
            GameObject.Find("MenuButtonGlow").GetComponent<Image>().enabled = true;
            dialogueManager.showBox("Go back to the menu then go to the decor menu.");
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
            dialogueManager.showBox("You need to place both a cow and your decoration down. Press them to place them down.");
            ps.TutorialState++;
        }
        if (ps.TutorialState == 7 && GameObject.FindObjectOfType<PlacementManager>().CurrentlyPlacing)
        {
            dialogueManager.showBox("Tap one of the yellow placemats to choose a location for your decoration.");
            ps.TutorialState++;
        }
        if (ps.TutorialState == 8 && ps.PlacematDecorations.Count > 0 && ps.Cows.Count > 0)
        {
            dialogueManager.showBox("You now you everything you need to attract an alien. Leave the app and return to see if one will visit you.");
            ps.TutorialState++;
        }
        if (ps.TutorialState == 9 && ps.Aliens.Count > 0)
        {
            dialogueManager.showBox("Congradulations! looks like " + ps.DiscoveredAliens[0] + " visted you! Tap on them to collect them!");
            ps.TutorialState++;
        }

        if (ps.TutorialState == 10 && IsInScene("Catelog"))
        {
            dialogueManager.showBox("This is your Alien Catelog. Come here to see what aliens you've collected. Once your alien leaves it will offer you scrap. Also your cows odds of being taken by an alien increase. Thats everything! happy collecting!");
            ps.TutorialState++;
        }
    }

    private bool IsInScene(string sceneName)
    {
        Scene ddol = SceneManager.GetSceneByName("DontDestroyOnLoad");
        if (ddol == null)
        {
            return SceneManager.GetSceneAt(0).name == sceneName;
        }
        int dontDestroySceneIndex = ddol.buildIndex;

        // Iterate through all loaded scenes
        for (int i = 0; i < SceneManager.sceneCount; i++)
        {
            Scene scene = SceneManager.GetSceneAt(i);
            if (scene.buildIndex != dontDestroySceneIndex)
            {
                if (scene.name == sceneName)
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
        }
        return false;
    }
}
