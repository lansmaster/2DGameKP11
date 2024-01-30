using UnityEngine;
using UnityEngine.Events;

public class Item : MonoBehaviour
{
    [SerializeField] private Sprite _default, _emission;
    [SerializeField] public ItemAsset itemAsset;

    private PlayerActions _playerActions;
    private SpriteRenderer _spriteRenderer;

    public static UnityAction<ItemAsset> ItemPickUped;

    private void Start()
    {
        _playerActions = Player.instance.actions;
        _spriteRenderer = GetComponent<SpriteRenderer>();
        
        if (DataBase.ExecuteQueryWithAnswer($"SELECT EXISTS(SELECT * FROM Inventory WHERE ItemAssetName = '{itemAsset.Name}')") != "0")
        {
            Destroy(gameObject);
        }
    }

    public void PickUp()
    {
        if (Input.GetKeyDown(KeyCode.E))
        {
            ItemPickUped?.Invoke(itemAsset);
            Destroy(gameObject);
        }
    }

    public void EnableEmission(bool enable)
    {
        if (enable)
        {
            _spriteRenderer.sprite = _emission;
        }
        else
        {
            _playerActions.PlayerApproachedTheItem -= EnableEmission;

            _spriteRenderer.sprite = _default;
        }
    }
}
