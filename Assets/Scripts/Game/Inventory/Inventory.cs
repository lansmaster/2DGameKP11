using UnityEngine;

public class Inventory : MonoBehaviour
{
    [SerializeField] private Transform _slotsContainer;

    private InventorySlot[] _inventorySlots = new InventorySlot[25];

    private ItemInfoWindow _itemInfoWindow;

    public bool IsOpened { get; private set; }

    private void Awake()
    {
        transform.localScale = Vector3.zero;

        for (int i = 0; i < _inventorySlots.Length; i++)
        {
            _inventorySlots[i] = _slotsContainer.GetChild(i).GetComponent<InventorySlot>();
            _inventorySlots[i].Init();
        }

        _itemInfoWindow = GetComponent<ItemInfoWindow>();
        _itemInfoWindow.Init();

        DontDestroyOnLoad(transform.parent.gameObject);
    }
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.I))
        {
            if (IsOpened)
            {
                transform.localScale = Vector3.zero;
                IsOpened = false;
            }
            else
            {
                transform.localScale = Vector3.one;
                IsOpened = true;
            }
        }
    }

    public void PutInEmptySlot(AssetItem item, string itemName)
    {
        for (int i = 0; i < _inventorySlots.Length; i++)
        {
            if (_inventorySlots[i].IsEmpty == true)
            {
                _inventorySlots[i].PutInSlot(item, itemName);
                return;
            }
        }
    }
}