using UnityEngine;
using UnityEngine.Events;

public class Inventory : MonoBehaviour
{
    [SerializeField] private Transform _slotsTransform;
    private InventorySlot[] inventorySlots = new InventorySlot[16];

    private bool _isOpened = false;

    public UnityAction<bool> InventoryIsOpened;

    private void Start()
    {
        transform.localScale = Vector3.zero;

        for (int i = 0; i < inventorySlots.Length; i++)
        {
            inventorySlots[i] = _slotsTransform.GetChild(i).GetComponent<InventorySlot>();
        }
    }

    public void PutInEmptySlot(AssetItem item, GameObject itemObject)
    {
        for (int i = 0; i < inventorySlots.Length; i++)
        {
            if (inventorySlots[i].ItemInSlot == null)
            {
                inventorySlots[i].PutInSlot(item, itemObject);
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
                transform.localScale = Vector3.zero;
                _isOpened = false;
            }
            else
            {
                transform.localScale = Vector3.one;
                _isOpened = true;
            }

            InventoryIsOpened.Invoke(_isOpened);
        }
    }
}