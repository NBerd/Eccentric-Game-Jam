using UnityEngine;

[RequireComponent(typeof(AudioSource))]
public class Sound : MonoBehaviour
{
    [SerializeField] private float _minPitch, _maxPitch;

    private AudioSource _sound;

    private void Awake()
    {
        _sound = GetComponent<AudioSource>();
    }

    public void Play()
    {
        float pitch = Random.Range(_minPitch, _maxPitch);

        _sound.pitch = pitch;
        _sound.Play();
    }
}