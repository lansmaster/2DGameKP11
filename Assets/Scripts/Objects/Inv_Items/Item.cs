using UnityEngine;

[CreateAssetMenu(fileName = "New item", menuName = "Inventory/Item")]

public class Item : ScriptableObject
{
    [Header("Базовые характеристики")]
    public string Name = " ";
    public string Description = " ";
    public Sprite icon = null;

    // сюда нужно будет вписать свойства если они будут
}
