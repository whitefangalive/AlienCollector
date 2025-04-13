using JetBrains.Annotations;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Timeline;
using UnityEngine.UI;

public class AlienBoxSpawner : MonoBehaviour
{
    public Transform Item1;
    public Transform Item2;
    public GameObject TemplateItem;
    public Transform MarkerTemplate;

    private PlayerStats ps;
    private Vector3 distance;
    public float ColumnMax = 3.0f;
    public List<GameObject> items = new List<GameObject>();
    // Start is called before the first frame update
    void Start()
    {
        spawnIn();
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
        distance = Item1.transform.position - Item2.transform.position;

        List<string> listOfItems = ps.DiscoveredAliens;
        int count = 0;
        int numberOfRows = Mathf.CeilToInt(listOfItems.Count / ColumnMax);
        for (int i = 0; i < numberOfRows; i++)
        {
            Debug.Log(listOfItems.Count);
            for (int j = 0; j < ColumnMax && listOfItems.Count > count; j++)
            {
                if (listOfItems[count] != null)
                {
                    items.Add(Instantiate(TemplateItem, new Vector3(Item1.position.x - (distance.x * j), Item1.position.y - (distance.y * i),
                        Item1.position.z), Item1.rotation, transform));
                    string alienNameInDiscoveredList = listOfItems[count];
                    items[count].SetActive(true);
                    
                    Debug.Log("Aliens/" + alienNameInDiscoveredList);
                    GameObject alienObj = Resources.Load<GameObject>("Aliens/" + alienNameInDiscoveredList);

                    AlienInfo info = items[count].GetComponentInChildren<AlienInfo>();
                    Sprite img = alienObj.GetComponentInChildren<Button>().targetGraphic.gameObject.GetComponent<Image>().sprite;
                    info.alienThumbnail.sprite = img;
                    info.alienImage.sprite = img;
                    info.nameText.text = alienNameInDiscoveredList;
                    AlienData data = alienObj.GetComponent<AlienData>();
                    info.DescriptionText.text = data.Description;
                    info.PlanetText.text = (data.FavoritePlanet).ToString();
                    info.WealthText.text = data.wealth.ToString();
                }
                count++;
            }
        }
        //set amount for dragging menu

        ClickSlidePosition CSP = transform.parent.GetComponent<ClickSlidePosition>();
        if (numberOfRows > 0)
        {
            Vector3 lastBoxLocPos = items[numberOfRows - 1].transform.localPosition;
            float x = Item2.position.x;
            int bottomVisibleBoxNumber = Mathf.Clamp(CSP.AllowedBoxesToSee - 1, 0, numberOfRows - 1);
            float lastVisibleBoxLocY = items[bottomVisibleBoxNumber].transform.localPosition.y;
            float y = (transform.localPosition.y) + lastVisibleBoxLocY - lastBoxLocPos.y;
            float z = lastBoxLocPos.z;
            MarkerTemplate.localPosition = new Vector3(x, y, z);
        }

        GameObject emptyGO = new GameObject();
        emptyGO.name = "SecondMarker";
        Transform newTransform = emptyGO.transform;

        CSP.ClampObject = Instantiate(newTransform, MarkerTemplate.position, MarkerTemplate.rotation, CSP.transform.parent);
    }
}
