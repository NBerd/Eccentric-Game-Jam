using UnityEngine;

public class Screen : MonoBehaviour
{
    [SerializeField] private Canvas _canvas;

    public static ScreenInfo ScreenInfo { get; private set; }

    private void Start()
    {
        int screenWidth = UnityEngine.Screen.width;
        int screenHeight = UnityEngine.Screen.height;

        Vector2 screenSize = new(screenWidth, screenHeight);
        Vector2 screenPosition = new(screenWidth / 2, screenHeight / 2);

        ScreenInfo = new(new(screenPosition, screenSize), _canvas.scaleFactor);
    }
}

public struct ScreenInfo
{
    public Bounds Bounds;
    public float Scale;

    public ScreenInfo(Bounds bounds, float scale)
    {
        Bounds = bounds;
        Scale = scale;
    }
}