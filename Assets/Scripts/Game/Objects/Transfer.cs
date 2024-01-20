using UnityEngine;
using UnityEngine.SceneManagement;

public class Transfer : MonoBehaviour
{
    public static Transfer instance { get; private set; }

    private void Start()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);    
        }

        SceneManager.sceneLoaded += DestroyOnLoad;

    }

    private void DestroyOnLoad(Scene scene, LoadSceneMode loadSceneMode)
    {
        if (scene.buildIndex == 0)
        {
            Destroy(gameObject);
            SceneManager.sceneLoaded -= DestroyOnLoad;
        }
    }
}
