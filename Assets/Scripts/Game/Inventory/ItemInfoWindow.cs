using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class ItemInfoWindow : MonoBehaviour
{
    [SerializeField] private GameObject _itemInfoWindow;

    private Player _player;

    private TextMeshProUGUI _title;
    private TextMeshProUGUI _description;
    private Image _iconImage;

    private GameObject _itemGameObject;
    private InventorySlot _currentSlot;

    private Rigidbody2D _itemRigidbody;

    public void Init()
    {
        _title = _itemInfoWindow.transform.GetChild(0).GetComponent<TextMeshProUGUI>();
        _description = _itemInfoWindow.transform.GetChild(1).GetComponent<TextMeshProUGUI>();
        _iconImage = _itemInfoWindow.transform.GetChild(2).GetComponent<Image>();

        _player = FindObjectOfType<Player>();
    }

    public void ChangeInfo(AssetItem item)
    {
        _title.text = item.Name;
        _description.text = item.Description;
        _iconImage.sprite = item.Icon;
    }

    public void Use()
    {
    }

    public void Drop() // повесил на кнопку
    {
        float positiveRandomValue = Random.Range(0.2f, 0.5f);
        float negativeRandomValue = Random.Range(-0.5f, -0.2f);
        float xRandomValue = Random.Range(0, 2) == 0 ? positiveRandomValue : negativeRandomValue;
        float yRandomValue = Random.Range(0, 2) == 0 ? positiveRandomValue : negativeRandomValue;

        Vector3 dropPosition = new Vector3(_player.Position.x + xRandomValue, _player.Position.y + yRandomValue, _player.Position.z);
        _itemGameObject.SetActive(true);
        _itemGameObject.transform.position = dropPosition;
        
        _itemRigidbody.velocity = new Vector3(xRandomValue * 2, yRandomValue * 2, 0);

        _currentSlot.ClearSlot();
        Close();
    }

    public void Open(AssetItem item, GameObject itemObject, InventorySlot currentSlot)
    {
        ChangeInfo(item);

        _itemGameObject = itemObject;
        _itemRigidbody = _itemGameObject.GetComponent<Rigidbody2D>();
        _currentSlot = currentSlot;

        _itemInfoWindow.SetActive(true);
    }

    public void Close() // повесил на кнопку
    {
        _itemInfoWindow.SetActive(false);
    }
}
