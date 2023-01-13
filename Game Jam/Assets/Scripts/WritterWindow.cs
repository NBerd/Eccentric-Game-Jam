using UnityEngine;
using TMPro;
using System.Collections.Generic;

public class WritterWindow : MonoBehaviour
{
    [SerializeField] private Library _library;
    [SerializeField] private TextMeshProUGUI _text;
    [SerializeField] private string _succesColor;

    private static List<string> CurrentWords;

    private string _currentWord;
    private int _currentCharId;

    private void Start()
    {
        _text.text = GetRandomWord();
    }

    private void UpdateLibrary() 
    {
        CurrentWords = new(_library.Words);
    }

    private string GetRandomWord() 
    {
        if (CurrentWords == null || CurrentWords.Count == 0) 
            UpdateLibrary();

        _currentWord = CurrentWords[Random.Range(0, CurrentWords.Count)].ToUpper();
        CurrentWords.Remove(_currentWord);

        return _currentWord;
    }

    private void OnEnable()
    {
        PlayerInput.OnType += Write;
    }

    private void OnDisable()
    {
        PlayerInput.OnType -= Write;
    }

    private void Write(string inputText)
    {
        if (_currentWord == null)
            return;

        char currentChar = _currentWord[_currentCharId];

        for (int i = 0; i < inputText.Length; i++)
        {
            char inputChar = inputText[i];

            if (inputChar == currentChar) 
            {
                _currentCharId++;

                UpdateProggres();
            }
        }

        if (_currentCharId >= _currentWord.Length)
            Destroy(gameObject);
    }

    private void UpdateProggres() 
    {
        string newText = $"<color={_succesColor}>" + _currentWord.Insert(_currentCharId, $"</color>");

        _text.text = newText;
    }
}