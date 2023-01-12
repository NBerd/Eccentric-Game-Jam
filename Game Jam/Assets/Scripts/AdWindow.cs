using UnityEngine;

public class AdWindow : Window
{
    public override void Init()
    {
        base.Init();

        SetStartPosition();
    }

    private void SetStartPosition() 
    {

        float deltaX = _transform.sizeDelta.x * _screenInfo.Scale / 2;

        float minX = _screenInfo.Bounds.min.x + deltaX;
        float maxX = _screenInfo.Bounds.max.x - deltaX;

        float deltaY = _transform.sizeDelta.y * _screenInfo.Scale / 2;

        float minY = _screenInfo.Bounds.min.y + deltaY;
        float maxY = _screenInfo.Bounds.max.y - deltaY;

        float positionX = Random.Range(minX, maxX);
        float positionY = Random.Range(minY, maxY);

        transform.position = new Vector2(positionX, positionY);
    }
}