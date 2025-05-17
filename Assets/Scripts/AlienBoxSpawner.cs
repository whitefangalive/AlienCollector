using JetBrains.Annotations;
using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEditor;
using UnityEngine;
using UnityEngine.Timeline;
using UnityEngine.UI;

public class AlienBoxSpawner : MonoBehaviour
{
    public Transform Item1;
    public Transform Item2;
    public GameObject TemplateItem;
    public Transform MarkerTemplate;
    public GameObject CategoryTemplate;

    private PlayerStats ps;
    private Vector3 distance;
    public float ColumnMax = 3.0f;
    public List<GameObject> items = new List<GameObject>();
    public List<GameObject> categories = new List<GameObject>();
    public List<GameObject> itemsAndCatories = new List<GameObject>();
    public Sprite QuestionMark;
    // Start is called before the first frame update
    void Start()
    {
        spawnIn();
    }

    private List<GameObject> loadAllAliens()
    {
        List<GameObject> list = new List<GameObject>();
        List<GameObject> AllPrefabs = new List<GameObject>(Resources.LoadAll<GameObject>("Aliens"));

        foreach (GameObject item in AllPrefabs)
        {
            if (item.GetComponent<AlienData>() != null && item.GetComponent<AlienData>().alienName != "" && !list.Contains(item))
            {
                list.Add(item);
            }
        }
        return list;
    }
    private List<string> loadAllAlienNames()
    {
        List<string> list = new List<string>();
        List<GameObject> AllPrefabs = new List<GameObject>(Resources.LoadAll<GameObject>("Aliens"));

        foreach (GameObject item in AllPrefabs)
        {
            if (item.GetComponent<AlienData>() != null && item.GetComponent<AlienData>().alienName != "" && !list.Contains(item.GetComponent<AlienData>().alienName))
            {
                list.Add(item.GetComponent<AlienData>().alienName);
            }
        }
        return list;
    }
    public void spawnIn()
    {
        items.Clear();
        Destroy(GameObject.Find("SecondMarker(Clone)"));
        GameObject pso = GameObject.Find("SaveState");
        if (pso != null)
        {
            ps = pso.GetComponent<PlayerStats>();
        }
        Item1.gameObject.SetActive(false);
        Item2.gameObject.SetActive(false);
        CategoryTemplate.gameObject.SetActive(false);
        distance = Item1.transform.position - Item2.transform.position;

        
        List<string> listOfItems = loadAllAlienNames();
        
        int AmountOfCategories = System.Enum.GetValues(typeof(Planets.Planet)).Length;
        Debug.Log(AmountOfCategories);
        int FullRowCountRightNow = 0;
        for(int i = 0; i < AmountOfCategories; i++)
        {
            Planets.Planet CurrentPlanet = (Planets.Planet)i;
            Vector3 pos = new Vector3(CategoryTemplate.transform.position.x, CategoryTemplate.transform.position.y - (FullRowCountRightNow * distance.y), CategoryTemplate.transform.position.z);
            categories.Add(Instantiate(CategoryTemplate, pos, CategoryTemplate.transform.rotation, transform));
            itemsAndCatories.Add(categories[i]);
            FullRowCountRightNow++;
            categories[i].gameObject.SetActive(true);
            categories[i].GetComponentInChildren<TMP_Text>().text = CurrentPlanet.ToString();

            List<string> AliensInThisCategory = new List<string>();
            List<GameObject> allAlienObjects = loadAllAliens();
            foreach (GameObject alienObj in allAlienObjects)
            {
                if (alienObj.GetComponent<AlienData>().FavoritePlanet == CurrentPlanet)
                {
                    AliensInThisCategory.Add(alienObj.GetComponent<AlienData>().alienName);
                }
            }

            spawnAllBoxes(AliensInThisCategory, FullRowCountRightNow);
            int numberSpawned = Mathf.CeilToInt(AliensInThisCategory.Count / ColumnMax);
            FullRowCountRightNow += numberSpawned;

        }

        int numberOfRows = FullRowCountRightNow;

        //set amount for dragging menu

        ClickSlidePosition CSP = transform.parent.GetComponent<ClickSlidePosition>();
        if (numberOfRows > 0)
        {
            Vector3 lastBoxLocPos = itemsAndCatories[itemsAndCatories.Count - 1].transform.localPosition;
            float x = Item2.position.x;
            int bottomVisibleBoxNumber = Mathf.Clamp(CSP.AllowedBoxesToSee - 1, 0, numberOfRows - 1);
            float lastVisibleBoxLocY = itemsAndCatories[bottomVisibleBoxNumber].transform.localPosition.y;

            float y = (transform.localPosition.y) + lastVisibleBoxLocY - lastBoxLocPos.y;

            float z = lastBoxLocPos.z;
            MarkerTemplate.localPosition = new Vector3(x, y, z);
        }

        GameObject emptyGO = new GameObject();
        emptyGO.name = "SecondMarker";
        Transform newTransform = emptyGO.transform;

        CSP.ClampObject = Instantiate(newTransform, MarkerTemplate.position, MarkerTemplate.rotation, CSP.transform.parent);
    }

    private void spawnAllBoxes(List<string> listOfItems, int StartingPos) 
    {
        int numberOfRows = Mathf.CeilToInt(listOfItems.Count / ColumnMax);
        int count = 0;
        int offset = items.Count;//how many items are already in items
        for (int i = 0; i < numberOfRows; i++)
        {
            for (int j = 0; j < ColumnMax && listOfItems.Count > count; j++)
            {
                if (listOfItems[count] != null)
                {
                    
                    items.Add(Instantiate(TemplateItem, new Vector3(Item1.position.x - (distance.x * j), Item1.position.y - (distance.y * (i + StartingPos)),
                        Item1.position.z), Item1.rotation, transform));
                    itemsAndCatories.Add(items[count + offset]);
                    string alienNameInDiscoveredList = listOfItems[count];
                    items[count + offset].SetActive(true);


                    GameObject alienObj = Resources.Load<GameObject>("Aliens/" + alienNameInDiscoveredList);

                    AlienInfo info = items[count + offset].GetComponentInChildren<AlienInfo>();
                    if (ps.DiscoveredAliens.Contains(alienNameInDiscoveredList))
                    {
                        Sprite img = alienObj.GetComponentInChildren<Button>().targetGraphic.gameObject.GetComponent<Image>().sprite;
                        info.alienThumbnail.sprite = img;
                        info.alienImage.sprite = img;
                        info.nameText.text = alienNameInDiscoveredList;
                        AlienData data = alienObj.GetComponent<AlienData>();
                        info.DescriptionText.text = data.Description;
                        info.PlanetText.text = (data.FavoritePlanet).ToString();
                        info.WealthText.text = data.wealth.ToString();
                    }
                    else
                    {
                        Sprite img = QuestionMark;
                        info.alienThumbnail.sprite = img;
                        info.alienImage.sprite = img;
                        info.nameText.text = "???";

                        info.DescriptionText.text = "Unknown";
                        info.PlanetText.text = "Unknown";
                        info.WealthText.text = "Unknown";
                    }
                }
                count++;
            }
        }
    }
}
