using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class ItemInfoWindow : MonoBehaviour
{
    [SerializeField] private GameObject _itemInfoWindow;
    [SerializeField] private TextMeshProUGUI _title;
    [SerializeField] private TextMeshProUGUI _description;
    [SerializeField] private Image _iconImage;

    private Player _player;
    private InventorySlot _currentSlot;
    private Items _items;
    private Inventory _inventory;

    private GameObject _itemGameObject;
    private string _itemName;

    public void Init()
    {
        _items = FindObjectOfType<Items>();
        _player = FindObjectOfType<Player>();
        _inventory = FindObjectOfType<Inventory>();

        _inventory.OnClose += Close;
    }

    private void SetInfo(AssetItem item, string itemName, InventorySlot currentSlot)
    {
        _title.text = item.Name;
        _description.text = item.Description;
        _iconImage.sprite = item.Icon;

        _itemName = itemName;
        _itemGameObject = _items.GetItem(_itemName);
        _currentSlot = currentSlot;
    }

    public void Drop() // повесил на кнопку
    {
        float positiveRandomValue = Random.Range(0.2f, 0.5f);
        float negativeRandomValue = Random.Range(-0.5f, -0.2f);
        float xRandomValue = Random.Range(0, 2) == 0 ? positiveRandomValue : negativeRandomValue;
        float yRandomValue = Random.Range(0, 2) == 0 ? positiveRandomValue : negativeRandomValue;

        Vector3 dropPosition = new Vector3(_player.Position.x + xRandomValue, _player.Position.y + yRandomValue, _player.Position.z);

        GameObject currentItem = Instantiate(_itemGameObject, dropPosition, Quaternion.identity);
        Rigidbody2D currentItemRigidbody = currentItem.GetComponent<Rigidbody2D>();

        currentItemRigidbody.velocity = new Vector2(xRandomValue * 2, yRandomValue * 2);

        _currentSlot.ClearSlot();
        Close();
    }

    public void Open(AssetItem item, string itemName, InventorySlot currentSlot)
    {
        SetInfo(item, itemName, currentSlot);

        _itemInfoWindow.SetActive(true);
    }

    public void Close() // повесил на кнопку
    {
        _itemInfoWindow.SetActive(false);
    }
}