using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DecorData : MonoBehaviour
{
    public List<GameObject> PossibleAliensToSpawn = new List<GameObject>();
    public List<GameObject> PossibleAliensToSpawnMoon = new List<GameObject>();
    public GameObject AlienAttached;
    private Image image;
    // Start is called before the first frame update
    void Start()
    {
        image = GetComponent<Image>();
    }

    // Update is called once per frame
    void Update()
    {
        if (AlienAttached == null)
        {
            image.color = Color.white;
        } 
        else
        {
            image.color = new Color(image.color.r, image.color.g, image.color.b, 0);
        }
    }
}
