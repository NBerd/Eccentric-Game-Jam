using System.Collections;
using UnityEngine;
using TMPro;
using System;

public class Message : MonoBehaviour
{
    [SerializeField] private float _closeDelay;
    [SerializeField] private float _animationDuration;
    [SerializeField] private AnimationCurve _animationCurve;
    [SerializeField] private Vector3 _startPositon, _endPosition;
    [SerializeField] private RectTransform _transform;
    [SerializeField] private CanvasGroup _canvasGroup;
    [SerializeField] private TextMeshProUGUI _messageText;

    private Coroutine _currentAnimation;
    private Action _callback;

    public void Set(string messageText, Action onClose = null) 
    {
        _callback = onClose ?? null;

        if (_currentAnimation != null) 
        {
            StopCoroutine(_currentAnimation);
            _currentAnimation = null;
        }

        Reset();

        _messageText.text = messageText;

        StartCoroutine(PopUpAnimation());
    }

    private void Reset()
    {
        _transform.anchoredPosition = _startPositon;
        _canvasGroup.alpha = 0;

        gameObject.SetActive(true);
    }

    public void Close() 
    {
        if (_currentAnimation != null) 
        {
            StopCoroutine(_currentAnimation);
            _currentAnimation = null;
        }

        _callback?.Invoke();

        gameObject.SetActive(false);
    }

    IEnumerator PopUpAnimation() 
    {
        float timer = 0;

        while(timer <= _animationDuration) 
        {
            timer += Time.deltaTime;
            float curveValue = _animationCurve.Evaluate(timer / _animationDuration);

            _canvasGroup.alpha = Mathf.Lerp(0, 1, curveValue);
            _transform.anchoredPosition = Vector3.Lerp(_startPositon, _endPosition, curveValue);

            yield return null;
        }

        _currentAnimation = StartCoroutine(FadeAnimation());
    }

    IEnumerator FadeAnimation() 
    {
        yield return new WaitForSeconds(_closeDelay);

        float timer = 0;

        while(timer <= _animationDuration) 
        {
            timer += Time.deltaTime;
            float curveValue = _animationCurve.Evaluate(timer / _animationDuration);

            _canvasGroup.alpha = Mathf.Lerp(1, 0, curveValue);

            yield return null;
        }

        Close();
    }
}