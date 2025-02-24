using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class FadeEffect : MonoBehaviour
{

    public CanvasGroup canvasGroup;
    public bool fadeOut = false;
    public bool fadeIn = false;

    public float TimeToFade;

    public string SceneGoingTo;
    // Start is called before the first frame update
    void Start()
    {
        fadeOut = true;
    }

    // Update is called once per frame
    void Update()
    {
        if (fadeOut)
        {

            if (canvasGroup.alpha < 1)
            {
                canvasGroup.alpha += TimeToFade * Time.deltaTime;
                if (canvasGroup.alpha >= 1)
                {
                    fadeOut = false;
                    if (SceneGoingTo != null)
                    {
                        transform.SetParent(null, true);
                        GameObject g = new GameObject("Black", typeof(Image));
                        g.transform.SetParent(GameObject.Find("Canvas").transform, true);
                        g.GetComponent<Image>().color = Color.black;
                        g.GetComponent<RectTransform>().localScale = new Vector3(500, 500, 1);
                        DontDestroyOnLoad(gameObject);
                        SceneManager.LoadScene(SceneGoingTo);
                        fadeIn = true;


                    }
                }
            }
        }

        if (fadeIn)
        {
            if (amInNewScene())
            {
                transform.SetParent(GameObject.Find("Canvas").transform, true);
            }
            
            if (canvasGroup.alpha >= 0)
            {
                canvasGroup.alpha -= TimeToFade * Time.deltaTime;
                if (canvasGroup.alpha == 0)
                {
                    fadeIn = false;

                    Destroy(gameObject);
                }
            }
        }
    }

    private bool amInNewScene()
    {
        int dontDestroySceneIndex = SceneManager.GetSceneByName("DontDestroyOnLoad").buildIndex;

        // Iterate through all loaded scenes
        for (int i = 0; i < SceneManager.sceneCount; i++)
        {
            Scene scene = SceneManager.GetSceneAt(i);
            if (scene.buildIndex != dontDestroySceneIndex)
            {
                if (scene.name == SceneGoingTo)
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
