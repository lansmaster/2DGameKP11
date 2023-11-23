using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    [SerializeField] private int _sceneIndex;

    public void PlayGame()
    {
        SceneManager.LoadScene(_sceneIndex);
    }

    public void ExitGame()
    {
        Application.Quit();
        
        Debug.Log(" нопка выход");
    }
}
