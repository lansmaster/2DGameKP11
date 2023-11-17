using System;
using TMPro;
using UnityEngine;

public class ChangingResolution : MonoBehaviour
{
    private Resolution[] _resolutions;
    private int _currentResolutionIndex;
    private bool _isfullScreen = true;
    [SerializeField] private TMP_Text _textResolution;

    private void Start()
    {
        _resolutions = Screen.resolutions;

        Screen.fullScreen = true;

        LoadSave();
    }

    public void ResolutionUp()
    {
        if (_currentResolutionIndex < _resolutions.Length - 1)
        {
            _currentResolutionIndex++;
        }
        _textResolution.text = $"{_resolutions[_currentResolutionIndex].width} x {_resolutions[_currentResolutionIndex].height} {_resolutions[_currentResolutionIndex].refreshRateRatio}Hz";
    }

    public void ResolutionDown()
    {
        if (_currentResolutionIndex > 0)
        {
            _currentResolutionIndex--;
        }
        _textResolution.text = $"{_resolutions[_currentResolutionIndex].width} x {_resolutions[_currentResolutionIndex].height} {_resolutions[_currentResolutionIndex].refreshRateRatio}Hz";
    }

    public void SetResolution()
    {
        Screen.SetResolution(_resolutions[_currentResolutionIndex].width, _resolutions[_currentResolutionIndex].height, _isfullScreen);

        PlayerPrefs.SetInt("CurrentResolutionIndex", _currentResolutionIndex);
    }

    public void FullScreenToggle()
    {
        _isfullScreen = !_isfullScreen;
        Screen.fullScreen = _isfullScreen;
    }

    //private void OnGUI()
    //{
    //    GUILayout.Label($"{_resolutions[_currentResolutionIndex].width} x {_resolutions[_currentResolutionIndex].height} {_resolutions[_currentResolutionIndex].refreshRateRatio}Hz");
    //}

    private void LoadSave()
    {
        if (PlayerPrefs.GetInt("CurrentResolutionIndex") != 0)
        {
            _currentResolutionIndex = PlayerPrefs.GetInt("CurrentResolutionIndex");
        }
        else
        {
            _currentResolutionIndex = 0;
        }
    }

}
