using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;

[RequireComponent(typeof(Slider))]
public class AudioSlider : MonoBehaviour
{
    private const int VolumeMultiplier = 40;

    [SerializeField] private Muter _muter;
    [SerializeField] AudioMixerGroup _mixerGroup;

    private Slider _slider;
    private float _currentSound;

    private void Awake() =>
        Init();

    private void Start() =>
        ChangeVolume(_currentSound);

    private void OnEnable()
    {
        _slider.onValueChanged.AddListener(ChangeVolume);
        _muter.SoundOff += SoundOff;
        _muter.SoundOn += SoundOn;
    }

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
        _currentSound = _slider.value;
        ChangeSlider(_slider.minValue);
        _slider.interactable = false;
    }

    private void SoundOn() 
    {
        ChangeSlider(_currentSound);
        _slider.value = _currentSound;
        _slider.interactable = true;
    }

    private void Init()
    {
        _slider = GetComponent<Slider>();

        _currentSound = _slider.value;
    }
}