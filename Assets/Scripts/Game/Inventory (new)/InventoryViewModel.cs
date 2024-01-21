using System;
using System.Collections.Generic;
using UnityEngine;

public class InventoryViewModel : MonoBehaviour
{
    private InventoryController _inventoryController;
    private InventoryView _inventoryView;

    public event Action<ItemAsset, int> ItemAddedNotification;
    public event Action<ItemAsset, int> ItemDroppedNotification;
    public event Action<ItemAsset> ItemSelectedNotification;

    public void Init(InventoryView InventoryVeiw, int inventorySizeInVeiw)
    {
        _inventoryView = InventoryVeiw;

        var config = new InventoryConfig
        {
            InventorySize = inventorySizeInVeiw,
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

        _inventoryController = new InventoryController(inventory, config);

        _inventoryController.ItemAdded += SendItemAdded;
        _inventoryController.ItemSelected += SendItemSelected;
        _inventoryController.ItemDropped += SendItemDropped;

        ItemNew.ItemPickUped += SendAddItem;

        _inventoryView.DropClicked += SendDropItem;
        _inventoryView.SlotSelected += SendSelectItem;
    }

    private void SendAddItem(ItemAsset itemAsset)
    {
        _inventoryController.Add(itemAsset);
    }

    private void SendSelectItem(int slotIndex)
    {
        _inventoryController.Select(slotIndex);
    }

    private void SendDropItem(int slotIndex)
    {
        _inventoryController.Drop(slotIndex);
    }

    private void SendItemAdded(ItemAsset itemAsset, int slotIndex)
    {
        ItemAddedNotification?.Invoke(itemAsset, slotIndex);
    }

    private void SendItemSelected(ItemAsset itemAsset)
    {
        ItemSelectedNotification?.Invoke(itemAsset);
    }

    private void SendItemDropped(ItemAsset itemAsset, int slotIndex)
    {
        ItemDroppedNotification?.Invoke(itemAsset, slotIndex);
    }
}
