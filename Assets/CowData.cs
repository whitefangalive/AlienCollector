using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CowData : MonoBehaviour
{
    public int risk = 0;
    private PlayerStats ps;
    public int defense = 1;
    public Scrollbar scrollbar;
    // Update is called once per frame
    void Update()
    {
        scrollbar.size = (risk * 0.01f);
        if (ps == null)
        {
            ps = GameObject.Find("SaveState").GetComponent<PlayerStats>();
        }
        int j = 0;
        if (risk >= 100)
        {
            for(int i = 0; i < ps.Cows.Count; i++)
            {
                if (ps.Cows[i].Item2 == risk)
                {
                    j = i; break;
                }
            }
            ps.Cows.RemoveAt(j);
            Destroy(gameObject);
        }
    }
}
