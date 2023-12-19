using UnityEngine;

[RequireComponent(typeof(SpriteRenderer))]
public class Item : MonoBehaviour
{   
    [SerializeField] private Sprite _default, _emission;
    [SerializeField] private AssetItem _item;
    
    private Inventory _inventory;

    private SpriteRenderer _spriteRenderer;

    public string Name { get { return _item.name; } private set { } }

    private void Start()
    {
        _inventory = FindObjectOfType<Inventory>();
        _spriteRenderer = GetComponent<SpriteRenderer>();
    }

    private void FixedUpdate()
    {
        _spriteRenderer.sprite = _default;
    }

    public void PickUp()
    {
        _spriteRenderer.sprite = _emission;

        if (Input.GetKeyDown(KeyCode.E))
        {
            _inventory.PutInEmptySlot(_item, Name);
            Destroy(gameObject);
        }
    }
}
