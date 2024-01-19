using UnityEngine;

[RequireComponent(typeof(SpriteRenderer))]
public class Item : MonoBehaviour
{   
    [SerializeField] private Sprite _default, _emission;
    [SerializeField] private AssetItem _assetItem;

    private PlayerActions _playerActions;
    private Inventory _inventory;
    private SpriteRenderer _spriteRenderer;

    public string Name { get { return _assetItem.name; } private set { } }

    private void Start()
    {
        _inventory = FindObjectOfType<Inventory>();
        _playerActions = FindObjectOfType<PlayerActions>();
        _spriteRenderer = GetComponent<SpriteRenderer>();
    }

    public void PickUp()
    {
        _playerActions.PlayerApproachedTheItem += EnableEmission;

        if (Input.GetKeyDown(KeyCode.E))
        {
            _inventory.PutInEmptySlot(_assetItem, Name);
            Destroy(gameObject);
        }
        
    }

    public void Drop()
    {
        // реализовать выбрасывание тут, а не в ItemInfoWindow...
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
}