using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    [SerializeField] private int SceneIndex;

    public void PlayGame()
    {
        SceneManager.LoadScene(SceneIndex);
    }

    public void ExitGame()
    {
        Application.Quit();
    }
}
