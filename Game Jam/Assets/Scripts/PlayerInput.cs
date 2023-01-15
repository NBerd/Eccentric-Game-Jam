using UnityEngine;
using System;

public class PlayerInput : MonoBehaviour
{
    [SerializeField] private Sound _typeSound;

    public static event Action<string> OnType;

    private void Update()
    {
        string inputText = Input.inputString.ToUpper();

        if (Input.anyKeyDown && !string.IsNullOrEmpty(inputText)) 
        {
            _typeSound.Play();
            OnType?.Invoke(inputText);
        }
    }
}