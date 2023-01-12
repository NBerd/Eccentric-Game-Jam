using UnityEngine;

public class ScrollWindow : Window
{
    [SerializeField] private float _travelTime;

    private Vector2 _startPosition;
    private Vector2 _targetPosition;

    private float _timer = 0;

    public override void Init(Bounds screenBounds)
    {
        base.Init(screenBounds);

        SetStartPosition();
    }

    private void SetStartPosition() 
    {
        float positionX = Random.Range(_screenBounds.min.x + _transform.sizeDelta.x / 2, _screenBounds.max.x - _transform.sizeDelta.x / 2);
        float positionY = _screenBounds.max.y + _transform.sizeDelta.y / 2;

        transform.position = new Vector2(positionX, positionY);

        _startPosition = transform.position;
        _targetPosition = _startPosition;
        _targetPosition.y = _screenBounds.min.y - (_transform.sizeDelta.y / 2);
    }

    private void Update()
    {
        _timer += Time.deltaTime;

        transform.position = Vector2.Lerp(_startPosition, _targetPosition, _timer / _travelTime);

        if (transform.position.y == _targetPosition.y)
            Destroy(gameObject);
    }
}