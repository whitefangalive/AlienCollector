using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class clickMovable : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler, IPointerDownHandler, IPointerUpHandler
{
    public Color Color = Color.white;
    public Color HoverColor = Color.gray;
    public float speed = 10f;
    public bool AttachedOffset = false;
    public float scaleAmount = 1.2f;
    public float scaleSpeed = 10f;
    public RectTransform canvas;
    public Button.ButtonClickedEvent OnGrab;
    public Button.ButtonClickedEvent OnLetGo;

    [Tooltip("If not empty will switch object to this parent when picked up then return once let go.")]
    public Transform objectToSwitchParent;

    private Image image;
    private RectTransform rectTransform;
    public bool clicking = false;
    private Vector3 startingScale;
    private Vector2 offset;
    private bool needsToBeShrunk = false;
    private Transform oldParent;

    private Vector3 lastTapperPosition;
    private Vector3 TapVelocity;
    private List<MoveCamera> moveCameras = new List<MoveCamera>();

    private void Start()
    {
        image = GetComponent<Image>();
        rectTransform = GetComponent<RectTransform>();
        startingScale = transform.localScale;
        oldParent = transform.parent;
        lastTapperPosition = Input.mousePosition;
        image.color = Color;

        //find top parent
        Transform parent = transform.parent;
        int deadmansSwitch = 0;
        while (canvas == null && parent != null && deadmansSwitch < 25)
        {
            deadmansSwitch++;
            if (parent.GetComponent<Canvas>() != null)
            {
                canvas = parent.GetComponent<RectTransform>();
            } 
            else
            {
                parent = parent.parent;
            }
        }

        //find cameras to lock;
        moveCameras = new List<MoveCamera>(GameObject.FindObjectsOfType<MoveCamera>());
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        image.color = HoverColor;
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        image.color = Color;
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        OnGrab.Invoke();
        foreach (MoveCamera mc in moveCameras)
        {
            mc.HoldingObject = true;
        }
        clicking = true;

        Vector2 localMousePos;
        RectTransformUtility.ScreenPointToLocalPointInRectangle(canvas, Input.mousePosition, Camera.main, out localMousePos);
        offset = (Vector2)rectTransform.localPosition - localMousePos;

        needsToBeShrunk = true;

        if (objectToSwitchParent != null)
        {
            transform.SetParent(objectToSwitchParent, true);
            transform.SetAsFirstSibling();
        }
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        clicking = false;
        foreach (MoveCamera mc in moveCameras)
        {
            mc.HoldingObject = false;
        }
    }

    private void FixedUpdate()
    {
        if (clicking)
        {
            TapVelocity = (Input.mousePosition - lastTapperPosition) / 10f;

            Vector2 localMousePos;
            RectTransformUtility.ScreenPointToLocalPointInRectangle(canvas, Input.mousePosition, Camera.main, out localMousePos);

            rectTransform.localScale = Vector3.Lerp(rectTransform.localScale, startingScale * scaleAmount, Time.deltaTime * scaleSpeed);

            if (AttachedOffset)
            {
                rectTransform.localPosition = localMousePos + offset;
            }
            else
            {
                rectTransform.localPosition = Vector2.Lerp(rectTransform.localPosition, localMousePos, Time.deltaTime * speed);
            }

            lastTapperPosition = Input.mousePosition;
        }
        else
        {
            
            if (needsToBeShrunk)
            {
                rectTransform.localScale = Vector3.Lerp(rectTransform.localScale, startingScale, Time.deltaTime * scaleSpeed);

                if (Vector3.Distance(rectTransform.localScale, startingScale) < 0.01f)
                {
                    needsToBeShrunk = false;
                    OnLetGo.Invoke();

                    if (objectToSwitchParent != null)
                    {
                        transform.SetParent(oldParent, true);
                        transform.SetAsFirstSibling();
                        transform.parent.SetAsLastSibling();
                    }
                }
            }
        }
    }

    public bool isAttached()
    {
        return clicking;
    }
}
