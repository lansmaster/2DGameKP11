using UnityEngine;
using UnityEngine.Events;

public class ItemNew : MonoBehaviour
{
    [SerializeField] private Sprite _default, _emission;
    [SerializeField] private ItemAsset _itemAsset;
    [SerializeField] private InventoryViewModel _inventory;

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
        _playerActions.PlayerApproachedTheItem += EnableEmission;

        if (Input.GetKeyDown(KeyCode.E))
        {
            ItemPickUped?.Invoke(_itemAsset);
            Destroy(gameObject);
        }

    }

    private void EnableEmission(bool enable)
    {
        if (enable)
        {
            _spriteRenderer.sprite = _emission;
        }
        else
        {
            _spriteRenderer.sprite = _default;
            _playerActions.PlayerApproachedTheItem -= EnableEmission;
        }
    }

    private void OnDisable()
    {
        _playerActions.PlayerApproachedTheItem -= EnableEmission;
    }
}
