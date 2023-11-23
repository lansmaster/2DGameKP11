using UnityEngine;
using UnityEngine.UI;

public class InventorySlot : MonoBehaviour
{
    [SerializeField] private ItemInfo _itemInfo;
    
    public Item SlotItem;
    private GameObject _itemObject;

    private Image _icon;
    private Button _button;


    private void Start()
    {
        _icon = gameObject.transform.GetChild(0).GetComponent<Image>();
        
        _button = GetComponent<Button>();

        _button.onClick.AddListener(SlotClicked);
    }

    public void PutInSlot(Item item, GameObject itemObject)
    {
        SlotItem = item;
        _itemObject = itemObject;   
        _icon.sprite = item.icon;
        _icon.enabled = true;
    }

    void SlotClicked()
    {
        if (SlotItem != null)
        {
            _itemInfo.Open(SlotItem, _itemObject, this);
        }
    }

    public void ClearSlot()
    {
        SlotItem = null;
        _itemObject = null;
        _icon.sprite = null;
        _icon.enabled = false;
    }
}
