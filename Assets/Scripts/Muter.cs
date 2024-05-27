using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class Muter : MonoBehaviour
{
    private const string Off = "O";
    private const string On = "X";

    private Button _muteButton;
    private TextMeshProUGUI _sign;

    private bool _isMuted;

    public event Action SoundOff;
    public event Action SoundOn;

    private void Awake() =>
        Init();
       
    private void OnEnable() =>
        _muteButton.onClick.AddListener(SwitchMuteState);

    private void OnDisable() =>
        _muteButton.onClick.RemoveListener(SwitchMuteState);

    private void SwitchMuteState()
    {
        _isMuted = !_isMuted;

        if (_isMuted)
            SwitchingMuteState(SoundOff, Off);
        else
            SwitchingMuteState(SoundOn, On);
    }

    private void SwitchingMuteState(Action action, string sign)
    {
        action?.Invoke();
        _sign.text = sign;
    }

    private void Init()
    {
        _muteButton = GetComponent<Button>();
        _sign = GetComponentInChildren<TextMeshProUGUI>();

        _isMuted = false;
        _sign.text = On;
    }
}