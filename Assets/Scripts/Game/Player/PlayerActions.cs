using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

[RequireComponent(typeof(Player))]
public class PlayerActions : MonoBehaviour
{
    [SerializeField] private float _interactionDistance;

    [Header("Взаимодействие с дверьми: ")]
    [SerializeField] private Image _imgPressE_Door;
    [SerializeField] private Sprite _pressEOpenDoor, _pressECloseDoor;
    [SerializeField] private LayerMask _doorsLayerMask;

    [Header("Взаимодействие с предметами: ")]
    [SerializeField] private Image _imgPressE_PickUpItem;
    [SerializeField] private LayerMask _itemsLayerMask;

    [Header("Взаимодействие с персонажами: ")]
    [SerializeField] private Image _imgPressE_Dialogue;
    [SerializeField] private LayerMask _charactersLayerMask;

    [Header("Взаимодействие с лестницами: ")]
    [SerializeField] private FloorChanger _floorChanger;
    [SerializeField] private Image _imgPressE_FloorChanger;
    [SerializeField] private LayerMask _floorChangersLayerMask;

    public UnityAction<bool> PlayerApproachedTheDoor;
    public UnityAction<bool> PlayerApproachedTheItem;

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
        Collider2D doorCollider = Physics2D.OverlapCircle(Player.instance.position, _interactionDistance, _doorsLayerMask);

        if (doorCollider == null)
        {
            PlayerApproachedTheDoor?.Invoke(false);

            _imgPressE_Door.enabled = false;

            return;
        }

        _imgPressE_Door.enabled = true;

        if (doorCollider.gameObject.TryGetComponent(out Door door))
        {
            PlayerApproachedTheDoor?.Invoke(true);

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

    private void FindingItem()
    {
        Collider2D itemCollider = Physics2D.OverlapCircle(Player.instance.position, _interactionDistance, _itemsLayerMask);
        
        if (itemCollider == null)
        {
            PlayerApproachedTheItem?.Invoke(false);

            _imgPressE_PickUpItem.enabled = false;

            return;
        }

        _imgPressE_PickUpItem.enabled = true;

        if (itemCollider.gameObject.TryGetComponent(out Item item))
        {
            PlayerApproachedTheItem?.Invoke(true);
                
            item.PickUp();
        }
    }

    private void FindingCharacter()
    {
        Collider2D characterCollider = Physics2D.OverlapCircle(Player.instance.position, _interactionDistance, _charactersLayerMask);
        
        if (characterCollider == null)
        {
            _imgPressE_Dialogue.enabled = false;

            return;
        }

        _imgPressE_Dialogue.enabled = true;

        if(characterCollider.gameObject.TryGetComponent(out NPCDialogueTrigger dialogueTrigger))
            dialogueTrigger.TriggerAction();
        
    }

    private void FindingFloorChanger()
    {
        Collider2D floorChangerCollider = Physics2D.OverlapPoint(Player.instance.position, _floorChangersLayerMask);
        
        if (floorChangerCollider == null || floorChangerCollider.layerOverridePriority != 1)
        {
            _imgPressE_FloorChanger.enabled = false;
        
            return;
        }
        
        if (floorChangerCollider.layerOverridePriority == 1)
        {
            _imgPressE_FloorChanger.enabled = true;
            _imgPressE_FloorChanger.sprite = _pressEOpenDoor;

            _floorChanger.Actions(floorChangerCollider.gameObject);
        }
    }
}