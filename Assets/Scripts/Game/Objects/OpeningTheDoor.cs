using UnityEngine;

[RequireComponent(typeof(SpriteRenderer))]
[RequireComponent(typeof(BoxCollider2D))]
[RequireComponent(typeof(PolygonCollider2D))]
public class OpeningTheDoor : MonoBehaviour
{
    [SerializeField] private Sprite _closedDoor, _openedDoor, _closedDoorEmission, _openedDoorEmission;
    
    public bool isOpened { get; private set; }

    private SpriteRenderer _spriteRenderer;
    private BoxCollider2D _boxCollider;
    private PolygonCollider2D _polygonCollider;
    private RoomNumberSign _roomNumberSign;

    private void Start()
    {
        _spriteRenderer = GetComponent<SpriteRenderer>();
        _boxCollider = GetComponent<BoxCollider2D>();
        _polygonCollider = GetComponent<PolygonCollider2D>();
        _roomNumberSign = GetComponent<RoomNumberSign>();
    }

    private void FixedUpdate()
    {
        _roomNumberSign.Show(false);

        if (_boxCollider.enabled == true)
        {
            _spriteRenderer.sprite = _closedDoor;
        }
        else
        {
            _spriteRenderer.sprite = _openedDoor;
        }
    }

    public void DoorActons()
    {
        _roomNumberSign.Show(true);

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
