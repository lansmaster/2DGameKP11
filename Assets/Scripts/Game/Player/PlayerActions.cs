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

    [Header("Взаимодействие с персонажами: ")]
    [SerializeField] private LayerMask _charactersLayerMask;

    private PlayerMover _player;

    private void Start()
    {
        _player = GetComponent<PlayerMover>();
    }

    private void Update()
    {
        FindingDoor();

        FindingItem();

        FindingCharacter();
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
            CheckingItem(true, itemCollider);
        }
    }

    private void CheckingItem(bool itemFound, Collider2D itemCollider)
    {
        if (itemFound)
        {
            if (itemCollider.gameObject.TryGetComponent(out PickUpItems item))
            {
                item.PickUp();
            }
        }
    }

    private void FindingCharacter()
    {
        Collider2D characterCollider = Physics2D.OverlapCircle(_player.transform.position, _interactionDistance, _charactersLayerMask);
        if (characterCollider != null)
        {
            if(characterCollider.gameObject.TryGetComponent(out Teacher203 character))
            {
                character.ShowDialog();
            }
        }
    }
}
