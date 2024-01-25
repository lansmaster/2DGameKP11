using UnityEngine;
using UnityEngine.Events;

public class Item : MonoBehaviour
{
    [SerializeField] private Sprite _default, _emission;
    [SerializeField] private ItemAsset _itemAsset;

    private PlayerActions _playerActions;
    private SpriteRenderer _spriteRenderer;

    public static UnityAction<ItemAsset> ItemPickUped;

    public string Name { get { return _itemAsset.name; } private set { } }

    private void Start()
    {
        _playerActions = Player.instance.actions;
        _spriteRenderer = GetComponent<SpriteRenderer>();
    }

    public void PickUp()
    {
        if (Input.GetKeyDown(KeyCode.E))
        {
            ItemPickUped?.Invoke(_itemAsset);
            Destroy(gameObject);
        }
    }

    public void EnableEmission(bool enable)
    {
        if (enable)
        {
            _spriteRenderer.sprite = _emission;
        }
        else
        {
            _playerActions.PlayerApproachedTheItem -= EnableEmission;

            _spriteRenderer.sprite = _default;
        }
    }
}
