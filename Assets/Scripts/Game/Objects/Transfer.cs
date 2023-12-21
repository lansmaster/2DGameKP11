using UnityEngine;
using UnityEngine.SceneManagement;

public class Transfer : MonoBehaviour
{
    private int _currentSceneIndex;

    private void Start()
    {
        DontDestroyOnLoad(gameObject);
    }

    private void Update()
    {
        _currentSceneIndex = SceneManager.GetActiveScene().buildIndex;

        if (_currentSceneIndex == 0)
            Destroy(gameObject);
    }
}
