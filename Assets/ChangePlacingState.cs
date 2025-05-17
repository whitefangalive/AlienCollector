using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChangePlacingState : MonoBehaviour
{
    public PlacementManager placementManager;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame


    public void updatePlacingState(bool tof)
    {
        placementManager.CurrentlyPlacing = tof;
    }
}
