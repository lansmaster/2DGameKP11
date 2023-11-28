using UnityEngine;
using UnityEngine.UI;

[RequireComponent (typeof(PlayerMover))]
public class PlayerActions : MonoBehaviour
{
    [SerializeField] private Image _imgPressE;
    [SerializeField] private Sprite _pressEOpenDoor, _pressECloseDoor;
    [SerializeField] private LayerMask _doors;

    private PlayerMover _player;

    private const float _interactionDistance = 0.9f;

    private void Start()
    {
        _player = GetComponent<PlayerMover>();
    }

    private void Update()
    {
        bool isDoor = Physics2D.OverlapCircle(_player.transform.position, _interactionDistance, _doors);
        Collider2D collider = Physics2D.OverlapCircle(_player.transform.position, _interactionDistance, _doors);

        if (isDoor)
        {
            _imgPressE.enabled = true;

            if (collider.TryGetComponent<OpeningTheDoor>(out OpeningTheDoor door))
            {
                if(door.isOpened)
                {
                    _imgPressE.sprite = _pressECloseDoor;
                }
                else
                {
                    _imgPressE.sprite = _pressEOpenDoor;
                }
            }
        }
        else
        {
            _imgPressE.enabled = false;
        }
    }
}
