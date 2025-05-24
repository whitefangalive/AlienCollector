using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class collisionEvent : MonoBehaviour
{
    public Button.ButtonClickedEvent onCollide;
    private void OnTriggerEnter2D(Collider2D collision)
    {
        onCollide.Invoke();
    }
}
