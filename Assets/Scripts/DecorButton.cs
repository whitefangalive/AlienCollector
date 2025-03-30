using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class DecorButton : MonoBehaviour
{
    public GameObject ContainedObject;
    public TMP_Text title;
    public Image image;
    private PlacementManager placementManager;
    public int cost = 0;
    public TMP_Text costText;
    private PlayerStats ps;
    // Start is called before the first frame update
    void Start()
    {
        GameObject pso = GameObject.Find("SaveState");
        if (pso != null)
        {
            ps = pso.GetComponent<PlayerStats>();
        }
        placementManager = GameObject.Find("PlacementManager").GetComponent<PlacementManager>();
        title.text = ContainedObject.name;
        image.sprite = ContainedObject.GetComponent<Image>().sprite;
        if (costText != null)
        {
            costText.text = cost.ToString();
        }
    }

    public void triggerPurchase()
    {
        if (ps.Scrap >= cost)
        {
            ps.Scrap -= cost;


        }
    }
    public void triggerPurchaseCow()
    {
        if (ps.Scrap >= cost)
        {
            ps.Scrap -= cost;
            if (ps.OwnedCows.ContainsKey(ContainedObject.name))
            {
                ps.OwnedCows.Add(ContainedObject.name, ps.OwnedCows[ContainedObject.name] + 1);
            }
            else
            {
                ps.OwnedCows.Add(ContainedObject.name, 1);
            }
            
        }
    }

    public void triggerPlacement()
    {
        placementManager.ObjectToPlace = ContainedObject;
        placementManager.CurrentlyPlacing = true;
    }

    public void triggerCowPlacement()
    {
        placementManager.ObjectToPlace = ContainedObject;
        placementManager.CurrentlyPlacingCow = true;
    }
}
