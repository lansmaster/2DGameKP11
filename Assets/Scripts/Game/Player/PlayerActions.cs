using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(PlayerMover))]
public class PlayerActions : MonoBehaviour
{
    [SerializeField] private float _interactionDistance;

    [Header("Взаимодействие с дверю: ")]
    [SerializeField] private Image _imgPressE_Door;
    [SerializeField] private Sprite _pressEOpenDoor, _pressECloseDoor;
    [SerializeField] private LayerMask _doorsLayerMask;

    [Header("Взаимодействие с предметами: ")]
    [SerializeField] private LayerMask _itemsLayerMask;

    [Header("Взаимодействие с персонажами: ")]
    [SerializeField] private LayerMask _charactersLayerMask;

    [Header("Взаимодействие с лестницами: ")]
    [SerializeField] private Image _imgPressE_FloorChanger;
    [SerializeField] private LayerMask _floorChangersLayerMask;

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

        FindingFloorChanger();
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
            _imgPressE_Door.enabled = true;

            if (doorCollider.gameObject.TryGetComponent(out OpeningTheDoor door))
            {
                if (door.isOpened)
                {
                    _imgPressE_Door.sprite = _pressECloseDoor;
                }
                else
                {
                    _imgPressE_Door.sprite = _pressEOpenDoor;
                }

                door.Actions();
            }
        }
        else
        {
            _imgPressE_Door.enabled = false;
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
                dialogueTrigger.TriggerAction();
            }
        }
    }

    private void FindingFloorChanger()
    {
        Collider2D floorChangerCollider = Physics2D.OverlapCircle(_player.Position, _interactionDistance, _floorChangersLayerMask);
        if (floorChangerCollider != null)
        {
            if (floorChangerCollider.layerOverridePriority == 1)
            {
                _imgPressE_FloorChanger.enabled = true;
                _imgPressE_FloorChanger.sprite = _pressEOpenDoor;

                if (floorChangerCollider.gameObject.TryGetComponent(out FloorChanger floorChanger))
                {
                    floorChanger.Actions();
                }
            }
            else
            {
                _imgPressE_FloorChanger.enabled = false;
            }
        }
        else
        {
            _imgPressE_FloorChanger.enabled = false;
        }
    }
}
