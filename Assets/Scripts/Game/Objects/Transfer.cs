using UnityEngine;
using UnityEngine.SceneManagement;

public class Transfer : MonoBehaviour
{
    private Transfer[] _gameObjects;

    private void Start()
    {
        _gameObjects = FindObjectsOfType<Transfer>();
        if (_gameObjects.Length > 1)
            Destroy(gameObject);
        else
            DontDestroyOnLoad(gameObject);

        //SceneManager.sceneLoaded += (scene, loadSceneMode) => DestroyOnLoad(scene, loadSceneMode);


    }

    //private void DestroyOnLoad(Scene scene, LoadSceneMode loadSceneMode)
    //{
    //    if (scene.buildIndex == 0)
    //        Destroy(gameObject);
    //}

    private void Update()
    {
        if (SceneManager.GetActiveScene().buildIndex == 0)
            Destroy(gameObject);
    }
}
