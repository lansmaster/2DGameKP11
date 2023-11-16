using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class FloorChanger : MonoBehaviour
{
    [SerializeField] private PlayerMover _player;
    [SerializeField] private bool _isUpper;

    public static Vector3 lastPlayerPositionBeforeMoving;

    private int _currentSceneIndex;
    private const float _interactionDistance = 1.5f;

    private void Start()
    {
        _currentSceneIndex = SceneManager.GetActiveScene().buildIndex;
    }

    private void Update()
    {
        float currentDistance = Vector3.Distance(transform.position, _player.transform.position);
        if (currentDistance < _interactionDistance)
        {
            if (Input.GetKeyDown(KeyCode.E))
            {
                lastPlayerPositionBeforeMoving = _player.transform.position;

                if(_isUpper)
                {
                    SceneManager.LoadScene(_currentSceneIndex + 1);
                }
                else
                {
                    SceneManager.LoadScene(_currentSceneIndex - 1);
                }
            }
        }
    }
}
