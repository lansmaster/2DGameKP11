using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseMenu: MonoBehaviour
{
    [SerializeField] private GameObject _pauseMenu;
    [SerializeField] private GameObject _optionsMenu;

    private bool _isEnabled = false;

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (_isEnabled)
            {
                _pauseMenu.SetActive(false);
                Time.timeScale = 1f;
                if(_optionsMenu.gameObject.activeSelf == true)
                {
                    _optionsMenu.SetActive(false);
                }
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

    public void ExitToMainMenu() // повесил на кнопку
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene("MainMenu");
    }
}
