using TMPro;
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
        AudioManager.playAudioClip(Resources.Load<AudioClip>("Tap"));
        createSoundImage("Tap");

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


    public void createSoundImage(string word)
    {
        GameObject textTemplate = Instantiate(Resources.Load<GameObject>("SoundAnimation"), transform.position, transform.rotation);
        textTemplate.GetComponent<Rigidbody2D>().velocity = new Vector2(Random.Range(-10, 10), Random.Range(-10, 10));
        textTemplate.GetComponentInChildren<TMP_Text>().text = word;
        textTemplate.GetComponent<Animator>().speed = 1.5f;
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
