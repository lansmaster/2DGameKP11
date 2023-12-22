using UnityEngine;
using UnityEngine.Events;

public class Inventory : MonoBehaviour
{
    [SerializeField] private Transform _slotsContainer;

    private InventorySlot[] _inventorySlots = new InventorySlot[25];

    private ItemInfoWindow _itemInfoWindow;

    public bool isOpened { get; private set; }

    public UnityAction OnClose;

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
    }
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.I))
        {
            if (isOpened)
                Close();
            else
                Open();
        }
        if (Input.GetKeyDown(KeyCode.Escape))
        { 
            if (isOpened)
                Close();
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

    private void Open()
    {
        transform.localScale = Vector3.one;
        isOpened = true;
    }

    private void Close()
    {
        transform.localScale = Vector3.zero;
        isOpened = false;
        
        OnClose();
    }
}