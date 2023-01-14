using UnityEngine;

public class Screen : MonoBehaviour
{
    [SerializeField] private Canvas _canvas;
    [SerializeField] private RectTransform _page;

    public static ScreenInfo ScreenInfo { get; private set; }

    private void Start()
    {
        float scaleFactor = _canvas.scaleFactor;

        int screenWidth = UnityEngine.Screen.width;
        int screenHeight = UnityEngine.Screen.height;

        Vector2 screenSize = new(screenWidth, screenHeight);
        Vector2 screenPosition = new(screenWidth / 2, screenHeight / 2);

        Bounds pageBounds = new(_page.position, new(_page.rect.width * scaleFactor, _page.rect.height * scaleFactor));

        ScreenInfo = new(new(screenPosition, screenSize), pageBounds, scaleFactor);
    }
}

public struct ScreenInfo
{
    public Bounds ScreenBounds;
    public Bounds PageBounds;
    public float Scale;

    public ScreenInfo(Bounds screenBounds, Bounds pageBounds, float scale)
    {
        ScreenBounds = screenBounds;
        PageBounds = pageBounds;
        Scale = scale;
    }
}