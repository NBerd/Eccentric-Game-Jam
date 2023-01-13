using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System.Collections.Generic;

public class BannerWindow : AdWindow
{
    [SerializeField] private WindowAnimator _animator;
    [SerializeField] private Image _bannerImage;
    [SerializeField] private TextMeshProUGUI _bannerText;
    [SerializeField] private BannerInfoLibrary _library;

    private BannerManager _bannerManager;

    private static List<BannerInfo> _currentLibrary;

    public void SetSettings() 
    {
        if (_currentLibrary == null) 
            _currentLibrary = new(_library.Library);

        if (_bannerManager == null)
            _bannerManager = GetComponentInParent<BannerManager>();
    }

    public override void Init()
    {
        base.Init();

        gameObject.SetActive(true);

        _animator.SpawnAnimation();
        _transform.SetAsLastSibling();

        SetRandomInfo();
        SetStartPosition();
    }

    private void SetRandomInfo() 
    {
        if (_currentLibrary.Count == 0) 
            _currentLibrary = new(_library.Library);

        BannerInfo info = _currentLibrary[Random.Range(0, _currentLibrary.Count)];
        _currentLibrary.Remove(info);

        _bannerImage.sprite = info.Image;
        _bannerText.text = info.Text;
    }

    public override void CloseWindow()
    {
        gameObject.SetActive(false);

        _bannerManager.ReturnBanner(this);
    }
}