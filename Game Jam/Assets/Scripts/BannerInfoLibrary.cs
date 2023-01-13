using UnityEngine;

[CreateAssetMenu(fileName = "new BannerInfoLibrary", menuName = "BannerInfoLibrary")]
public class BannerInfoLibrary : ScriptableObject
{
    [SerializeField] private BannerInfo[] _bannerLibrary;

    public BannerInfo[] Library => _bannerLibrary;
}

[System.Serializable]
public struct BannerInfo 
{
    public Sprite Image;
    [TextArea] public string Text;
}