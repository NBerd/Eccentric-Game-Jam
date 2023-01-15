using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;

public class SoundManager : MonoBehaviour
{
    private const string MASTER_VOLUME = nameof(MASTER_VOLUME);
    private const string MUSIC_VOLUME = nameof(MUSIC_VOLUME);

    [SerializeField] private AudioMixerGroup _audioMixer;
    [SerializeField] private Slider _volumeSlider;

    private void Start()
    {
        _audioMixer.audioMixer.GetFloat(MASTER_VOLUME, out float volume);

        _volumeSlider.value = volume == 0 ? 1 : Mathf.Lerp(1, 0, Mathf.Abs(volume) / 80);

        ChangeMusicVolume(1);
    }

    public void OnSliderChanged() 
    {
        _audioMixer.audioMixer.SetFloat(MASTER_VOLUME, Mathf.Lerp(-80, 0, _volumeSlider.value));
    }

    public void ChangeMusicVolume(float value) 
    {
        _audioMixer.audioMixer.SetFloat(MUSIC_VOLUME, Mathf.Lerp(-80, 0, value));
    }
}