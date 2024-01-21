using UnityEngine;

[RequireComponent(typeof(SpriteRenderer))]
public class Item : MonoBehaviour
{   
    [SerializeField] private Sprite _default, _emission;
    [SerializeField] private ItemAsset _itemAsset;

    private PlayerActions _playerActions;
    private Inventory _inventory;
    private SpriteRenderer _spriteRenderer;

    public string Name { get { return _itemAsset.name; } private set { } }

    private void Start()
    {
        _inventory = Inventory.instance;
        _playerActions = Player.instance.actions;
        _spriteRenderer = GetComponent<SpriteRenderer>();
    }

    public void PickUp()
    {
        _playerActions.PlayerApproachedTheItem += EnableEmission;

        if (Input.GetKeyDown(KeyCode.E))
        {
            _inventory.PutInEmptySlot(_itemAsset, Name);
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