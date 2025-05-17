using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class DialogueManager : MonoBehaviour
{
    public TextMeshProUGUI dialogueText;
    public RectTransform window;

    private string currentText;
    private string nextText;

    private bool runChecker = false;

    private TypewriterEffect typewriterSettings;
    private float defaultspeed;
    private Action endOfTextEvent = null;
    private GameObject ContinueWarning;
    public float waitTimeTillWarning = 5;
    private float currentTime;
    private List<AudioClip> CurrentVoiceSamples = new List<AudioClip>();
    private GameObject dialogueChoicesBox;

    private int AlreadyPlayed = 0;
    // Start is called before the first frame update
    void Start()
    {
        currentTime = waitTimeTillWarning;
        if (dialogueText == null)
        {
            dialogueText = GameObject.Find("DiaText").GetComponent<TextMeshProUGUI>();
        }
        if (window == null)
        {
            window = GameObject.Find("DialogueBox").GetComponent<RectTransform>();
        }
        typewriterSettings = dialogueText.GetComponent<TypewriterEffect>();
        ContinueWarning = window.GetChild(1).gameObject;
        ContinueWarning.SetActive(false);
        
        hideBox();
    }

    public void showBox(string textToShow)
    {
        CurrentVoiceSamples.Clear();
        endOfTextEvent = null;
        window.gameObject.SetActive(true);
        dialogueText.text = textToShow;
        runChecker = true;
        dialogueText.maxVisibleCharacters = 0;
        ContinueWarning.SetActive(false);
        currentTime = waitTimeTillWarning;
    }
    public void showBox(string textToShow, Action dialogueEndEvent)
    {
        CurrentVoiceSamples.Clear();
        window.gameObject.SetActive(true);
        dialogueText.text = textToShow;
        runChecker = true;
        dialogueText.maxVisibleCharacters = 0;
        ContinueWarning.SetActive(false);
        currentTime = waitTimeTillWarning;
        endOfTextEvent = dialogueEndEvent;
    }
    public void showBox(string textToShow, Action dialogueEndEvent, List<AudioClip> voiceSamples)
    {
        if (window != null)
        {
            window.gameObject.SetActive(true);
        }
        
        dialogueText.text = textToShow;
        runChecker = true;
        dialogueText.maxVisibleCharacters = 0;
        //cannot check if null, it breaks for some reason

        if (ContinueWarning == null)
        {
            if (window == null)
            {
                throw new Exception("Loaded out of order . . . Handling: Fixed");
            }
            if (window != null)
            {
                ContinueWarning = window.GetChild(1).gameObject;
            }
            
        }
        
        ContinueWarning.SetActive(false);

        



        currentTime = waitTimeTillWarning;
        endOfTextEvent = dialogueEndEvent;
        CurrentVoiceSamples = voiceSamples;
        
    }
    public void showBox(string textToShow, List<AudioClip> voiceSamples)
    {
        if (window != null)
        {
            window.gameObject.SetActive(true);
        }

        dialogueText.text = textToShow;
        runChecker = true;
        dialogueText.maxVisibleCharacters = 0;
        //cannot check if null, it breaks for some reason
        ContinueWarning.SetActive(false);

        currentTime = waitTimeTillWarning;
        CurrentVoiceSamples = voiceSamples;
    }
    public void hideBox()
    {
        defaultspeed = typewriterSettings.charactersPerSecond;
        dialogueText.text = default;
        window.gameObject.SetActive(false);
        currentText = null;
        nextText = null;
        runChecker = false;
        if (dialogueChoicesBox != null)
        {
            Destroy(dialogueChoicesBox);
        }
        if (endOfTextEvent != null)
        {
            endOfTextEvent.Invoke();
        }
    }
    public void DialougeOption(List<ButtonEffect> optionList)
    {
        dialogueChoicesBox = Instantiate(Resources.Load<GameObject>("DialogueBoxChoices"), window.transform.position, window.transform.rotation, gameObject.transform);
        Transform buttons = dialogueChoicesBox.transform.GetChild(0);
        if (optionList.Count <= 4)
        {
            for (int i = 0; i < optionList.Count; i++)
            {
                buttons.GetChild(i).GetComponentInChildren<TMP_Text>().text = optionList[i].OptionName;
                buttons.GetChild(i).GetComponent<Button>().onClick.AddListener(optionList[i].Effect);
            }
            for (int i = 0; i < (4 - optionList.Count); i++)
            {
                Destroy(buttons.GetChild(3 - i).gameObject);
            }
        }
        
        
    }

    private void Update()
    {
        if (CurrentVoiceSamples.Count > 0 && dialogueText.text != default && dialogueText.maxVisibleCharacters <= dialogueText.text.Length - 1)
        {
            if (typewriterSettings.character == ' ' || dialogueText.maxVisibleCharacters == 0)
            {
                if (AlreadyPlayed < 2)
                {
                    GameObject obj = new GameObject("AudioSource");
                    obj.transform.parent = transform;
                    AudioSource newSource = obj.AddComponent<AudioSource>();
                    newSource.volume = 0.4f;
                    newSource.playOnAwake = false;
                    obj.AddComponent<DestroyOnAudioSourceEnd>();
                    newSource.clip = CurrentVoiceSamples[UnityEngine.Random.Range(0, CurrentVoiceSamples.Count)];
                    newSource.pitch = UnityEngine.Random.Range(0.95f, 1.05f);
                    newSource.Play();
                    AlreadyPlayed++;
                }
            }
            else
            {
                AlreadyPlayed = 0;
            }
        }

        if (dialogueText.text != default && dialogueText.maxVisibleCharacters >= dialogueText.text.Length - 1)
        {
            currentTime -= Time.deltaTime;
        }
        if (currentTime <= 0)
        {
            ContinueWarning.SetActive(true);
        }

        if (Input.anyKeyDown && dialogueText.text != default && Input.GetAxisRaw("Horizontal") == 0 && Input.GetAxisRaw("Vertical") == 0)
        {
            if (dialogueText.maxVisibleCharacters >= dialogueText.text.Length - 1)
            {
                
                if (nextText != null)
                {
                    if (endOfTextEvent == null)
                    {
                        showBox(nextText);
                    } else
                    {
                        if (CurrentVoiceSamples != null)
                        {
                            showBox(nextText, endOfTextEvent, CurrentVoiceSamples);
                        }
                        else
                        {
                            showBox(nextText, endOfTextEvent);
                        }
                        
                    }
                    
                    currentText = null;
                    nextText = null;
                } else
                {
                    
                    hideBox();
                }
            } else
            {
                typewriterSettings.Skip(true);
            }
        }
        if (runChecker && dialogueText.textInfo.lineCount >= 5)
        {
            string textToShowString = dialogueText.text;
            int nextLineIndex = dialogueText.textInfo.lineInfo[4].firstCharacterIndex;
            currentText = textToShowString.Substring(0, nextLineIndex);
            nextText = textToShowString.Substring(nextLineIndex, textToShowString.Length - nextLineIndex);
            dialogueText.text = currentText;
            dialogueText.maxVisibleCharacters = 0;
            runChecker = false;
        }
    }
    private bool IsInScene(string sceneName)
    {
        Scene ddol = SceneManager.GetSceneByName("DontDestroyOnLoad");
        if (ddol == null)
        {
            return SceneManager.GetSceneAt(0).name == sceneName;
        }
        int dontDestroySceneIndex = ddol.buildIndex;

        // Iterate through all loaded scenes
        for (int i = 0; i < SceneManager.sceneCount; i++)
        {
            Scene scene = SceneManager.GetSceneAt(i);
            if (scene.buildIndex != dontDestroySceneIndex)
            {
                if (scene.name == sceneName)
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
        }
        return false;
    }
}
