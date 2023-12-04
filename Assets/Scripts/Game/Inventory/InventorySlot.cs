using UnityEngine;
using UnityEngine.UI;

public class InventorySlot : MonoBehaviour
{
    [SerializeField] private ItemInfo _itemInfo;
    
    public AssetItem ItemInSlot;
    private GameObject itemGameObject;

    private Image slotIcon;


    private void Start()
    {
        slotIcon = gameObject.transform.GetChild(0).GetComponent<Image>();
    }

    public void PutInSlot(AssetItem item, GameObject itemObject)
    {
        ItemInSlot = item;
        itemGameObject = itemObject;   
        slotIcon.sprite = item.Icon;
        slotIcon.enabled = true;
    }

    public void SlotClicked()
    {
        if (ItemInSlot != null)
        {
            _itemInfo.Open(ItemInSlot, itemGameObject, this);
        }
    }

    public void ClearSlot()
    {
        ItemInSlot = null;
        itemGameObject = null;
        slotIcon.sprite = null;
        slotIcon.enabled = false;
    }
}