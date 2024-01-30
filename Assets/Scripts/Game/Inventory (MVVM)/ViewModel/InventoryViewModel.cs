using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class InventoryViewModel : MonoBehaviour
{
    private InventoryModel _inventoryModel;
    private InventoryView _inventoryView;

    public UnityAction<ItemAsset, int> ItemAdded;
    public UnityAction<ItemAsset> ItemSelected;
    public UnityAction<ItemAsset, int> ItemRemoved;

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

        _inventoryModel.ItemAdded += SendItemAdded;
        _inventoryModel.ItemSelected += SendItemSelected;
        _inventoryModel.ItemRemoved += SendItemRemoved;

        Item.ItemPickUped += SendAddItem;

        _inventoryView.DropClicked += SendRemoveItem;
        _inventoryView.SlotSelected += SendSelectItem;
        
        _inventoryModel.LoadDataFromDataBase();
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

    private void SendItemAdded(ItemAsset itemAsset, int slotIndex)
    {
        ItemAdded?.Invoke(itemAsset, slotIndex);
    }

    private void SendItemSelected(ItemAsset itemAsset)
    {
        ItemSelected?.Invoke(itemAsset);
    }

    private void SendItemRemoved(ItemAsset itemAsset, int slotIndex)
    {
        ItemRemoved?.Invoke(itemAsset, slotIndex);
    }
}
