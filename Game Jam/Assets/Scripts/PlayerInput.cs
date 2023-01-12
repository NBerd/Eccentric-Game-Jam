using UnityEngine;
using System;

public class PlayerInput : MonoBehaviour
{
    public static event Action<string> OnType;

    private void Update()
    {
        string inputText = Input.inputString.ToUpper();

        if (Input.anyKeyDown && !string.IsNullOrEmpty(inputText)) 
        {
            OnType?.Invoke(inputText);
        }
    }
}