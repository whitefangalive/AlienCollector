using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class OfferingsSpawner : MonoBehaviour
{
    public Transform Item1;
    public Transform Item2;
    public GameObject TemplateItem;
    public Transform MarkerTemplate;

    private PlayerStats ps;
    private Vector3 distance;
    public List<GameObject> items = new List<GameObject>();

    private void Update()
    {
        GameObject pso = GameObject.Find("SaveState");
        if (pso != null)
        {
            ps = pso.GetComponent<PlayerStats>();
        }
    }
    public void spawn()
    {

        foreach(GameObject it in items)
        {
            Destroy(it);
        }
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


        for (int i = 0; i < ps.AlienGifts.Count; i++)
        {
            items.Add(Instantiate(TemplateItem, Item1.position - (distance * i), Item1.rotation, transform));
            Tuple<string, int> itemG = ps.AlienGifts[i];
            items[i].SetActive(true);
            items[i].GetComponentInChildren<OfferingsContainer>().costText.text = itemG.Item2.ToString();
            Debug.Log("Aliens/" + itemG.Item1);
            GameObject alienImg = Resources.Load<GameObject>("Aliens/" + itemG.Item1);
            
            items[i].GetComponentInChildren<OfferingsContainer>().image.sprite = alienImg.GetComponentInChildren<Button>().targetGraphic.gameObject.GetComponent<Image>().sprite;
            items[i].GetComponentInChildren<OfferingsContainer>().nameText.text = alienImg.GetComponentInChildren<AlienData>().alienName;
            items[i].GetComponentInChildren<OfferingsContainer>().number = i;
        }


        //set amount for dragging menu

        ClickSlidePosition CSP = transform.parent.GetComponent<ClickSlidePosition>();
        if (ps.AlienGifts.Count > 0)
        {
            MarkerTemplate.localPosition = new Vector3(
                items[ps.AlienGifts.Count - 1].transform.localPosition.x,
               (transform.localPosition.y) + ((items[Mathf.Clamp(CSP.AllowedBoxesToSee - 1, 0, items.Count - 1)].transform.localPosition.y) - (items[ps.AlienGifts.Count - 1].transform.localPosition.y)),
                items[ps.AlienGifts.Count - 1].transform.localPosition.z);
        }

        GameObject emptyGO = new GameObject();
        emptyGO.name = "SecondMarker";
        Transform newTransform = emptyGO.transform;
        
        CSP.ClampObject = Instantiate(newTransform, MarkerTemplate.position, MarkerTemplate.rotation, CSP.transform.parent);
    }
}
