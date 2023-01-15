using System.Collections.Generic;
using UnityEngine;

public class BannerManager : MonoBehaviour
{
    [SerializeField] private List<Banner> _notActiveBanners;
    [SerializeField] private float _delay;
    [SerializeField] private int _maxBannerCount;

    private readonly List<Banner> _activeBanners = new();

    private float _lastSpawnTime = 0;

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
    }

    private void Update()
    {
        if (Time.time >= _lastSpawnTime + _delay) 
            OpenRandomBanner();
    }

    private void OpenRandomBanner() 
    {
        Banner banner;

        if (_activeBanners.Count >= _maxBannerCount) 
        {
            banner = _activeBanners[0];
            banner.Close();
        }

        banner = _notActiveBanners[Random.Range(0, _notActiveBanners.Count)];
        banner.Open();

        _notActiveBanners.Remove(banner);
        _activeBanners.Add(banner);
        _lastSpawnTime = Time.time;
    }

    public void ReturnBanner(Banner banner) 
    {
        _notActiveBanners.Add(banner);
        _activeBanners.Remove(banner);
    }

    public void CloseAll() 
    {
        List<Banner> temp = new(_activeBanners);

        foreach (Banner banner in temp)
        {
            banner.Close();
        }
    }
}