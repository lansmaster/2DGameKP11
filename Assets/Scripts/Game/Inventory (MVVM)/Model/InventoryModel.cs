using System;
using System.Data;

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

    public void LoadDataFromDataBase()
    {
        if(DataBase.ExecuteQueryWithAnswer("SELECT EXISTS(SELECT * FROM Inventory)") != "0")
        {
            DataTable InventoryDataTable = DataBase.GetTable("SELECT * FROM Inventory");

            for (int i = 0; i < InventoryDataTable.Rows.Count; i++)
            {
                int slotIndex = int.Parse(InventoryDataTable.Rows[i][2].ToString());
                string itemAssetName = InventoryDataTable.Rows[i][1].ToString();
                var slot = _inventoryData.Slots[slotIndex];
                slot.ItemAsset = Items.instance.GetItemAsset(itemAssetName);
                ItemAdded?.Invoke(slot.ItemAsset, slotIndex);
            }
        }
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

        RemoveSlotToDataBase(slotIndex);

        ItemRemoved?.Invoke(itemAsset, slotIndex);
        return true;
    }

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

            SaveSlotToDataBase(itemAsset, slotIndex);

            ItemAdded?.Invoke(itemAsset, slotIndex);

            return;
        }
    }

    private void SaveSlotToDataBase(ItemAsset itemAsset, int slotIndex)
    {
        DataBase.ExecuteQueryWithoutAnswer($"INSERT INTO Inventory (ItemAssetName, SlotIndex) VALUES ('{itemAsset.Name.ToString()}',{slotIndex})");
    }

    private void RemoveSlotToDataBase(int slotIndex)
    {
        if($"SELECT EXISTS(SELECT * FROM Inventory WHERE SlotIndex = {slotIndex})" != "0")
        {
            DataBase.ExecuteQueryWithoutAnswer($"DELETE FROM Inventory WHERE SlotIndex = {slotIndex}");
        }
    }
}
