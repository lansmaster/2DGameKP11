using System;

public class InventoryModel
{
    public event Action<ItemAsset, int> ItemAdded;
    public event Action<ItemAsset> ItemSelected;
    public event Action<ItemAsset, int> ItemRemoved;
    
    private readonly InventoryData _inventoryData;
    private readonly InventoryConfig _inventoryConfig;

    public InventoryModel(InventoryData inventoryData, InventoryConfig inventoryConfig)
    {
        _inventoryData = inventoryData; 
        _inventoryConfig = inventoryConfig;
    }

    public void Add(ItemAsset itemAsset)
    {
        AddToFirstAvailableSlot(itemAsset);
    }

    public bool Select(int slotIndex)
    {
        var slot = _inventoryData.Slots[slotIndex];

        if (slot.IsEmpty())
        {
            return false;
        }

        var itemAsset = slot.ItemAsset;

        ItemSelected?.Invoke(itemAsset);
        return true;
    }

    public bool Remove(int slotIndex)
    {
        var slot = _inventoryData.Slots[slotIndex];

        if (slot.IsEmpty())
        {
            return false;
        }

        var itemAsset = slot.ItemAsset;
        slot.Clean();

        ItemRemoved?.Invoke(itemAsset, slotIndex);
        return true;
    }

    //public void SaveData()
    //{
    //    var size = _inventoryConfig.InventorySize;

    //    for (int slotIndex = 0; slotIndex < size; slotIndex++)
    //    {
    //        var slot = _inventoryData.Slots[slotIndex];
    //    }
    //}

    private void AddToFirstAvailableSlot(ItemAsset itemAsset)
    {
        var size = _inventoryConfig.InventorySize;

        for (int slotIndex = 0; slotIndex < size; slotIndex++)
        {
            var slot = _inventoryData.Slots[slotIndex];

            if (!slot.IsEmpty())
            {
                continue;
            }

            slot.ItemAsset = itemAsset;

            ItemAdded?.Invoke(itemAsset, slotIndex);
            return;
        }
    }
}
