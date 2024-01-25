using TMPro;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class InventoryView : MonoBehaviour
{
    [SerializeField] private GameObject _itemInfoWindow;
    [SerializeField] private Transform _slotsContainer;
    [SerializeField] private TextMeshProUGUI _infoTitle;
    [SerializeField] private TextMeshProUGUI _infoDescription;
    [SerializeField] private Image _infoItemIcon;

    private InventoryViewModel _inventory;
    private InventorySlotView[] _inventorySlotViews = new InventorySlotView[25];
    private int _currentSlotIndex;

    public bool isOpened { get; private set; }

    public UnityAction<int> DropClicked;
    public UnityAction<int> SlotSelected;

    private void Start()
    {
        transform.localScale = Vector3.zero;
        isOpened = false;

        _itemInfoWindow.SetActive(false);

        _inventory = new InventoryViewModel();
        _inventory.Init(this, _inventorySlotViews.Length);

        for (int i = 0; i < _inventorySlotViews.Length; i++)
        {
            _inventorySlotViews[i] = _slotsContainer.GetChild(i).GetComponent<InventorySlotView>();
            _inventorySlotViews[i].SetSlotIndex(i);
            _inventorySlotViews[i].SlotClicked += SelectSlot;
        }

        _inventory.ItemAddedNotificationForView += SetItemInfo;
        _inventory.ItemDroppedNotificationForView += ClearItemInfo;
        _inventory.ItemDroppedNotificationForView += DropItem;
        _inventory.ItemSelectedNotificationForView += ShowItemInfo;
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.I))
        {
            if (isOpened)
                Close();
            else
                Open();
        }
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (isOpened)
                Close();
        }
    }

    private void Open()
    {
        transform.localScale = Vector3.one;
        isOpened = true;
    }

    private void Close()
    {
        transform.localScale = Vector3.zero;
        isOpened = false;
    }

    public void Drop() // повесил на кнопку
    {
        DropClicked?.Invoke(_currentSlotIndex);
    }

    public void CloseItemIfoWindow() // повесил на кнопку
    {
        _itemInfoWindow.SetActive(false);
    }

    private void SetItemInfo(ItemAsset itemAsset, int slotIndex)
    {
        _infoTitle.text = itemAsset.Name;
        _infoDescription.text = itemAsset.Description;
        _infoItemIcon.sprite = itemAsset.Icon;

        _inventorySlotViews[slotIndex].SetItemIcon(itemAsset.Icon);
    }

    private void ClearItemInfo(ItemAsset itemAsset, int slotIndex)
    {
        _infoTitle.text = null;
        _infoDescription.text = null;
        _infoItemIcon.sprite = null;

        _inventorySlotViews[slotIndex].ClearItemIcon();

        _itemInfoWindow.SetActive(false);
    }

    private void SelectSlot(int slotIndex)
    {
        _currentSlotIndex = slotIndex;

        SlotSelected?.Invoke(slotIndex);
    }

    private void ShowItemInfo(ItemAsset itemAsset)
    {
        _infoTitle.text = itemAsset.Name;
        _infoDescription.text = itemAsset.Description;
        _infoItemIcon.sprite = itemAsset.Icon;

        _itemInfoWindow.SetActive(true);
    }

    private void DropItem(ItemAsset itemAsset, int slotIndex)
    {
        var item = Items.instance.GetItem(itemAsset.name);

        float positiveRandomValue = Random.Range(0.2f, 0.5f);
        float negativeRandomValue = Random.Range(-0.5f, -0.2f);
        float xRandomValue = Random.Range(0, 2) == 0 ? positiveRandomValue : negativeRandomValue;
        float yRandomValue = Random.Range(0, 2) == 0 ? positiveRandomValue : negativeRandomValue;
        Vector3 dropPosition = new Vector3(Player.instance.position.x + xRandomValue, Player.instance.position.y + yRandomValue, Player.instance.position.z);
        GameObject currentItem = Instantiate(item, dropPosition, Quaternion.identity);
        Rigidbody2D currentItemRigidbody = currentItem.GetComponent<Rigidbody2D>();

        currentItemRigidbody.velocity = new Vector2(xRandomValue * 2, yRandomValue * 2);
    }
}
