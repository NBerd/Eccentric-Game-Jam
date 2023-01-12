using UnityEngine;
using TMPro;

public class WritterWindow : MonoBehaviour
{
    [SerializeField] private Library _library;
    [SerializeField] private TextMeshProUGUI _text;
    [SerializeField] private string _succesColor;

    private string _currentWord;
    private int _currentCharId;

    private void Start()
    {
        string[] words = _library.Words;

        _currentWord = words[Random.Range(0, words.Length)].ToUpper();
        _text.text = _currentWord;
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