using System;
using UnityEngine;

[RequireComponent(typeof(DialogueWindow))]
public class CooldownTag : MonoBehaviour, ITag
{
    public void Calling(string value)
    {
        float number = (float)Convert.ToDouble(value.Replace(".", ","));

        var dialogueWindow = GetComponent<DialogueWindow>();

        try
        {
            dialogueWindow.SetCoolDown(number);
        }
        catch(ArgumentException ex )
        {
            Debug.LogError(ex.Message);
        }
    }
}
