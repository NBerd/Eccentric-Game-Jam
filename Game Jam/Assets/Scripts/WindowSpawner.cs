using UnityEngine;

public class WindowSpawner : MonoBehaviour
{
    [SerializeField] private Window _windowPrefab;
    [SerializeField] private float _spawnDelay;
    [SerializeField] private float _startSpawnDelay;

    private void Start()
    {
        InvokeRepeating(nameof(Spawn), _startSpawnDelay, _spawnDelay);
    }

    private void Spawn() 
    {
        Window window = Instantiate(_windowPrefab, transform);

        window.Init(this);
    }
}