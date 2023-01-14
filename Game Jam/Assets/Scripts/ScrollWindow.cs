using UnityEngine;

public class ScrollWindow : Window
{
    [SerializeField] private float _travelTime;

    private Vector2 _startPosition;
    private Vector2 _targetPosition;

    private float _timer = 0;

    public override void Init()
    {
        base.Init();

        SetPosition();
    }

    private void SetPosition() 
    {
        float deltaX = _transform.sizeDelta.x * _screenInfo.Scale / 2;
        float deltaY = _transform.sizeDelta.y * _screenInfo.Scale / 2;

        float minX = _screenInfo.PageBounds.min.x + deltaX;
        float maxX = _screenInfo.PageBounds.max.x - deltaX;

        float positionX = Random.Range(minX, maxX);
        float positionY = _screenInfo.PageBounds.max.y + deltaY;

        transform.position = new Vector2(positionX, positionY);

        _startPosition = transform.position;
        _targetPosition = _startPosition;
        _targetPosition.y = _screenInfo.ScreenBounds.min.y - deltaY;
    }

    private void Update()
    {
        _timer += Time.deltaTime;

        transform.position = Vector2.Lerp(_startPosition, _targetPosition, _timer / _travelTime);

        if (transform.position.y == _targetPosition.y)
            CloseWindow();
    }
}