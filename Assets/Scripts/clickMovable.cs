using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine; 
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class clickMovable : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler, IPointerDownHandler, IPointerUpHandler
{
    public Color Color = Color.white;
    public Color HoverColor = Color.gray;
    private Image image;
    private bool clicking = false;

    public float speed = 100f;
    public bool AttachedOffset = false;
    public float scaleAmount = 1f;
    public float scaleSpeed = 1f;

    private Vector3 startingScale;
    private Vector3 offset;
    private bool needsToBeShrunk;

    [Tooltip("If not empty will switch object to this parent when picked up then return once let go.")]
    public Transform objectToSwitchParent;
    private Transform oldParent;
    private void Start()
    {
        image = GetComponent<Image>();
        image.color = Color;
        startingScale = transform.localScale;
        oldParent = transform.parent;
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
        playAudioClip(Resources.Load<AudioClip>("CardGrab"));
        clicking = true;
        offset = Input.mousePosition - transform.position;
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
        playAudioClip(Resources.Load<AudioClip>("CardDrop"));
    }

    private void playAudioClip(AudioClip clip)
    {
        GameObject obj = new GameObject("AudioSource");
        AudioSource newSource = obj.AddComponent<AudioSource>();
        newSource.volume = PlayerPrefs.GetFloat("effectsVolume");
        newSource.pitch = Random.Range(1.10f, 1.22f);
        newSource.playOnAwake = false;
        obj.AddComponent<DestroyOnAudioSourceEnd>();
        obj.AddComponent<dontDestroyOnLoad>();
        newSource.clip = clip;
        newSource.Play();
    }
    private void FixedUpdate()
    {
        if (clicking)
        {
            transform.localScale = new Vector3(Mathf.Lerp(transform.localScale.x, startingScale.x * scaleAmount, Time.deltaTime * scaleSpeed),
                                           Mathf.Lerp(transform.localScale.y, startingScale.y * scaleAmount, Time.deltaTime * scaleSpeed), transform.localScale.z);
            if (AttachedOffset)
            {
                transform.position = Input.mousePosition - offset;
            }
            else
            {
                transform.position = new Vector3(Mathf.Lerp(transform.position.x, Input.mousePosition.x, Time.deltaTime * speed),
                                                 Mathf.Lerp(transform.position.y, Input.mousePosition.y, Time.deltaTime * speed), transform.position.z);
            }
        }
        else
        {
            if (needsToBeShrunk)
            {
                transform.localScale = new Vector3(Mathf.Lerp(transform.localScale.x, startingScale.x, Time.deltaTime * scaleSpeed),
                                           Mathf.Lerp(transform.localScale.y, startingScale.y, Time.deltaTime * scaleSpeed), transform.localScale.z);
                if (transform.localScale.x - startingScale.x < 0.001)
                {
                    needsToBeShrunk = false;
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
