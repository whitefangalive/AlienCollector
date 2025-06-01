using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class changeMaxVisibleCharacters : MonoBehaviour
{
    public TMP_Text m_Text;

    public void SetMaxVisibleCharacters(int maxVisibleCharacters)
    {
        m_Text.maxVisibleCharacters = maxVisibleCharacters;
    }

}
