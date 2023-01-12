using UnityEngine;

public class WindowSpawner : MonoBehaviour
{
    [SerializeField] private Window _windowPrefab;
    [SerializeField] private float _spawnDelay;
    [SerializeField] private float _startSpawnDelay;

    private Bounds _screenBounds;

    private void Start()
    {
        int screenWidth = Screen.width;
        int screenHeight = Screen.height;

        Vector2 screenSize = new(screenWidth, screenHeight);
        Vector2 screenPosition = new(screenWidth / 2, screenHeight / 2);
        _screenBounds = new(screenPosition, screenSize);

        InvokeRepeating(nameof(Spawn), _startSpawnDelay, _spawnDelay);
    }

    private void Spawn() 
    {
        Window window = Instantiate(_windowPrefab, transform);

        window.Init(_screenBounds);
    }
}