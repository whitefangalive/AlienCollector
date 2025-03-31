using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class updateScrapContainer : MonoBehaviour
{
    private PlayerStats ps;
    public TMP_Text textToUpdate;
    private GameObject pso;
    // Update is called once per frame
    void Update()
    {
        if (pso == null)
        {
            pso = GameObject.Find("SaveState");
        }
        if (pso != null && ps == null)
        {
            ps = pso.GetComponent<PlayerStats>();
        }
        textToUpdate.text = ps.Scrap.ToString();
    }
}
