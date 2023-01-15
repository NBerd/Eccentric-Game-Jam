using System.Collections.Generic;
using UnityEngine;

public class WindowSpawner : MonoBehaviour
{
    [SerializeField] private Window _windowPrefab;
    [SerializeField] private float _spawnDelay;
    [SerializeField] private float _startSpawnDelay;
    [SerializeField] private float _windowsMaxCount;

    private readonly List<Window> _windows = new();

    private void Start()
    {
        InvokeRepeating(nameof(Spawn), _startSpawnDelay, _spawnDelay);
    }

    private void Spawn() 
    {
        if(_windows.Count >= _windowsMaxCount) 
            _windows[0].CloseWindow();

        Window window = Instantiate(_windowPrefab, transform);

        window.Init(this);

        _windows.Add(window);
    }

    public void ReturnWindow(Window window) 
    {
        _windows.Remove(window);
    }
}