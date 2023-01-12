using UnityEngine;

public class Window : MonoBehaviour
{
    protected ScreenInfo _screenInfo;
    protected RectTransform _transform;

    private void Awake()
    {
        _transform = GetComponent<RectTransform>();
    }

    public virtual void Init()
    {
        _screenInfo = Screen.ScreenInfo;
    }

    public virtual void CloseWindow() 
    {
        Destroy(gameObject);
    }
}