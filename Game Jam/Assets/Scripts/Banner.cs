using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System.Collections.Generic;

public class Banner : MonoBehaviour
{
    [SerializeField] private BannerInfoLibrary _bannersLibrary;
    [SerializeField] private Image _bannerImage;
    [SerializeField] private TextMeshProUGUI _bannerText;
    [SerializeField] private WindowAnimator _animator;

    private static List<BannerInfo> CurrentLibrary;

    private void UpdateLibrary() 
    {
        CurrentLibrary = new(_bannersLibrary.Library);
    }

    private void SetRandomInfo() 
    {
        if (CurrentLibrary == null || CurrentLibrary.Count == 0) 
            UpdateLibrary();

        BannerInfo info = CurrentLibrary[Random.Range(0, CurrentLibrary.Count)];

        CurrentLibrary.Remove(info);

        _bannerImage.sprite = info.Image;
        _bannerText.text = info.Text;
    }

    public void Open() 
    {
        SetRandomInfo();

        gameObject.SetActive(true);

        _animator.SpawnAnimation();
    }

    public void Close() 
    {
        gameObject.SetActive(false);

        BannerManager.Instance.ReturnBanner(this);
    }

    public void OnClick() 
    {
        GameManager.Instance.Defeat("Стоило заниматся делом делом...");
    }
}