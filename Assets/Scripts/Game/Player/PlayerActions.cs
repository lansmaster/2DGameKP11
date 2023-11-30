using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

[RequireComponent(typeof(PlayerMover))]
public class PlayerActions : MonoBehaviour
{
    [SerializeField] private float _interactionDistance;

    [Header("Взаимодействие с дверю: ")]
    [SerializeField] private Image _imgPressE;
    [SerializeField] private Sprite _pressEOpenDoor, _pressECloseDoor;
    [SerializeField] private LayerMask _doors;

    private PlayerMover _player;

    public UnityAction<bool> DoorDetected;

    private void Start()
    {
        _player = GetComponent<PlayerMover>();
    }

    private void Update()
    {
        bool isDoor = Physics2D.OverlapCircle(_player.transform.position, _interactionDistance, _doors);
        Collider2D doorCollider = Physics2D.OverlapCircle(_player.transform.position, _interactionDistance, _doors);

        DoorDetected?.Invoke(CheckingDoors(isDoor, doorCollider));

        CheckingItems();
    }

    private bool CheckingDoors(bool isDoor, Collider2D collider)
    {
        if (isDoor)
        {
            _imgPressE.enabled = true;

            if (collider.gameObject.TryGetComponent(out OpeningTheDoor door))
            {
                if (door.isOpened)
                {
                    _imgPressE.sprite = _pressECloseDoor;
                    return true;
                }
                else
                {
                    _imgPressE.sprite = _pressEOpenDoor;
                    return true;
                }
            }

            return false;
        }
        else
        {
            _imgPressE.enabled = false;
            return false;
        }
    }

    private void CheckingItems()
    {

    }
}
