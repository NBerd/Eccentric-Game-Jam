using UnityEngine;
using TMPro;
using System.Collections.Generic;
using System;

public class WritterWindow : MonoBehaviour
{
    [SerializeField] private Library _library;
    [SerializeField] private TextMeshProUGUI _text;
    [SerializeField] private string _succesColor;

    private static List<string> CurrentWords;

    private string _currentWord;
    private int _currentCharId;

    private Action _callback;

    private void Start()
    {
        if (_currentWord == null) 
            Set();
    }

    public void Set(string startWord = null, Action onClose = null)
    {
        _currentWord = startWord ?? GetRandomWord();
        _callback = onClose ?? null;
    }

    private void UpdateLibrary()
    {
        CurrentWords = new(_library.Words);
    }

    private string GetRandomWord() 
    {
        if (CurrentWords == null || CurrentWords.Count == 0) 
            UpdateLibrary();

        string word = CurrentWords[UnityEngine.Random.Range(0, CurrentWords.Count)].ToUpper();

        CurrentWords.Remove(word);

        return word;
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
            Close();
    }

    private void UpdateProggres() 
    {
        string newText = $"<color={_succesColor}>" + _currentWord.Insert(_currentCharId, $"</color>");

        _text.text = newText;
    }

    private void Close() 
    {
        _callback?.Invoke();
        Destroy(gameObject);
    }
}