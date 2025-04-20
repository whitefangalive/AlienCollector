using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollectionFunctions : MonoBehaviour
{
    private PlayerStats ps;
    private void Update()
    {
        if (ps == null)
        {
            ps = GameObject.Find("SaveState").GetComponent<PlayerStats>();
        }
    }
    public void collectName(string alienName)
    {
        if (ps.DiscoveredAliens == null)
        {
            ps.DiscoveredAliens = new List<string>();
        }
        if (!ps.DiscoveredAliens.Contains(alienName))
        {
            ps.DiscoveredAliens.Add(alienName);
            Animator animator = GameObject.Find("MainAnimator").GetComponent<Animator>();
            animator.SetTrigger("Open");
        }

    }
}
