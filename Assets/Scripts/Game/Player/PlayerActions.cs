using UnityEngine;
using UnityEngine.Events;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

[RequireComponent(typeof(Player))]
public class PlayerActions : MonoBehaviour
{
    [SerializeField] private float _interactionDistance;
    [SerializeField] private Image _imgPressE;

    [Header("Взаимодействие с дверьми: ")]
    [SerializeField] private Sprite _pressE_OpenDoor;
    [SerializeField] private Sprite _pressE_CloseDoor;
    [SerializeField] private LayerMask _doorsLayerMask;

    [Header("Взаимодействие с предметами: ")]
    [SerializeField] private Sprite _pressE_Item;
    [SerializeField] private LayerMask _itemsLayerMask;

    [Header("Взаимодействие с персонажами: ")]
    [SerializeField] private Sprite _pressE_Dialogue;
    [SerializeField] private LayerMask _charactersLayerMask;

    [Header("Взаимодействие с лестницами: ")]
    [SerializeField] private FloorChanger _floorChanger;
    [SerializeField] private LayerMask _floorChangersLayerMask;

    public UnityAction<bool> PlayerApproachedTheDoor;
    public UnityAction<bool> PlayerApproachedTheItem;
    public UnityAction<bool> PlayerApproachedTheCharacter;
    public UnityAction<bool> PlayerApproachedTheFloorChanger;

    private void Start()
    {
        SceneManager.sceneLoaded += (Scene scene, LoadSceneMode sceneMode) => { PlayerApproachedTheFloorChanger?.Invoke(false); };
    }

    private void OnDestroy()
    {
        SceneManager.sceneLoaded -= (Scene scene, LoadSceneMode sceneMode) => { PlayerApproachedTheFloorChanger?.Invoke(false); };
    }

    private void Update()
    {
        FindiDoor();

        FindItem();

        FindCharacter();

        FindFloorChanger();
    }

    //private void OnDrawGizmos()
    //{
    //    Gizmos.color = Color.yellow;

    //    Gizmos.DrawWireSphere(_player.transform.position, _interactionDistance);
    //}

    private void FindiDoor()
    {
        Collider2D doorCollider = Physics2D.OverlapCircle(Player.Instance.Position, _interactionDistance, _doorsLayerMask);

        if (doorCollider == null)
        {
            PlayerApproachedTheDoor?.Invoke(false);

            return;
        }

        if (doorCollider.gameObject.TryGetComponent(out Door door))
        {
            if (PlayerApproachedTheDoor != null)
            {
                PlayerApproachedTheDoor.Invoke(true);
            }
            else
            {
                PlayerApproachedTheDoor += ShowImagePressE_Door;
            }

            if (door.IsOpened)
            {
                _imgPressE.sprite = _pressE_CloseDoor;
            }
            else
            {
                _imgPressE.sprite = _pressE_OpenDoor;
            }

            door.Actions();
        }
    }

    private void FindItem()
    {
        Collider2D itemCollider = Physics2D.OverlapCircle(Player.Instance.Position, _interactionDistance, _itemsLayerMask);
        
        if (itemCollider == null)
        {
            PlayerApproachedTheItem?.Invoke(false);

            return;
        }

        if (itemCollider.gameObject.TryGetComponent(out Item item))
        {
            if (PlayerApproachedTheItem != null)
            {
                PlayerApproachedTheItem.Invoke(true);
            }
            else
            {
                PlayerApproachedTheItem += ShowImagePressE_Item;
                PlayerApproachedTheItem += item.EnableEmission;
            }
               
            item.PickUp();
        }
    }

    private void FindCharacter()
    {
        Collider2D characterCollider = Physics2D.OverlapCircle(Player.Instance.Position, _interactionDistance, _charactersLayerMask);
        
        if (characterCollider == null)
        {
            PlayerApproachedTheCharacter?.Invoke(false);

            return;
        }

        if (characterCollider.gameObject.TryGetComponent(out NPCDialogueTrigger dialogueTrigger))
        {
            if (PlayerApproachedTheCharacter != null)
            {
                PlayerApproachedTheCharacter.Invoke(true);
            }
            else
            {
                PlayerApproachedTheCharacter += ShowImagePressE_Dialogue;
            }

            dialogueTrigger.TriggerAction();
        }
    }

    private void FindFloorChanger()
    {
        Collider2D floorChangerCollider = Physics2D.OverlapPoint(Player.Instance.Position, _floorChangersLayerMask);
        
        if (floorChangerCollider == null || floorChangerCollider.layerOverridePriority != 1)
        {
            PlayerApproachedTheFloorChanger?.Invoke(false);

            return;
        }
        
        if (floorChangerCollider.layerOverridePriority == 1 && floorChangerCollider.gameObject.TryGetComponent(out FloorDoor floorDoor))
        {
            if (PlayerApproachedTheFloorChanger != null)
            {
                PlayerApproachedTheFloorChanger.Invoke(true);
            }
            else
            {
                PlayerApproachedTheFloorChanger += ShowImagePressE_FloorChanger;
                PlayerApproachedTheFloorChanger += floorDoor.EnableEmission;
            }

            _floorChanger.Launch();
        }
    }

    private void ShowImagePressE_Door(bool show)
    {
        if (show)
        {
            _imgPressE.enabled = true;
        }
        else
        {
            _imgPressE.enabled = false;
            PlayerApproachedTheDoor -= ShowImagePressE_Door;
        }
    }

    private void ShowImagePressE_Item(bool show)
    {
        _imgPressE.sprite = _pressE_Item;

        if (show)
        {
            _imgPressE.enabled = true;
        }
        else
        {
            _imgPressE.enabled = false;
            PlayerApproachedTheItem -= ShowImagePressE_Item;
        }
    }

    private void ShowImagePressE_Dialogue(bool show)
    {
        _imgPressE.sprite = _pressE_Dialogue;

        if (show)
        {
            _imgPressE.enabled = true;
        }
        else
        {
            _imgPressE.enabled = false;
            PlayerApproachedTheCharacter -= ShowImagePressE_Dialogue;
        }
    }

    private void ShowImagePressE_FloorChanger(bool show)
    {
        _imgPressE.sprite = _pressE_OpenDoor;

        if (show)
        {
            _imgPressE.enabled = true;
        }
        else
        {
            _imgPressE.enabled = false;
            PlayerApproachedTheFloorChanger -= ShowImagePressE_FloorChanger;
        }
    }
}