using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Image))]
public class LoadAnimation : MonoBehaviour
{
    private Image _image;

    private float _value;
    private int _diraction = 1;

    private void Start()
    {
        _image = GetComponent<Image>();
    }

    private void Update()
    {
        _value += Time.deltaTime * _diraction;
        _value = Mathf.Clamp01(_value);

        _image.fillAmount = _value;

        if (_value == 0 || _value == 1) 
            ChangeDiraction();
    }

    private void ChangeDiraction() 
    {
        _diraction = -_diraction;
        _image.fillClockwise = _diraction > 0;
    }
}