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

    private Player _player;

    private void Start()
    {
        _player = GetComponent<Player>();
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
        Collider2D doorCollider = Physics2D.OverlapCircle(_player.Position, _interactionDistance, _doorsLayerMask);
        if (doorCollider != null)
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
        Collider2D itemCollider = Physics2D.OverlapCircle(_player.Position, _interactionDistance, _itemsLayerMask);
        if (itemCollider != null)
        {
            if (itemCollider.gameObject.TryGetComponent(out PickUpItems item))
            {
                item.PickUp();
            }
        }
    }

    private void FindingCharacter()
    {
        Collider2D characterCollider = Physics2D.OverlapCircle(_player.Position, _interactionDistance, _charactersLayerMask);
        if (characterCollider != null)
        {
            if(characterCollider.gameObject.TryGetComponent(out NPCDialogueTrigger dialogueTrigger))
            {
                if (dialogueTrigger != null)
                {
                    if (Input.GetKeyDown(KeyCode.E))
                    {
                        dialogueTrigger.TriggerAction();
                    }
                }
            }
        }
    }
}
