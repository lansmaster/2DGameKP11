using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(PlayerMover))]
public class PlayerActions : MonoBehaviour
{
    [SerializeField] private float _interactionDistance;

    [Header("Взаимодействие с дверю: ")]
    [SerializeField] private Image _imgPressE;
    [SerializeField] private Sprite _pressEOpenDoor, _pressECloseDoor;
    [SerializeField] private LayerMask _doorsLayerMask;

    [Header("Взаимодействие с предметами: ")]
    [SerializeField] private LayerMask _itemsLayerMask;

    private PlayerMover _player;

    private void Start()
    {
        _player = GetComponent<PlayerMover>();
    }

    private void Update()
    {
        FindingDoor();

        FindingItem();
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.yellow;

        Gizmos.DrawWireSphere(_player.transform.position, _interactionDistance);
    }

    private void FindingDoor()
    {
        Collider2D doorCollider = Physics2D.OverlapCircle(_player.transform.position, _interactionDistance, _doorsLayerMask);
        if (doorCollider != null && doorCollider.layerOverridePriority == 0)
        {
            CheckingDoor(true, doorCollider);
        }
        else
        {
            CheckingDoor(false);
        }
    }

    private void CheckingDoor(bool isDoor, Collider2D doorCollider = null)
    {
        if (isDoor)
        {
            _imgPressE.enabled = true;

            if (doorCollider.gameObject.TryGetComponent(out OpeningTheDoor door))
            {
                if (door.isOpened)
                {
                    _imgPressE.sprite = _pressECloseDoor;
                }
                else
                {
                    _imgPressE.sprite = _pressEOpenDoor;
                }

                door.DoorActons();
            }

        }
        else
        {
            _imgPressE.enabled = false;
        }
    }

    private void FindingItem()
    {
        Collider2D itemCollider = Physics2D.OverlapCircle(_player.transform.position, _interactionDistance, _itemsLayerMask);
        if (itemCollider != null)
        {
            CheckingItem(itemCollider);
        }
    }

    private void CheckingItem(Collider2D itemCollider)
    {
        if (itemCollider.gameObject.TryGetComponent(out PickUpObjects item))
        {
            item.PickUp();
        }
    }
}
