using UnityEngine;
using UnityEngine.SceneManagement;

public class Transfer : MonoBehaviour
{
    public static Transfer Instance { get; private set; }

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
            return;
        }
        Destroy(gameObject);
    }

    private void Start()
    {
        SceneManager.sceneLoaded += DestroyOnLoad;
    }

    private void DestroyOnLoad(Scene scene, LoadSceneMode loadSceneMode)
    {
        if (scene.buildIndex == 0)
        {
            SceneManager.sceneLoaded -= DestroyOnLoad;
            Destroy(gameObject);
        }
    }
}
