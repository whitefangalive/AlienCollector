using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ClickBackgroundFunctions : MonoBehaviour
{
    public GameObject ExitSettingsButton;
    public GameObject LeaveMenuButton;
    public MoveObjectToLocation SettingsMovementScript;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    public void CloseItems()
    {
        if (SettingsMovementScript.atLocation == 1)
        {
            ExitSettingsButton.GetComponent<Button>().onClick.Invoke();
        } 
        else
        {
            LeaveMenuButton.GetComponent<Button>().onClick.Invoke();
        }
    }
}
