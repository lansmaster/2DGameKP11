using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ChangingResolution : MonoBehaviour
{
    private Resolution[] _resolutions;
    private int _currentResolutionIndex;

    private void Start()
    {
        _resolutions = Screen.resolutions;
        _currentResolutionIndex = 0;
    }

    public void ResolutionUp()
    {
        if (_currentResolutionIndex < _resolutions.Length)
        {
            _currentResolutionIndex++;
        }
    }

    public void ResolutionDown()
    {
        if (_currentResolutionIndex > 0)
        {
            _currentResolutionIndex--;
        }
    }

    public void SetResolution()
    {
        Screen.SetResolution(_resolutions[_currentResolutionIndex].width, _resolutions[_currentResolutionIndex].height, false);
    }

    private void OnGUI()
    {
        GUILayout.Label(_resolutions[_currentResolutionIndex].ToString());
    }
}
