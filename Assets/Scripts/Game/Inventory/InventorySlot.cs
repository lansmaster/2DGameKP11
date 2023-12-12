using UnityEngine;
using UnityEngine.UI;

public class InventorySlot : MonoBehaviour
{   
    private ItemInfoWindow _itemInfoWindow;

    private AssetItem _itemInSlot;
    private GameObject _itemGameObject;

    private Image _slotIcon;

    public bool IsEmpty {  get; private set; }

    public void Init()
    {
        _itemInfoWindow = FindObjectOfType<ItemInfoWindow>();

        _slotIcon = transform.GetChild(0).GetComponent<Image>();

        IsEmpty = true;
    }

    public void PutInSlot(AssetItem item, GameObject itemObject)
    {
        IsEmpty = false;

        _itemInSlot = item;
        _itemGameObject = itemObject;   
        _slotIcon.sprite = item.Icon;
        _slotIcon.enabled = true;
    }

    public void SlotClicked() // Повесил на кнопки
    {
        if (_itemInSlot != null)
        {
            _itemInfoWindow.Open(_itemInSlot, _itemGameObject, this);
        }
    }

    public void ClearSlot()
    {
        IsEmpty = true;

        _itemInSlot = null;
        _itemGameObject = null;
        _slotIcon.sprite = null;
        _slotIcon.enabled = false;
    }
}