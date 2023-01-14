using System.Collections.Generic;
using UnityEngine;

public class BannerManager : MonoBehaviour
{
    [SerializeField] private List<Banner> _notActiveBanners;
    [SerializeField] private float _delay;
    [SerializeField] private int _maxBannerCount;

    private readonly Queue<Banner> _activeBanners = new();

    #region Singleton

    public static BannerManager Instance;

    private void Awake()
    {
        if (Instance != null) 
            Destroy(Instance.gameObject);

        Instance = this;
    }

    #endregion

    private void Start()
    {
        _maxBannerCount = _maxBannerCount > _notActiveBanners.Count ? _notActiveBanners.Count : _maxBannerCount;

        InvokeRepeating(nameof(OpenRandomBanner), 0, _delay);
    }

    private void OpenRandomBanner() 
    {
        Banner banner;

        if (_activeBanners.Count >= _maxBannerCount) 
        {
            banner = _activeBanners.Dequeue();
            banner.Close();
        }

        banner = _notActiveBanners[Random.Range(0, _notActiveBanners.Count)];
        banner.Open();

        _notActiveBanners.Remove(banner);
        _activeBanners.Enqueue(banner);
    }

    public void ReturnBanner(Banner banner) 
    {
        _notActiveBanners.Add(banner);
    }
}