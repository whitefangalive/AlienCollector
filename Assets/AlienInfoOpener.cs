using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class AlienInfoOpener : MonoBehaviour
{
    public string OpeningAlien = null;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (OpeningAlien != null)
        {
            if (IsInScene("Catelog"))
            {
                foreach (AlienInfo info in new List<AlienInfo>(FindObjectsOfType<AlienInfo>()))
                {
                    if (info.nameText.text == OpeningAlien)
                    {
                        info.transform.parent.parent.GetComponent<Button>().onClick.Invoke();
                        OpeningAlien = null; 
                        break;
                    }
                }
            }
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
