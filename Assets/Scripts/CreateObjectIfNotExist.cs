using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CreateObjectIfNotExist : MonoBehaviour
{
    public GameObject obj;
    // Start is called before the first frame update
    void Start()
    {
       if (obj != null && GameObject.Find(obj.name) == null)
        {
            GameObject obj1 = Instantiate(obj, transform.position, transform.rotation, transform);
            obj1.name = obj.name;
        }
    }
}
