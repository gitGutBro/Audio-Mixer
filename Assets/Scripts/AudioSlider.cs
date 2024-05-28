using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;

[RequireComponent(typeof(Slider))]
public class AudioSlider : MonoBehaviour
{
    private const int VolumeMultiplier = 40;

    [SerializeField] private Muter _muter;
    [SerializeField] private AudioMixerGroup _mixerGroup;

    private Slider _slider;
    private float _currentVolume;

    private void Awake() =>
        Init();

    private void OnEnable()
    {
        _slider.onValueChanged.AddListener(ChangeVolume);
        _muter.SoundOff += SoundOff;
        _muter.SoundOn += SoundOn;
    }

    private void Start() =>
        ChangeVolume(_currentVolume);

    private void OnDisable()
    {
        _slider.onValueChanged.RemoveListener(ChangeVolume);
        _muter.SoundOff -= SoundOff;
        _muter.SoundOn -= SoundOn;
    }

    private void ChangeVolume(float volume) =>
        _mixerGroup.audioMixer.SetFloat(_mixerGroup.name, Mathf.Log10(volume) * VolumeMultiplier);

    private void ChangeSlider(float volume) =>
        _slider.value = volume;

    private void SoundOff()
    {
        _currentVolume = _slider.value;
        ChangeSlider(_slider.minValue);
        _slider.interactable = false;
    }

    private void SoundOn() 
    {
        ChangeSlider(_currentVolume);
        _slider.value = _currentVolume;
        _slider.interactable = true;
    }

    private void Init()
    {
        _slider = GetComponent<Slider>();

        _currentVolume = _slider.value;
    }
}