using UnityEngine;
using UnityEngine.UI;

public class InventorySlot : MonoBehaviour
{
    [SerializeField] private ItemInfo _itemInfo;
    
    private AssetItem ItemInSlot;
    private GameObject itemGameObject;

    private Image slotIcon;

    public bool IsEmpty {  get; private set; }

    public void Init()
    {
        slotIcon = gameObject.transform.GetChild(0).GetComponent<Image>();

        IsEmpty = true;
    }

    public void PutInSlot(AssetItem item, GameObject itemObject)
    {
        IsEmpty = false;

        ItemInSlot = item;
        itemGameObject = itemObject;   
        slotIcon.sprite = item.Icon;
        slotIcon.enabled = true;
    }

    public void SlotClicked() // Повесил на кнопки
    {
        if (ItemInSlot != null)
        {
            _itemInfo.Open(ItemInSlot, itemGameObject, this);
        }
    }

    public void ClearSlot()
    {
        IsEmpty = true;

        ItemInSlot = null;
        itemGameObject = null;
        slotIcon.sprite = null;
        slotIcon.enabled = false;
    }
}