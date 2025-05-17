using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class CustomButton : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler, IPointerDownHandler, IPointerUpHandler
{
    public Color Color = Color.white;
    public Color HoverColor = Color.gray;
    public Button.ButtonClickedEvent OnGrab;
    public Button.ButtonClickedEvent OnLetGo;
    private Image image;
    public bool clicking = false;

    private Vector2 ClickerPositionOrigin = Vector2.zero;
    public RectTransform canvas;
    public string canvasName = null;
    public float moveSensativity = 0.5f;
    private void Start()
    {
        image = GetComponent<Image>();
        if (canvas == null)
        {
            canvas = FindCanvasAbove().GetComponent<RectTransform>();
        }
    }
    private Canvas FindCanvasAbove()
    {
        Canvas canv = null;
        Transform parent = transform;
        while (canv == null)
        {
            if (parent.GetComponent<Canvas>() != null)
            {
                canv = parent.GetComponent<Canvas>();
            } else
            {
                parent = parent.parent;
            }
        }
        return canv;
    }
    public void OnPointerEnter(PointerEventData eventData)
    {
        if (image != null)
            image.color = HoverColor;
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        if (image != null)
            image.color = Color;
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        OnGrab.Invoke();
        clicking = true;

        Vector2 localMousePos;
        RectTransformUtility.ScreenPointToLocalPointInRectangle(canvas, Input.mousePosition, Camera.main, out localMousePos);

        ClickerPositionOrigin = localMousePos;
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        Vector2 localMousePos;
        RectTransformUtility.ScreenPointToLocalPointInRectangle(canvas, Input.mousePosition, Camera.main, out localMousePos);
        if (Vector3.Distance(localMousePos, ClickerPositionOrigin) < moveSensativity)
        {
            clicking = false;
            OnLetGo.Invoke();
        }
    }
}
