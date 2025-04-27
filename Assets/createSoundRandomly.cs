using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class createSoundRandomly : MonoBehaviour
{
    public int MinFrequency = 5;
    public int MaxFrequency = 10;
    private float time = 0;
    private int rand = 0;
    public AudioClip sound;
    public string word;
    // Start is called before the first frame update
    void Start()
    {
        
    }
    private void FixedUpdate()
    {
        time += 0.02f;
        if (time == rand)
        {
            rand = Random.Range(MinFrequency, MaxFrequency);
            if (word != null)
            {
                createSoundImage(word);
            }
            AudioManager.playAudioClip(sound);
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void createSoundImage(string word)
    {
        GameObject textTemplate = Instantiate(Resources.Load<GameObject>("SoundAnimation"), transform.position, transform.rotation, transform);
        textTemplate.GetComponent<Rigidbody2D>().velocity = new Vector2 (Random.Range(-1, 1), Random.Range(-1, 1));
        textTemplate.GetComponentInChildren<TMP_Text>().text = word;
    }
}
