using UnityEngine;
using UnityEngine.UI;
using System;

public class CookiesWindow : MonoBehaviour
{
    [SerializeField] private Button _acceptButton;
    [SerializeField] private WindowAnimator _animator;

    private Action _callback;

    public void Open(Action onClose = null) 
    {
        gameObject.SetActive(true);

        _callback = onClose ?? null;
        _animator.SpawnAnimation(() => _acceptButton.interactable = true);
    }

    public void Close() 
    {
        _callback?.Invoke();

        gameObject.SetActive(false);
    }
}