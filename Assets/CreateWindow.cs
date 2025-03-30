using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using static UnityEngine.UI.Button;

public class CreateWindow : MonoBehaviour
{
    public GameObject window;
    public Transform ChildOf;
    private GameObject instance;

    public Button.ButtonClickedEvent YesButton;
    public Button.ButtonClickedEvent NoButton;

    public void WindowAppear()
    {
        if (instance == null)
        {
            instance = Instantiate(window, ChildOf.transform.position, ChildOf.transform.rotation, ChildOf);
        }
    }

    public void PurchaseAppear()
    {
        int quantity = 1;
        if (instance == null)
        {
            instance = Instantiate(window, ChildOf.transform.position, ChildOf.transform.rotation, ChildOf);
            DecorButton decor = GetComponent<DecorButton>();
            MenuScript menu = instance.GetComponent<MenuScript>();
            menu.mainImage.sprite = decor.ContainedObject.GetComponent<Image>().sprite;
            menu.mainText.text = "Purchase " + quantity.ToString() + " " + decor.ContainedObject.name;
            if (NoButton != null)
            {
                menu.noButton.onClick = NoButton;
            }
            
            if (YesButton != null)
            {
                menu.yesButton.onClick = YesButton;
            }
        }
    }

    public void WindowDisappear()
    {
        if (instance != null)
        {
            Destroy(instance);
        }
    }
}
