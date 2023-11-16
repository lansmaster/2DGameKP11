using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseMenu: MonoBehaviour
{
    [SerializeField] private GameObject _pauseMenu;

    private bool _isEnabled = false;

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (_isEnabled)
            {
                _pauseMenu.SetActive(false);
                Time.timeScale = 1f;
                _isEnabled = false;
            }
            else
            {
                _pauseMenu.SetActive(true);
                Time.timeScale = 0f;
                _isEnabled = true;
            }
        }   
    }

    public void ExitToMainMenu()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene("MainMenu");
    }
}
