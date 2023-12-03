using UnityEngine;
using UnityEngine.UI;
using static UnityEditor.Progress;

[RequireComponent(typeof(PlayerMover))]
public class PlayerActions : MonoBehaviour
{
    [SerializeField] private float _interactionDistance;

    [Header("�������������� � �����: ")]
    [SerializeField] private Image _imgPressE;
    [SerializeField] private Sprite _pressEOpenDoor, _pressECloseDoor;
    [SerializeField] private LayerMask _doorsLayerMask;

    [Header("�������������� � ����������: ")]
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

    //private void OnDrawGizmos()
    //{
    //    Gizmos.color = Color.yellow;

    //    Gizmos.DrawWireSphere(_player.transform.position, _interactionDistance);
    //}

    private void FindingDoor()
    {
        Collider2D doorCollider = Physics2D.OverlapCircle(_player.transform.position, _interactionDistance, _doorsLayerMask);
        if (doorCollider != null)
        {
            CheckingDoor(true, doorCollider);
        }
        else
        {
            CheckingDoor(false);
        }
    }

    private void CheckingDoor(bool doorFound, Collider2D doorCollider = null)
    {
        if (doorFound)
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

                door.DoorActons(true);
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
            CheckingItem(true, itemCollider);
        }
    }

    private void CheckingItem(bool isItem, Collider2D itemCollider)
    {
        if (isItem)
        {
            if (itemCollider.gameObject.TryGetComponent(out PickUpItems item))
            {
                item.PickUp();
            }
        }
        
    }
}
