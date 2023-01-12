using UnityEngine;

public class Window : MonoBehaviour
{
    protected Bounds _screenBounds;
    protected RectTransform _transform;

    private void Awake()
    {
        _transform = GetComponent<RectTransform>();
    }

    public virtual void Init(Bounds screenBounds)
    {
        _screenBounds = screenBounds;
    }
}