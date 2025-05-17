using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class DetectPressing : MonoBehaviour, IPointerDownHandler, IPointerUpHandler
{
    public bool ButtonpressedRecently = false;
    public bool NobuttonPressing = false;
    public void Detect()
    {
        ButtonpressedRecently = true;
        StartCoroutine(StartTimer(2.0f));
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        if (NobuttonPressing)
        {
            ButtonpressedRecently = true;
        }
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        if (NobuttonPressing)
        {
            ButtonpressedRecently = false;
        }
    }

    private IEnumerator StartTimer(float timeToWait)
    {
        yield return new WaitForSeconds(timeToWait);
        ButtonpressedRecently = false;
    }
}
