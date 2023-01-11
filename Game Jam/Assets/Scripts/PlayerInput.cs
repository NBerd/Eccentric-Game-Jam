using UnityEngine;
using System;

public class PlayerInput : MonoBehaviour
{
    public static event Action<string> OnType;

    private void Update()
    {
        string inputText = Input.inputString;

        if (Input.anyKeyDown && !string.IsNullOrEmpty(inputText)) 
        {
            Debug.Log(inputText);

            OnType?.Invoke(inputText);
        }
    }
}