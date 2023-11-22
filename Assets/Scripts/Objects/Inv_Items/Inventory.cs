using UnityEngine;

public class Inventory : MonoBehaviour
{
    [SerializeField] private GameObject _inventory;
    [SerializeField] private Transform _slotsTransform;
    private InventorySlot[] _inventorySlots = new InventorySlot[15];

    private bool _isOpened = false;

    private void Start()
    {
        for (int i = 0; i < _inventorySlots.Length; i++)
        {
            _inventorySlots[i] = _slotsTransform.GetChild(i).GetComponent<InventorySlot>();
        }
    }

    public void PutInEmptySlot(Item item, GameObject itemObject)
    {
        for (int i = 0; i < _inventorySlots.Length; i++)
        {
            if (_inventorySlots[i].SlotItem == null) // Если слот пустой, то кладем туда item
            {
                _inventorySlots[i].PutInSlot(item, itemObject);
                return;
            }
        }
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.I))
        {
            if (_isOpened)
            {
                _inventory.SetActive(false);
                _isOpened = false;
            }
            else
            {
                _inventory.SetActive(true);
                _isOpened = true;
            }
        }
    }
}
