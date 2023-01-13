using System.Collections.Generic;
using UnityEngine;

public class BannerManager : MonoBehaviour
{
    [SerializeField] private int _maxBannerCount;
    [SerializeField] private float _startSpawnDelay;
    [SerializeField] private float _spawnDelay;
    [SerializeField] private BannerWindow[] _bannerPrefabs;

    private readonly Queue<BannerWindow> _activeWindows = new();
    private readonly List<BannerWindow> _notActiveWindows = new();

    private void Start()
    {
        InstantiateBanners();

        InvokeRepeating(nameof(SpawnBanner), _startSpawnDelay, _spawnDelay);
    }

    private void InstantiateBanners() 
    {
        for(int i = 0; i < _maxBannerCount; i++) 
        {
            BannerWindow banner = Instantiate(_bannerPrefabs[Random.Range(0, _bannerPrefabs.Length)], transform);
            banner.gameObject.SetActive(false);
            banner.SetSettings();

            _notActiveWindows.Add(banner);
        }
    }

    private void SpawnBanner() 
    {
        BannerWindow banner;

        if (_notActiveWindows.Count == 0) 
        {
            banner = _activeWindows.Dequeue();

            _notActiveWindows.Add(banner);
        }
        else 
            banner = _notActiveWindows[Random.Range(0, _notActiveWindows.Count)];

        banner.Init();

        _activeWindows.Enqueue(banner);
        _notActiveWindows.Remove(banner);
    }

    public void ReturnBanner(BannerWindow banner) 
    {
        _notActiveWindows.Add(banner);
    }
}