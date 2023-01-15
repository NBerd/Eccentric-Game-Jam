using UnityEngine;
using TMPro;
using System.Collections.Generic;

public class WritterWindow : MonoBehaviour
{
    [SerializeField] private Library _library;
    [SerializeField] private TextMeshProUGUI _text;
    [SerializeField] private string _succesColor;
    [SerializeField] private ScrollWindow _window;
    [SerializeField] private Sound _compliteSound;

    public string CurrentWord { get; private set; }

    private static List<string> CurrentWords;

    private string _currentWord;
    private int _currentCharId;

    private void Start()
    {
        _currentWord = GetRandomWord();

        _text.text = _currentWord;
    }

    private void UpdateLibrary()
    {
        CurrentWords = new(_library.Words);
    }

    private string GetRandomWord() 
    {
        if (CurrentWords == null || CurrentWords.Count == 0) 
            UpdateLibrary();

        string word = CurrentWords[Random.Range(0, CurrentWords.Count)];

        CurrentWords.Remove(word);

        return word.ToUpper();
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

        for (int i = 0; i < inputText.Length; i++)
        {
            char currentChar = _currentWord[_currentCharId];
            char inputChar = inputText[i];

            if (inputChar == currentChar) 
            {
                _currentCharId++;

                UpdateProggres();
            }
        }
    }

    private void UpdateProggres() 
    {
        string newText = $"<color={_succesColor}>" + _currentWord.Insert(_currentCharId, $"</color>");

        _text.text = newText;

        if (_currentCharId >= _currentWord.Length) 
        {
            _compliteSound.Play();

            _window.enabled = false;
            _window.Invoke(nameof(_window.CloseWindow), .2f);

            PlayerInput.OnType -= Write;
        }
    }

    public void SetDescriptionToLose() 
    {
        string description = new($"<color=#DD3554>{_currentWord}</color> NOT FOUND");

        GameManager.Instance.Defeat(description);
    }
}