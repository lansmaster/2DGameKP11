using UnityEngine;

[RequireComponent(typeof(SpriteRenderer))]
public class PickUpItems : MonoBehaviour
{
    [SerializeField] private Inventory _inventory;
    [SerializeField] private Sprite _default, _emission;

    public AssetItem item;
    private GameObject _itemGameObject;
    private SpriteRenderer _spriteRenderer;

    private void Start()
    {
        _itemGameObject = gameObject;
        _spriteRenderer = GetComponent<SpriteRenderer>();
    }

    private void Update()
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
