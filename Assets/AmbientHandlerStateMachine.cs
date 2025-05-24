using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AmbientHandlerStateMachine : MonoBehaviour
{
    public List<GameObject> states = new List<GameObject>();
    private List<GameObject> CreatedSates = new List<GameObject>();
    private int prevState = -1;
    private PlayerStats ps;
    private void initalize()
    {
        states = new List<GameObject>(Resources.LoadAll<GameObject>("AmbientSounds"));
        for (int i = 0; i < states.Count; i++)
        {
            GameObject go = states[i];
            CreatedSates.Add(Instantiate(go, transform));
            PauseOrPlayState(i, false);
        }
    }
    //used in referenceLater
    public void loopStates()
    {
        if (ps.MusicState > CreatedSates.Count)
        { ps.MusicState++;} else { ps.MusicState = 0; }
    }

    // Update is called once per frame
    void Update()
    {
        GameObject pso = GameObject.Find("SaveState");
        if (pso != null && ps == null)
        {
            ps = pso.GetComponent<PlayerStats>();
            initalize();
        }

        if (ps != null)
        {
            //setting musicStateCount because it needs to be referenced by referenceLater
            ps.MusicStateCount = CreatedSates.Count;

            //if a change update all states other then current state to pause.
            //then play the current state
            if (ps.MusicState != prevState)
            {
                for (int i = 0; i < CreatedSates.Count; i++)
                {
                    if (ps.MusicState != i)
                    {
                        PauseOrPlayState(i, false);
                    }
                }
                PauseOrPlayState(ps.MusicState, true);
                prevState = ps.MusicState;
            }
        }
    }

    private void PauseOrPlayState(int state, bool play)
    {
        foreach(AudioSource AS in CreatedSates[state].GetComponentsInChildren<AudioSource>())
        {
            if (play) 
            {
                if (AS.isPlaying)
                {
                    AS.UnPause();
                } 
                else
                {
                    AS.Play();
                }
            }
            else { AS.Pause(); }
        }
    }
}
