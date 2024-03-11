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

    private InventoryViewModel _viewModel;
    private InventorySlotView[] _inventorySlotViews = new InventorySlotView[25];
    private int _currentSlotIndex;

    public bool IsOpened { get; private set; }
    public int InventorySize { get; private set; }

    public UnityAction<int> DropClicked;
    public UnityAction<int> SlotSelected;

    private void Awake()
    {
        transform.localScale = Vector3.zero;
        _itemInfoWindow.SetActive(false);
        IsOpened = false;

        InventorySize = _inventorySlotViews.Length;
    }

    private void Start()
    {
        _viewModel = GetComponent<InventoryViewModel>();

        for (int i = 0; i < InventorySize; i++)
        {
            _inventorySlotViews[i] = _slotsContainer.GetChild(i).GetComponent<InventorySlotView>();
            _inventorySlotViews[i].SetSlotIndex(i);
            _inventorySlotViews[i].SlotClicked += SelectSlot;
        }

        _viewModel.ItemAdded += SetItemInfo;
        _viewModel.ItemSelected += ShowItemInfo;
        _viewModel.ItemRemoved += ClearItemInfo;
        _viewModel.ItemRemoved += DropItem;
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.I))
        {
            if (IsOpened)
                Close();
            else
                Open();
        }
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (IsOpened)
                Close();
        }
    }

    private void Open()
    {
        transform.localScale = Vector3.one;
        IsOpened = true;
    }

    private void Close()
    {
        transform.localScale = Vector3.zero;
        IsOpened = false;
    }

    public void DropButton() // повесил на кнопку
    {
        DropClicked?.Invoke(_currentSlotIndex);
    }

    public void CloseItemInfoWindow() // повесил на кнопку
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
        var item = Items.Instance.GetItemPrefab(itemAsset.Name);

        float positiveRandomValue = Random.Range(0.2f, 0.5f);
        float negativeRandomValue = Random.Range(-0.5f, -0.2f);
        float xRandomValue = Random.Range(0, 2) == 0 ? positiveRandomValue : negativeRandomValue;
        float yRandomValue = Random.Range(0, 2) == 0 ? positiveRandomValue : negativeRandomValue;
        Vector3 dropPosition = new Vector3(Player.Instance.Position.x + xRandomValue, Player.Instance.Position.y + yRandomValue, Player.Instance.Position.z);
        GameObject currentItem = Instantiate(item, dropPosition, Quaternion.identity);
        Rigidbody2D currentItemRigidbody = currentItem.GetComponent<Rigidbody2D>();

        currentItemRigidbody.velocity = new Vector2(xRandomValue * 2, yRandomValue * 2);
    }
}
