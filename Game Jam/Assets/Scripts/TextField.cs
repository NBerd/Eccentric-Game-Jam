using UnityEngine;
using TMPro;

public class TextField : MonoBehaviour
{
    private const char CURSOR_CHAR_0 = ' ';
    private const char CURSOR_CHAR = '|';
    private const float CURSOR_UPDATE_TIME = .75f;

    [TextArea(10, 25)]
    [SerializeField] private string _text;
    [SerializeField] private TextMeshProUGUI _textField;
    [SerializeField] private int _charPerClick;
    [SerializeField] private Progress _progress;

    private int _currentIndex;
    private string _currentText;

    private void Start()
    {
        _currentText = _textField.text;

        InvokeRepeating(nameof(UpdateCursor), 0, CURSOR_UPDATE_TIME);
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
        _currentIndex += _charPerClick;
        _currentIndex = _currentIndex > _text.Length ? _text.Length : _currentIndex;

        _currentText = _text.Substring(0, _currentIndex);

        _progress.SetProgress((float)_currentText.Length / _text.Length, _currentIndex);

        UpdateCursor();
    }

    private void UpdateCursor()
    {
        char lastChar = _currentText[^1];

        if (lastChar == CURSOR_CHAR)
            _currentText = _currentText[0..^1] + CURSOR_CHAR_0;
        else if (lastChar == CURSOR_CHAR_0)
            _currentText = _currentText[0..^1] + CURSOR_CHAR;
        else 
            _currentText += CURSOR_CHAR;


        UpdateText();
    }

    private void UpdateText() 
    {
        _textField.text = _currentText;
    }
}