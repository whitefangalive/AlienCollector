using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class DecorButton : MonoBehaviour
{
    public GameObject ContainedObject;
    public TMP_Text title;
    public Image image;
    private PlacementManager placementManager;
    // Start is called before the first frame update
    void Start()
    {
        placementManager = GameObject.Find("PlacementManager").GetComponent<PlacementManager>();
        title.text = ContainedObject.name;
        image.sprite = ContainedObject.GetComponent<Image>().sprite;
    }

    public void triggerPlacement()
    {
        placementManager.ObjectToPlace = ContainedObject;
        placementManager.CurrentlyPlacing = true;
    }




}
