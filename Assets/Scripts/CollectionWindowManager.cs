using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CollectionWindowManager : MonoBehaviour
{
    private PlayerStats ps;

    private void Start()
    {
        ps = GetComponent<PlayerStats>();
    }
    private void FixedUpdate()
    {
        ps.SpawningTimer++;

        if (ps.SpawningTimer > 50000)
        {
            ps.SpawningTimer = 0;
        }
    }
}
