using UnityEngine;
using UnityEngine.SceneManagement;

[RequireComponent(typeof(SpriteRenderer))]
public class FloorChanger : MonoBehaviour
{
    [SerializeField] private bool _isUpper;
    [SerializeField] private Sprite _floorDoor, _floorDoor_Emission;

    private Player _player;

    private SpriteRenderer _spriteRenderer;

    public static Vector3 lastPlayerPositionBeforeTeleportation;

    private int _currentSceneIndex;
    private const float _interactionDistance = 1f;

    private void Start()
    {
        _player = FindObjectOfType<Player>();
        _spriteRenderer = GetComponent<SpriteRenderer>();

        _currentSceneIndex = SceneManager.GetActiveScene().buildIndex;
    }

    private void Update()
    {
        float currentDistance = Vector3.Distance(transform.position, _player.Position);
        if (currentDistance < _interactionDistance)
        {
            _spriteRenderer.sprite = _floorDoor_Emission;

            if (Input.GetKeyDown(KeyCode.E))
            {
                lastPlayerPositionBeforeTeleportation = _player.Position;

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
        else
        {
            _spriteRenderer.sprite = _floorDoor;
        }
    }
}
