using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(SpriteRenderer))]
[RequireComponent(typeof(BoxCollider2D))]
public class OpeningTheDoor : MonoBehaviour
{
    [SerializeField] private PlayerMover _player;
    [SerializeField] private Sprite _closedDoor, _openedDoor, _closedDoorEmission, _openedDoorEmission;

    private const float _interactionDistance = 1f;
    public bool isOpened;

    private SpriteRenderer _spriteRenderer;
    private BoxCollider2D _boxCollider;
    private PolygonCollider2D _polygonCollider;

    private void Start()
    {
        _spriteRenderer = GetComponent<SpriteRenderer>();
        _boxCollider = GetComponent<BoxCollider2D>();
        _polygonCollider = GetComponent<PolygonCollider2D>();

        _polygonCollider.enabled = false;
    }

    private void Update()
    {
        float currentDistance = Vector3.Distance(transform.position, _player.transform.position);
        if (currentDistance < _interactionDistance)
        {
            if (_boxCollider.enabled == true)
            {
                _spriteRenderer.sprite = _closedDoorEmission;
                if (Input.GetKeyDown(KeyCode.E))
                {
                    MoveDoor();
                }
            }
            else
            {
                _spriteRenderer.sprite = _openedDoorEmission;
                if (Input.GetKeyDown(KeyCode.E))
                {
                    MoveDoor();
                }
            }
        }
        else
        {
            if (_boxCollider.enabled == true)
            {
                _spriteRenderer.sprite = _closedDoor;
            }
            else
            {
                _spriteRenderer.sprite = _openedDoor;
            }
        }
    }

    private void MoveDoor()
    {
        if (_boxCollider.enabled == true)
        {
            _spriteRenderer.sprite = _openedDoor;
            _boxCollider.enabled = false;
            _polygonCollider.enabled = true;

            isOpened = true;
        }
        else
        {
            _spriteRenderer.sprite = _closedDoor;
            _boxCollider.enabled = true;
            _polygonCollider.enabled = false;

            isOpened = false;
        }
    }
}
