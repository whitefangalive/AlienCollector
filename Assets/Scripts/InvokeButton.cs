using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InvokeButton : MonoBehaviour
{
    public Button.ButtonClickedEvent Event;
    public bool Activate;
    // Update is called once per frame
    void Update()
    {
        if (Activate)
        {
            Event.Invoke();
            Activate = false;
        }
    }

}
