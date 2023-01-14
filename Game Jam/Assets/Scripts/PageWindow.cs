using UnityEngine;

public class PageWindow : Window
{
    [SerializeField] private Vector2 _minSize, _maxSize;

    public override void Init(WindowSpawner spawner)
    {
        base.Init(spawner);

        SetRandomSize();
        SetStartPosition();
    }

    private void SetRandomSize() 
    {
        float sizeX = Random.Range(_minSize.x, _maxSize.x);
        float sizeY = Random.Range(_minSize.y, _maxSize.y);

        _transform.sizeDelta = new(sizeX, sizeY);
    }

    private void SetStartPosition() 
    {
        float deltaX = _transform.rect.width * _screenInfo.Scale / 2;
        float deltaY = _transform.rect.height * _screenInfo.Scale / 2;

        float minX = _screenInfo.PageBounds.min.x + deltaX;
        float maxX = _screenInfo.PageBounds.max.x - deltaX;

        float minY = _screenInfo.PageBounds.min.x + deltaY;
        float maxY = _screenInfo.PageBounds.max.y - deltaY;

        float positionX = Random.Range(minX, maxX);
        float positionY = Random.Range(minY, maxY);

        _transform.position = new Vector2(positionX, positionY);
        _transform.SetAsLastSibling();
    }
}