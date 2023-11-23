using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(SpriteRenderer))]
[RequireComponent(typeof(BoxCollider2D))]
public class OpeningTheDoor : MonoBehaviour
{
    [SerializeField] private PlayerMover _player;
    [SerializeField] private Sprite _openedDoor, _closedDoor, _openedDoorEmission, _closedDoorEmission;
    [SerializeField] private Image _imgPressE;
    [SerializeField] private Sprite _pressEOpen, _pressEClose;

    private const float _interactionDistance = 1.5f;
    private bool _isOpened;

    private SpriteRenderer _spriteRenderer;
    private BoxCollider2D _boxCollider;

    private void Awake()
    {
        _spriteRenderer = GetComponent<SpriteRenderer>();
        _boxCollider = GetComponent<BoxCollider2D>();
    }

    private void Update()
    {
        float currentDistance = Vector3.Distance(transform.position, _player.transform.position);
        if (currentDistance < _interactionDistance)
        {
            _imgPressE.enabled = true;

            if (_boxCollider.enabled == true)
            {
                _imgPressE.sprite = _pressEOpen;
                _spriteRenderer.sprite = _closedDoorEmission;
                if (Input.GetKeyDown(KeyCode.E))
                {
                    MoveDoor();
                }
            }
            else
            {
                _imgPressE.sprite = _pressEClose;
                _spriteRenderer.sprite = _openedDoorEmission;
                if (Input.GetKeyDown(KeyCode.E))
                {
                    MoveDoor();
                }
            }
        }
        else
        {
            _imgPressE.enabled = false;

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
        }
        else
        {
            _spriteRenderer.sprite = _closedDoor;
            _boxCollider.enabled = true;
        }
    }
}