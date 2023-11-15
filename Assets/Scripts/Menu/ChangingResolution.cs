using System;
using TMPro;
using UnityEngine;

public class ChangingResolution : MonoBehaviour
{
    private Resolution[] _resolutions;
    private RefreshRate[] _refreshRate;
    private int _currentResolutionIndex;
    private int _currentRefreshRateIndex;
    private bool _isfullScreen = true;
    [SerializeField] private TMP_Text _textResolution;
    [SerializeField] private TMP_Text _textRefreshRate;

    private void Start()
    {
        _resolutions = GetResolutions();
        _refreshRate = GetRefreshRates();

        Screen.fullScreen = true;

        LoadSave();
    }

    public void ResolutionUp()
    {
        if (_currentResolutionIndex < _resolutions.Length - 1)
        {
            _currentResolutionIndex++;
        }
        _textResolution.text = $"{_resolutions[_currentResolutionIndex].width} x {_resolutions[_currentResolutionIndex].height}";
    }

    public void ResolutionDown()
    {
        if (_currentResolutionIndex > 0)
        {
            _currentResolutionIndex--;
        }
        _textResolution.text = $"{_resolutions[_currentResolutionIndex].width} x {_resolutions[_currentResolutionIndex].height}";
    }

    public void RefreshRateUp()
    {
        if (_currentRefreshRateIndex < _refreshRate.Length - 1)
        {
            _currentRefreshRateIndex++;
        }
        _textRefreshRate.text = $"{_refreshRate[_currentRefreshRateIndex]}Hz";
    }

    public void RefreshRateDown()
    {
        if (_currentRefreshRateIndex > 0)
        {
            _currentRefreshRateIndex--;
        }
        _textRefreshRate.text = $"{_refreshRate[_currentRefreshRateIndex]}Hz";
    }

    public void SetResolution()
    {
        if(_isfullScreen)
        {
            Screen.SetResolution(_resolutions[_currentResolutionIndex].width, _resolutions[_currentResolutionIndex].height, FullScreenMode.ExclusiveFullScreen, _refreshRate[_currentRefreshRateIndex]);
        }
        else
        {
            Screen.SetResolution(_resolutions[_currentResolutionIndex].width, _resolutions[_currentResolutionIndex].height, false);
        }
        PlayerPrefs.SetInt("CurrentResolutionIndex", _currentResolutionIndex);
    }

    public void FullScreenToggle()
    {
        _isfullScreen = !_isfullScreen;
        Screen.fullScreen = _isfullScreen;
    }

    private void OnGUI()
    {
        GUILayout.Label($"{_resolutions[_currentResolutionIndex].width} x {_resolutions[_currentResolutionIndex].height} {Screen.currentResolution.refreshRateRatio}Hz");
    }

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

        _currentRefreshRateIndex = 0;
    }

    private Resolution[] GetResolutions()
    {
        Resolution[] resolutions = new Resolution[Screen.resolutions.Length];

        for (int i = 0; i < resolutions.Length; i++)
        {
            if (resolutions[i].width != Screen.resolutions[i].width && resolutions[i].height != Screen.resolutions[i].height)
            {
                resolutions[i] = Screen.resolutions[i];
            }
        }
        return resolutions;
    }

    private RefreshRate[] GetRefreshRates()
    {
        RefreshRate[] refreshRates = new RefreshRate[Screen.resolutions.Length];

        for (int i = 0; i < refreshRates.Length; i++)
        {
            refreshRates[i] = Screen.resolutions[i].refreshRateRatio;
        }
        return refreshRates;
    }
}
