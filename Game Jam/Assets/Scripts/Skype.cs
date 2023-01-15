using UnityEngine;
using UnityEngine.UI;

public class Skype : MonoBehaviour
{
    [SerializeField] private SoundManager _soundManager;
    [SerializeField] private WindowAnimator _fullWindow;
    [SerializeField] private WindowAnimator _smallWindow;
    [SerializeField] private Button[] _buttons;

    private void Start()
    {
        _fullWindow.gameObject.SetActive(true);
        _fullWindow.SpawnAnimation(() => 
        {
            _soundManager.ChangeMusicVolume(.4f);

            foreach(Button button in _buttons) 
            {
                button.interactable = true;
            }
        });
    }

    public void AcceptCall() 
    {
        _fullWindow.gameObject.SetActive(false);
        _smallWindow.gameObject.SetActive(true);
        _smallWindow.SpawnAnimation();

        Invoke(nameof(Close), 110f);
    }

    public void Close() 
    {
        _soundManager.ChangeMusicVolume(1f);

        gameObject.SetActive(false);
    }
}