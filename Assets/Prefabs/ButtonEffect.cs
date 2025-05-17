
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class ButtonEffect
{
    [TextArea]
    public string OptionName;
    public UnityAction Effect;
    public ButtonEffect(string optionName, UnityAction effect)
    {
        OptionName = optionName;
        Effect = effect;
    }
}
