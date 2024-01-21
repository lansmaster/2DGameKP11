using UnityEngine;

[CreateAssetMenu(fileName = "New item", menuName = "Items/Item")]
public class ItemAsset : ScriptableObject, IItem
{
    public string Name => _name;
    public string Description => _description;
    public Sprite Icon => _icon;
    
    [Header("Базовые характеристики")]

    [SerializeField] private string _name;
    [SerializeField, TextArea] private string _description;
    [SerializeField] private Sprite _icon;
}