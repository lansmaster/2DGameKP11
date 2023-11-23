using UnityEngine;
using UnityEngine.SceneManagement;

public class FloorChanger : MonoBehaviour
{
    [SerializeField] private PlayerMover _player;
    [SerializeField] private bool _isUpper;
    [SerializeField] private Sprite _floorDoor, _floorDoor_Emission;

    private SpriteRenderer _spriteRenderer;

    public static Vector3 lastPlayerPositionBeforeTeleportation;

    private int _currentSceneIndex;
    private const float _interactionDistance = 1.5f;

    private void Start()
    {
        _spriteRenderer = GetComponent<SpriteRenderer>();

        _currentSceneIndex = SceneManager.GetActiveScene().buildIndex;
    }

    private void Update()
    {
        float currentDistance = Vector3.Distance(transform.position, _player.transform.position);
        if (currentDistance < _interactionDistance)
        {
            _spriteRenderer.sprite = _floorDoor_Emission;

            if (Input.GetKeyDown(KeyCode.E))
            {
                lastPlayerPositionBeforeTeleportation = _player.transform.position;

                if(_isUpper)
                {
                    Inventory.SaveSlots();
                    SceneManager.LoadScene(_currentSceneIndex + 1);
                }
                else
                {
                    Inventory.SaveSlots();
                    SceneManager.LoadScene(_currentSceneIndex - 1);
                }
            }
        }
        else
        {
            _spriteRenderer.sprite = _floorDoor;
        }
    }
}
