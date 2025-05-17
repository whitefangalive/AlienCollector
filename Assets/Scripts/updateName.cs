using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class updateName : MonoBehaviour
{
    private TMP_Text text;
    // Start is called before the first frame update
    void Start()
    {
        text = GetComponent<TMP_Text>();
        text.text = transform.parent.parent.parent.name;
    }
}
