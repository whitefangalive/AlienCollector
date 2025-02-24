using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;


public class ChangeSceneTo : MonoBehaviour
{
    public bool fade = false;
    
    public void ChangeScene(string SceneName) 
    {
        playAudioClip(Resources.Load<AudioClip>("ButtonPress"));
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

    private void playAudioClip(AudioClip clip)
    {
        GameObject obj = new GameObject("AudioSource");
        AudioSource newSource = obj.AddComponent<AudioSource>();
        newSource.volume = PlayerPrefs.GetFloat("effectsVolume");
        newSource.playOnAwake = false;
        obj.AddComponent<DestroyOnAudioSourceEnd>();
        obj.AddComponent<dontDestroyOnLoad>();
        newSource.clip = clip;
        newSource.Play();
    }
}
