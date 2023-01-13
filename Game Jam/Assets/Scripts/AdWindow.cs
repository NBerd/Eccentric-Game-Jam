using UnityEngine;

public class AdWindow : Window
{
    public override void Init()
    {
        base.Init();

        SetStartPosition();
    }

    protected void SetStartPosition() 
    {
        float deltaX = _transform.sizeDelta.x * _screenInfo.Scale / 2;

        float minX = _screenInfo.PageBounds.min.x + deltaX;
        float maxX = _screenInfo.PageBounds.max.x - deltaX;

        float deltaY = _transform.sizeDelta.y * _screenInfo.Scale / 2;

        float minY = _screenInfo.PageBounds.min.y + deltaY;
        float maxY = _screenInfo.PageBounds.max.y - deltaY;

        float positionX = Random.Range(minX, maxX);
        float positionY = Random.Range(minY, maxY);

        transform.position = new(positionX, positionY);
    }
}