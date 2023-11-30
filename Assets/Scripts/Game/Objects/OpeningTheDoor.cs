using UnityEngine;

[RequireComponent(typeof(SpriteRenderer))]
[RequireComponent(typeof(BoxCollider2D))]
public class OpeningTheDoor : MonoBehaviour
{
    [SerializeField] private PlayerActions _player;
    [SerializeField] private Sprite _closedDoor, _openedDoor, _closedDoorEmission, _openedDoorEmission;

    public bool isOpened;
    private float _interactionDistance = 1f;

    private SpriteRenderer _spriteRenderer;
    private BoxCollider2D _boxCollider;
    private PolygonCollider2D _polygonCollider;

    private void Start()
    {
        _spriteRenderer = GetComponent<SpriteRenderer>();
        _boxCollider = GetComponent<BoxCollider2D>();
        _polygonCollider = GetComponent<PolygonCollider2D>();

        _player.DoorDetected += DoorActons;
    }

    private void DoorActons(bool DoorDetected)
    {
        //float currentDistance = Vector3.Distance(transform.position, _player.transform.position);
        //DoorDetected = _interactionDistance > currentDistance;

        if (DoorDetected)
        {
            if (_boxCollider.enabled == true)
            {
                _spriteRenderer.sprite = _closedDoorEmission;
                if (Input.GetKeyDown(KeyCode.E))
                {
                    DoorSwitch();
                }
            }
            else
            {
                _spriteRenderer.sprite = _openedDoorEmission;
                if (Input.GetKeyDown(KeyCode.E))
                {
                    DoorSwitch();
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

    private void DoorSwitch()
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
