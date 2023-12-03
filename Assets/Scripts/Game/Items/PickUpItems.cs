using UnityEngine;

[RequireComponent(typeof(Animator))]
public class PickUpItems : MonoBehaviour
{
    [SerializeField] private Inventory _inventory;

    public AssetItem item;

    private GameObject _itemGameObject;
    private Animator _animator;

    private void Start()
    {
        _animator = GetComponent<Animator>();
        _itemGameObject = gameObject;

        _animator.SetBool("Emission", false);
    }

    private void FixedUpdate()
    {
        _animator.SetBool("Emission", false);
    }

    public void PickUp()
    {
        _animator.SetBool("Emission", true);

        if (Input.GetKeyDown(KeyCode.E))
        {
            _inventory.PutInEmptySlot(item, _itemGameObject);
            gameObject.SetActive(false);
        }
    }
}
