using UnityEngine;

public class PickUpObjects : MonoBehaviour
{
    [SerializeField] private Inventory _inventory;

    public Item item;
    private GameObject _itemObject;

    private void Start()
    {
        _itemObject = gameObject; // на будущее, для выбрасывания предмета
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player")) // чтобы работало у игрока обязательно должен быть тег Player
        {
            // кладем в инвентарь Item
            _inventory.PutInEmptySlot(item, _itemObject);
            gameObject.SetActive(false); // выключает объект при "столкновении"
        }
    }


}
