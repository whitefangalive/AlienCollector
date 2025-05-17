using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ReferenceObjectLater : MonoBehaviour
{
    private PlayerStats ps;
    public string alienName;

    // Update is called once per frame
    void Update()
    {
        GameObject pso = GameObject.Find("SaveState");
        if (pso != null)
        {
            ps = pso.GetComponent<PlayerStats>();
        }

    }

    public void runOpeningAliens()
    {
        ps.gameObject.GetComponent<AlienInfoOpener>().OpeningAlien = alienName;
    }
}
