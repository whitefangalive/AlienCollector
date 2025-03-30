using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TimeEvent : MonoBehaviour
{
    public Button.ButtonClickedEvent Event;

    public void StartTimerSeconds(float timeToWait)
    {
        StartCoroutine(StartTimer(timeToWait));
    }
    private IEnumerator StartTimer(float timeToWait)
    {
        yield return new WaitForSeconds(timeToWait);
        Event.Invoke();
    }
}
