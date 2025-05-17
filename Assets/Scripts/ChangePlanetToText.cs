using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Image))]
public class ChangePlanetToText : MonoBehaviour
{
    public TMP_Text text;
    private Image image;
    private string oldText = "";
    // Start is called before the first frame update
    void Start()
    {
        image = GetComponent<Image>();
    }
    private void FixedUpdate()
    {
        if (oldText != text.text)
        {
            oldText = text.text;
            if (Resources.Load<Sprite>("SpaceStuff/" + text.text) != null)
            {
                image.sprite = Resources.Load<Sprite>("SpaceStuff/" + text.text);
            } 
            else
            {
                image.sprite = Resources.Load<Sprite>("SpaceStuff/StandInPlanet");
            }
            
        }
    }
}
