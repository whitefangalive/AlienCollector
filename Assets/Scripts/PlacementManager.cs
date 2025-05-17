using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlacementManager : MonoBehaviour
{
    public bool CurrentlyPlacing = false;
    public bool CurrentlyPlacingCow = false;
    public GameObject ObjectToPlace;
    public GameObject ExitButton;
    // Start is called before the first frame update
    private void FixedUpdate()
    {
        ExitButton.SetActive(CurrentlyPlacing);
    }
}
