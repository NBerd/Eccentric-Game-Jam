using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Image))]
public class CallAnimation : MonoBehaviour
{
    [SerializeField] private Vector3 _startScale, _endScale;
    [SerializeField] private Color _startColor, _endColor;
    [SerializeField] private float _animationDuration;
    [SerializeField] private AnimationCurve _animationCurve;

    private float _timer;
    private Image _image;

    private void Awake()
    {
        _image = GetComponent<Image>();
    }

    private void Update()
    {
        _timer += Time.deltaTime;
        float curveValue = _animationCurve.Evaluate(_timer / _animationDuration);

        _image.color = Color.Lerp(_startColor, _endColor, curveValue);
        _image.transform.localScale = Vector3.LerpUnclamped(_startScale, _endScale, curveValue);

        if (_timer >= _animationDuration) 
            Reset();
    }

    private void Reset()
    {
        _timer = 0;

        _image.transform.localScale = _startScale;
        _image.color = _startColor;
    }
}