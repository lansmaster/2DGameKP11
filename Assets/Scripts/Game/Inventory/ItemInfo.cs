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

    private GameObject _itemGameObject;
    private InventorySlot _currentSlot;

    private Rigidbody2D _itemRigidbody;

    //private Item InfoItem;

    private void Start()
    {
        _title = _itemInfo.transform.GetChild(0).GetComponent<TextMeshProUGUI>();
        _description = _itemInfo.transform.GetChild(1).GetComponent<TextMeshProUGUI>();
        _iconImage = _itemInfo.transform.GetChild(2).GetComponent<Image>();

        
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
        float positiveRandomValue = Random.Range(0.2f, 0.5f);
        float negativeRandomValue = Random.Range(-0.5f, -0.2f);
        float xRandomValue = Random.Range(0, 2) == 0 ? positiveRandomValue : negativeRandomValue;
        float yRandomValue = Random.Range(0, 2) == 0 ? positiveRandomValue : negativeRandomValue;

        Vector3 dropPosition = new Vector3(_playerMover.transform.position.x + xRandomValue, _playerMover.transform.position.y + yRandomValue, _playerMover.transform.position.z);
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

        _itemInfo.SetActive(true);
    }

    public void Close()
    {
        _itemInfo.SetActive(false);
    }
}
