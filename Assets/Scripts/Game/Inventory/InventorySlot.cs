using UnityEngine;
using UnityEngine.UI;

public class InventorySlot : MonoBehaviour
{
    [SerializeField] private ItemInfo _itemInfo;
    
    public Item ItemInSlot;
    public GameObject itemGameObject;

    public Image slotIcon;
    private Button _button;


    private void Start()
    {
        slotIcon = gameObject.transform.GetChild(0).GetComponent<Image>();
        
        _button = GetComponent<Button>();

        _button.onClick.AddListener(SlotClicked);
    }

    public void PutInSlot(Item item, GameObject itemObject)
    {
        ItemInSlot = item;
        itemGameObject = itemObject;   
        slotIcon.sprite = item.icon;
        slotIcon.enabled = true;
    }

    void SlotClicked()
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