using UnityEngine;
using System;
using System.Collections.Generic;

public class GameManager : MonoBehaviour
{
    [SerializeField] private PlayerInput _input;
    [SerializeField] private BannerManager _bannerManager;
    [SerializeField] private WindowSpawner _mainSpawner;
    [SerializeField] private WritterWindow _writterWindow;
    [SerializeField] private Message _message;

    private Queue<GameState> _gameStates = new();
    private GameState _currentState;

    private void Start()
    {
        GenerateStory();

        ToNextState();
    }

    private void GenerateStory() 
    {
        GameState typeState = new(this, float.MaxValue, 4, () => 
        {
            _input.enabled = false;
            _message.Set("Ты ведь успеешь дописать материал до выступления?", () => 
            {
                _input.enabled = true;
                ToNextState();
            });
        });

        _gameStates.Enqueue(typeState);

        GameState wordState = new(this, float.MaxValue, 15, () => 
        {
            _input.enabled = false;
            _message.Set("Попробуй пошутить про говно. Всегда заходило.", () =>
            {
                _input.enabled = true;
                _writterWindow.Set("ГОВНО", () => 
                {
                    _mainSpawner.enabled = true;
                    ToNextState();
                });
                _writterWindow.gameObject.SetActive(true);
            });
        });

        _gameStates.Enqueue(wordState);

        GameState cookiesState = new(this, 0.1f, int.MaxValue, () => _bannerManager.enabled = true);

        _gameStates.Enqueue(cookiesState);
    }

    private void OnEnable()
    {
        Progress.OnProgressChange += OnProggressChange;
    }

    private void OnDisable()
    {
        Progress.OnProgressChange -= OnProggressChange;
    }

    private void OnProggressChange(float progress, int charCount) 
    {
        _currentState.UpdateProgress(progress, charCount);
    }

    public void ToNextState() 
    {
        if (_gameStates.Count == 0) 
            return;

        _currentState = _gameStates.Dequeue();
    }
}

public struct GameState 
{
    private GameManager _gameManager;
    private readonly float _progressToComplite;
    private readonly float _charCountToComplite;
    private readonly Action _callback;

    public GameState(GameManager gameManager, float progressToComplite, int charCountToComplite, Action onComplite = null) 
    {
        _gameManager = gameManager;
        _progressToComplite = progressToComplite;
        _charCountToComplite = charCountToComplite;

        _callback = onComplite ?? null;
    }

    public void UpdateProgress(float progress, int charCount) 
    {
        Debug.Log($"{progress} {_progressToComplite}");

        if (progress >= _progressToComplite || charCount >= _charCountToComplite) 
        {
            _callback?.Invoke();
        }
    }
}