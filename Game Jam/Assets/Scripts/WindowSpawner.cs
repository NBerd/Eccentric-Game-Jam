using System.Collections.Generic;
using UnityEngine;

public class WindowSpawner : MonoBehaviour
{
    [SerializeField] private Window _windowPrefab;
    [SerializeField] private float _delay;
    [SerializeField] private float _windowsMaxCount;

    private float _lastSpawnTime;

    private readonly List<Window> _windows = new();

    private void Update()
    {
        if (Time.time >= _lastSpawnTime + _delay) 
            Spawn();
    }

    private void Spawn() 
    {
        if(_windows.Count >= _windowsMaxCount) 
            _windows[0].CloseWindow();

        Window window = Instantiate(_windowPrefab, transform);

        window.Init(this);

        _windows.Add(window);
        _lastSpawnTime = Time.time;
    }

    public void ReturnWindow(Window window) 
    {
        _windows.Remove(window);
    }

    public void CloseAll() 
    {
        _lastSpawnTime = Time.time;

        List<Window> temp = new(_windows);

        foreach(Window window in temp) 
        {
            window.CloseWindow();
        }
    }
}