using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MassObjectEditor : MonoBehaviour
{
    public List<string> objectTags = new List<string>();
    
    public void SetAllActive(bool Active)
    {
        foreach (string tag in objectTags)
        {
            foreach (var obj in GameObject.FindGameObjectsWithTag(tag))
            {
                obj.SetActive(Active);
            }
        }
    }
}
