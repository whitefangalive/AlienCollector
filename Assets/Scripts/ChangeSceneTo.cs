using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;


public class ChangeSceneTo : MonoBehaviour
{
    public bool fade = false;
    
    public void ChangeScene(string SceneName) 
    {
        if (IsInScene("Station"))
        {
            //GameObject.Find("SaveState").GetComponent<SaveManager>().saveData();
        }
        //playAudioClip(Resources.Load<AudioClip>("ButtonPress"));
        if (!fade)
        {
            SceneManager.LoadScene(SceneName);
        } 
        else
        {
            GameObject fader = Instantiate(Resources.Load("Fader") as GameObject, GameObject.Find("Canvas").transform);
            fader.GetComponent<FadeEffect>().SceneGoingTo = SceneName;
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
