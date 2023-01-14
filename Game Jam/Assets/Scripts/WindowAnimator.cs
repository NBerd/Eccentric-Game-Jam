using System.Collections;
using UnityEngine;
using System;

public class WindowAnimator : MonoBehaviour
{
    [SerializeField] private AnimationCurve _animationCurve;
    [SerializeField] private float _animationDuration;
    [SerializeField] private Vector3 _startScale;
    [SerializeField] private Vector3 _endScale;

    private Coroutine _animationCoroutine;
    private RectTransform _transform;

    private Action _callback;

    private void Awake()
    {
        _transform = GetComponent<RectTransform>();
    }

    public void SpawnAnimation(Action onComplite = null) 
    {
        _callback = onComplite ?? null;

        if (_animationCoroutine != null) 
        {
            StopCoroutine(_animationCoroutine);
            _animationCoroutine = null;
        }

        _animationCoroutine = StartCoroutine(SpawnAnimationCoroutine());
    }

    IEnumerator SpawnAnimationCoroutine() 
    {
        float timer = 0;

        while(timer <= _animationDuration) 
        {
            timer += Time.unscaledDeltaTime;
            float curveValue = _animationCurve.Evaluate(timer / _animationDuration);

            _transform.localScale = Vector3.LerpUnclamped(_startScale, _endScale, curveValue);

            yield return null;
        }

        _animationCoroutine = null;
        _callback?.Invoke();
    }
}