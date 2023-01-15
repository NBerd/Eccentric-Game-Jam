using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class WinScreen : MonoBehaviour
{
    [SerializeField] private GameObject _container;
    [SerializeField] private GameObject _overlay;
    [SerializeField] private Image _backgroundImage;
    [SerializeField] private TextMeshProUGUI _mainText;
    [SerializeField] private TextMeshProUGUI _gameText;
    [SerializeField] private float _animationDuration;
    [SerializeField] private AnimationCurve _animationCurve;
    [SerializeField] private Color _textStartColor, _textEndColor;
    [SerializeField] private Color _backgroundStartColor, _backgroundEndColor;

    public void Open()
    {
        _gameText.enabled = false;
        _container.SetActive(true);

        StartCoroutine(WinAnimation());
    }

    IEnumerator WinAnimation() 
    {
        float timer = 0;

        while(timer <= _animationDuration) 
        {
            timer += Time.unscaledDeltaTime;
            float curveValue = _animationCurve.Evaluate(timer / _animationDuration);

            _mainText.color = Color.Lerp(_textStartColor, _textEndColor, curveValue);
            _backgroundImage.color = Color.Lerp(_backgroundStartColor, _backgroundEndColor, curveValue);

            yield return null;
        }

        _overlay.SetActive(true);
    }
}