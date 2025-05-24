using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExistIfApplicationType : MonoBehaviour
{
    public RuntimePlatform runtimePlatform = RuntimePlatform.Android;
    public GameObject obj;
    // Start is called before the first frame update
    void Start()
    {
        if (Application.platform == runtimePlatform) 
        {
            obj.SetActive(true);
        } else
        {
            obj.SetActive(false);
        }
    }

}
