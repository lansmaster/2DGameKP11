using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class ItemInfo : MonoBehaviour
{
    [SerializeField] private GameObject _itemInfo;
    [SerializeField] private PlayerMover _playerMover;

    private TextMeshProUGUI _title;
    private TextMeshProUGUI _description;
    private Image _iconImage;
    private Button _exitButton;
    private Button _dropButton;

    private GameObject _itemGameObject;
    private InventorySlot _currentSlot;

    //private Item InfoItem;

    private void Start()
    {
        _title = _itemInfo.transform.GetChild(0).GetComponent<TextMeshProUGUI>();
        _description = _itemInfo.transform.GetChild(1).GetComponent<TextMeshProUGUI>();
        _iconImage = _itemInfo.transform.GetChild(2).GetComponent<Image>();
        _exitButton = _itemInfo.transform.GetChild(3).GetComponent<Button>();
        _dropButton = _itemInfo.transform.GetChild(4).GetComponent<Button>();

        _exitButton.onClick.AddListener(Close);
        _dropButton.onClick.AddListener(Drop);
    }

    public void ChangeInfo(AssetItem item)
    {
        _title.text = item.Name;
        _description.text = item.Description;
        _iconImage.sprite = item.Icon;
    }

    public void Use() 
    {
        //UseOfItems.instance.Use(InfoItem);
    }

    public void Drop()
    {
        Vector3 dropPosition = new Vector3(_playerMover.transform.position.x + 3f, _playerMover.transform.position.y, _playerMover.transform.position.z);
        _itemGameObject.SetActive(true);
        _itemGameObject.transform.position = dropPosition;

        _currentSlot.ClearSlot();
        Close();
    }

    public void Open(AssetItem item, GameObject itemObject, InventorySlot currentSlot)
    {
        ChangeInfo(item);

        _itemGameObject = itemObject;
        _currentSlot = currentSlot;

        _itemInfo.SetActive(true);
    }

    public void Close()
    {
        _itemInfo.SetActive(false);
    }
}
