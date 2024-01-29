
public static class InventoryExtensions
{
    public static bool IsEmpty(this InventorySlotData slot)
    {
        return slot.ItemAsset == null;
    }

    public static void Clean(this InventorySlotData slot)
    {
        slot.ItemAsset = null;
    }
}
