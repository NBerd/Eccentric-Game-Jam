using UnityEngine;

public class Window : MonoBehaviour
{
    protected ScreenInfo _screenInfo;
    protected RectTransform _transform;

    protected WindowSpawner _spawner;

    private void Awake()
    {
        _transform = GetComponent<RectTransform>();
    }

    public virtual void Init(WindowSpawner spawner)
    {
        _screenInfo = Screen.ScreenInfo;
    }

    public virtual void CloseWindow() 
    {
        Destroy(gameObject);
    }
}