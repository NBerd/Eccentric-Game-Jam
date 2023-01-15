using UnityEngine;
using System;
using System.Collections.Generic;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    [SerializeField] private PlayerInput _input;
    [SerializeField] private BannerManager _bannerManager;
    [SerializeField] private WindowSpawner _mainSpawner;
    [SerializeField] private WindowSpawner _pageSpawner;
    [SerializeField] private CookiesWindow _cookiesWindow;
    [SerializeField] private Message _message;
    [SerializeField] private Skype _skype;
    [SerializeField] private WinScreen _winScreen;
    [SerializeField] private LoseScreen _loseScreen;

    private Queue<GameState> _gameStates = new();
    private GameState _currentState;

    public bool IsPlaying { get; private set; } = true;

    #region Singleton

    public static GameManager Instance;

    private void Awake()
    {
        if (Instance != null) 
            Destroy(Instance.gameObject);

        Instance = this;
    }

    #endregion

    private void Start()
    {
        GenerateStory();

        ToNextState();
    }

    private void GenerateStory() 
    {
        GameState typeState = new(float.MaxValue, 10, () => 
        {
            ToNextState();
            _input.enabled = false;

            _message.Set("Ты ведь успеешь дописать материал до выступления?", () => _input.enabled = true);
        });

        _gameStates.Enqueue(typeState);

        GameState wordState = new(float.MaxValue, 50, () => 
        {
            ToNextState();
            _input.enabled = false;

            _message.Set("Попробуй шутить про говно, или что-то в этом роде. Людям нравится", () =>
            {
                _input.enabled = true;
                _mainSpawner.enabled = true;
            });
        });

        _gameStates.Enqueue(wordState);

        GameState cookiesState = new(.25f, int.MaxValue, () => 
        {
            ToNextState();

            _input.enabled = false;
            Time.timeScale = 0.1f;

            _cookiesWindow.Open(() =>
            {
                _input.enabled = true;
                _bannerManager.enabled = true;
                _pageSpawner.enabled = true;
                Time.timeScale = 1;

                _message.Set("Всё в порядке?");
            });
        });

        _gameStates.Enqueue(cookiesState);

        GameState skypeState = new(.35f, int.MaxValue, () =>
        {
            ToNextState();

            _skype.enabled = true;
        });

        _gameStates.Enqueue(skypeState);

        GameState winState = new(1, int.MaxValue, () => 
        {
            IsPlaying = false;

            _mainSpawner.enabled = false;
            _bannerManager.enabled = false;
            _pageSpawner.enabled = false;
            _pageSpawner.CloseAll();

            _winScreen.Open();

            ToNextState();
        });

        _gameStates.Enqueue(winState);
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
        if (_currentState == null) 
            return;

        _currentState.UpdateProgress(progress, charCount);
    }

    public void ToNextState() 
    {
        if (_gameStates.Count == 0) 
        {
            _currentState = null;

            return;
        }

        _currentState = _gameStates.Dequeue();
    }

    public void Defeat(string cause = null) 
    {
        if (IsPlaying == false) 
            return;

        _loseScreen.Open(cause);

        _mainSpawner.enabled = false;
        _bannerManager.enabled = false;
        _pageSpawner.enabled = false;

        IsPlaying = false;
    }

    public void QuitGame()
    {
        Application.Quit();
    }

    public void Reload() 
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
}

public class GameState 
{
    private readonly float _progressToComplite;
    private readonly float _charCountToComplite;
    private readonly Action _callback;

    public GameState(float progressToComplite, int charCountToComplite, Action onComplite = null) 
    {
        _progressToComplite = progressToComplite;
        _charCountToComplite = charCountToComplite;

        _callback = onComplite ?? null;
    }

    public void UpdateProgress(float progress, int charCount) 
    {
        if (progress >= _progressToComplite || charCount >= _charCountToComplite) 
            _callback?.Invoke();
    }
}