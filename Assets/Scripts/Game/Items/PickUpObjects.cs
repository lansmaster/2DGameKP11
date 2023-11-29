using UnityEngine;

public class PickUpObjects : MonoBehaviour
{
    [SerializeField] private PlayerMover _player;
    [SerializeField] private Inventory _inventory;
    [SerializeField] private Sprite _default, _emission;

    public AssetItem item;
    private GameObject _itemGameObject;
    private SpriteRenderer _spriteRenderer;

    private const float _interactionDistance = 1.5f;

    private void Start()
    {
        _itemGameObject = gameObject;
        _spriteRenderer = GetComponent<SpriteRenderer>();
    }

    private void Update()
    {
        float currentDistance = Vector3.Distance(transform.position, _player.transform.position);
        if (currentDistance < _interactionDistance)
        {
            _spriteRenderer.sprite = _emission;
            if (Input.GetKeyDown(KeyCode.E))
            {
                _inventory.PutInEmptySlot(item, _itemGameObject);
                gameObject.SetActive(false);
            }
        }
        else
        {
            _spriteRenderer.sprite = _default;
        }
    }
}
