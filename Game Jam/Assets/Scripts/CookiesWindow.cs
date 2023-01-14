using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using System;

public class CookiesWindow : MonoBehaviour
{
    [SerializeField] private float _animationDuration;
    [SerializeField] private AnimationCurve _animationCurve;
    [SerializeField] private Image _backgroundImage;
    [SerializeField] private Color _startColor, _endColor;
    [SerializeField] private Button _acceptButton;

    private Action _callback;

    public void Open(Action onClose = null) 
    {
        _callback = onClose ?? null;

        gameObject.SetActive(true);

        StartCoroutine(FadeAnimation());
    }

    public void Close() 
    {
        _callback?.Invoke();

        gameObject.SetActive(false);
    }

    IEnumerator FadeAnimation() 
    {
        float timer = 0;

        while(timer <= _animationDuration) 
        {
            timer += Time.unscaledDeltaTime;
            float curveValue = _animationCurve.Evaluate(timer / _animationDuration);

            _backgroundImage.color = Color.Lerp(_startColor, _endColor, curveValue);

            yield return null;
        }

        _acceptButton.interactable = true;
    }
}