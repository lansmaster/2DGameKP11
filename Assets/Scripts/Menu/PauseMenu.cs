using UnityEngine;
using UnityEngine.Events;
using UnityEngine.SceneManagement;

public class PauseMenu: MonoBehaviour
{
    [SerializeField] private GameObject _pauseMenu;
    [SerializeField] private GameObject _optionsMenu;
    
    private bool _isEnabled = false;

    public bool canOpen { get; set; }

    public static UnityAction SaveButtonClicked;
    public static UnityAction LoadButtonClicked;

    private void Update()
    {
        if (canOpen)
            if (Input.GetKeyDown(KeyCode.Escape))
                if (_isEnabled)
                    SetActive(false);
                else
                    SetActive(true);
    }

    public void ExitToMainMenu() // ������� �� ������
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene("MainMenu");
    }

    public void Save() // ������
    {
        SaveButtonClicked?.Invoke();
    }

    public void Load() // ������
    {
        LoadButtonClicked?.Invoke();
    }

    public void SetActive(bool active)
    {
        _pauseMenu.SetActive(active);
        Time.timeScale = active ? 0f : 1f;

        if (_optionsMenu.gameObject.activeSelf == true)
            _optionsMenu.SetActive(active);

        _isEnabled = active;
    }
}
