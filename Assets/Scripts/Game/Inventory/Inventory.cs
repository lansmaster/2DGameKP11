using UnityEngine;
using UnityEngine.Events;

public class Inventory : MonoBehaviour
{
    [SerializeField] private Transform _slotsContainer;

    private InventorySlot[] _inventorySlots = new InventorySlot[25];

    public bool IsOpened { get; private set; }

    private void Awake()
    {
        transform.localScale = Vector3.zero;

        for (int i = 0; i < _inventorySlots.Length; i++)
        {
            _inventorySlots[i] = _slotsContainer.GetChild(i).GetComponent<InventorySlot>();
            _inventorySlots[i].Init();
        }
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

    public void PutInEmptySlot(AssetItem item, GameObject itemObject)
    {
        for (int i = 0; i < _inventorySlots.Length; i++)
        {
            if (_inventorySlots[i].IsEmpty == true)
            {
                _inventorySlots[i].PutInSlot(item, itemObject);
                return;
            }
        }
    }
}