using System.Collections.Generic;
using UnityEngine;

public class BannerManager : MonoBehaviour
{
    [SerializeField] private List<Banner> _notActiveBanners;
    [SerializeField] private float _delay;
    [SerializeField] private float _startDelay;

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
        InvokeRepeating(nameof(OpenRandomBanner), _startDelay, _delay);
    }

    private void OpenRandomBanner() 
    {
        Banner banner = null;

        if (_notActiveBanners.Count == 0 && _activeBanners.Count > 0) 
        {
            banner = _activeBanners.Dequeue();
            banner.Close();
        }

        banner = banner == null ? _notActiveBanners[Random.Range(0, _notActiveBanners.Count)] : banner;
        banner.Open();

        _notActiveBanners.Remove(banner);
        _activeBanners.Enqueue(banner);
    }

    public void ReturnBanner(Banner banner) 
    {
        _notActiveBanners.Add(banner);
    }
}