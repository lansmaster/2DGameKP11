using UnityEngine;

[CreateAssetMenu(fileName = "New item", menuName = "Inventory/Item")]
public class AssetItem : ScriptableObject, IItem
{
    public string Name => _name;
    public string Description => _description;
    public Sprite Icon => _icon;
    
    [Header("Базовые характеристики")]

    [SerializeField] private string _name;
    [SerializeField] private string _description;
    [SerializeField] private Sprite _icon;
}

    