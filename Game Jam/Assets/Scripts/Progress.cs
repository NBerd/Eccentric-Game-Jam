using UnityEngine;
using UnityEngine.UI;
using System;

public class Progress : MonoBehaviour
{
    [SerializeField] private Image _proggressBar;

    public static event Action<float> OnProgressChange;

    public void SetProgress(float progress) 
    {
        _proggressBar.fillAmount = progress;

        OnProgressChange?.Invoke(progress);
    }
}