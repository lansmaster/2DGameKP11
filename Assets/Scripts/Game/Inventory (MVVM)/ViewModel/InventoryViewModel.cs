using System.Collections.Generic;
using UnityEngine;

public class InventoryViewModel : MonoBehaviour
{
    private InventoryModel _inventoryModel;
    private InventoryView _inventoryView;

    public void Start()
    {
        _inventoryView = GetComponent<InventoryView>();

        var config = new InventoryConfig
        {
            InventorySize = _inventoryView.inventorySize,
        };

        var inventorySize = config.InventorySize;
        var inventory = new InventoryData 
        {
            Slots = new List<InventorySlotData>(inventorySize),
        };

        for (int i = 0; i < inventorySize; i++)
        {
            inventory.Slots.Add(new InventorySlotData());
        }

        _inventoryModel = new InventoryModel(inventory, config);

        _inventoryModel.ItemAdded += SetItemInView;
        _inventoryModel.ItemSelected += DisplayItemInView;
        _inventoryModel.ItemRemoved += RemoveItemInView;

        Item.ItemPickUped += SendAddItem;

        _inventoryView.DropClicked += SendRemoveItem;
        _inventoryView.SlotSelected += SendSelectItem;
    }

    private void SendAddItem(ItemAsset itemAsset)
    {
        _inventoryModel.Add(itemAsset);
    }

    private void SendSelectItem(int slotIndex)
    {
        _inventoryModel.Select(slotIndex);
    }

    private void SendRemoveItem(int slotIndex)
    {
        _inventoryModel.Remove(slotIndex);
    }

    private void SetItemInView(ItemAsset itemAsset, int slotIndex)
    {
        _inventoryView.SetItemInfo(itemAsset, slotIndex);
    }

    private void DisplayItemInView(ItemAsset itemAsset)
    {
        _inventoryView.ShowItemInfo(itemAsset);
    }

    private void RemoveItemInView(ItemAsset itemAsset, int slotIndex)
    {
        _inventoryView.ClearItemInfo(itemAsset, slotIndex);

        _inventoryView.DropItem(itemAsset, slotIndex);
    }
}
