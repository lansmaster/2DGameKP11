using UnityEngine;

public class FloorDoor : MonoBehaviour
{
    [SerializeField] private Sprite _default;
    [SerializeField] private Sprite _emission;

    private PlayerActions _playerActions;
    private SpriteRenderer _spriteRenderer;

    private void Start()
    {
        _spriteRenderer = GetComponent<SpriteRenderer>();
        _playerActions = Player.Instance.Actions;
    }

    public void EnableEmission(bool enable)
    {
        if (enable)
        {
            _spriteRenderer.sprite = _emission;
        }
        else
        {
            _playerActions.PlayerApproachedTheFloorChanger -= EnableEmission;

            _spriteRenderer.sprite = _default;
        }
    }
}
