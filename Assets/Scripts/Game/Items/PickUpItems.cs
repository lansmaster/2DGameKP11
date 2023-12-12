using UnityEngine;

[RequireComponent(typeof(SpriteRenderer))]
public class PickUpItems : MonoBehaviour
{   
    [SerializeField] private Sprite _default, _emission;
    [SerializeField] private AssetItem item;
    
    private Inventory _inventory;

    private GameObject _itemGameObject;
    private SpriteRenderer _spriteRenderer;

    private void Start()
    {
        _inventory = FindObjectOfType<Inventory>();
        _spriteRenderer = GetComponent<SpriteRenderer>();
        _itemGameObject = gameObject;
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
            _inventory.PutInEmptySlot(item, _itemGameObject);
            gameObject.SetActive(false);
        }
    }
}
