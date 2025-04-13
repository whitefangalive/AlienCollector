using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DetectPressing : MonoBehaviour
{
    public bool ButtonpressedRecently = false;
    
    public void Detect()
    {
        ButtonpressedRecently = true;
        StartCoroutine(StartTimer(2.0f));
    }
    private IEnumerator StartTimer(float timeToWait)
    {
        yield return new WaitForSeconds(timeToWait);
        ButtonpressedRecently = false;
    }
}
