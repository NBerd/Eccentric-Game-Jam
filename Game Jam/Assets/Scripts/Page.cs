using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class Page : MonoBehaviour
{
    [SerializeField] private PlayerInput _input;
    [SerializeField] private Button _reloadButton;
    [SerializeField] private GameObject _loadingScreen;
    [SerializeField] private float _loadDuration;

    private GameManager _gameManager;

    private WaitForSeconds _delay;
    private Coroutine _loading;

    private void Start()
    {
        _delay = new WaitForSeconds(_loadDuration);

        _gameManager = GameManager.Instance;

        Reload();
    }

    public void Reload() 
    {
        if (_gameManager.IsPlaying == false) 
            _gameManager.Reload();

        if (_loading != null) 
            return;

        _reloadButton.interactable = false;

        _input.enabled = false;
        _loadingScreen.SetActive(true);

        _loading = StartCoroutine(LoadingCoroutine());
    }


    IEnumerator LoadingCoroutine() 
    {
        yield return _delay;
        
        _input.enabled = true;
        _loadingScreen.SetActive(false);
        _loading = null;

        _reloadButton.interactable = true;
    }
}