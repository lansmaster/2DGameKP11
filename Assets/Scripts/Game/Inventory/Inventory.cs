using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using System;
using UnityEngine;
using UnityEngine.UI;

public class Inventory : MonoBehaviour
{
    [SerializeField] private GameObject _inventory;
    [SerializeField] private Transform _slotsTransform;
    public static InventorySlot[] inventorySlots = new InventorySlot[16];

    private bool _isOpened = false;

    public static Item[] itemsInSlotsToSave;
    public static Image[] itemIconsToSave;
    public static GameObject[] itemGameObjectsToSave;

    private void Start()
    {
        for (int i = 0; i < inventorySlots.Length; i++)
        {
            inventorySlots[i] = _slotsTransform.GetChild(i).GetComponent<InventorySlot>();
        }
    }

    public void PutInEmptySlot(Item item, GameObject itemObject)
    {
        for (int i = 0; i < inventorySlots.Length; i++)
        {
            if (inventorySlots[i].ItemInSlot == null)
            {
                inventorySlots[i].PutInSlot(item, itemObject);
                return;
            }
        }
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.I))
        {
            if (_isOpened)
            {
                _inventory.SetActive(false);
                _isOpened = false;
            }
            else
            {
                _inventory.SetActive(true);
                _isOpened = true;
            }
        }
    }

    public static void SaveSlots()
    {
        itemsInSlotsToSave = new Item[inventorySlots.Length];
        itemIconsToSave = new Image[inventorySlots.Length];
        itemGameObjectsToSave = new GameObject[inventorySlots.Length];

        for (int i = 0;i < inventorySlots.Length; i++)
        {
            itemsInSlotsToSave[i] = inventorySlots[i].ItemInSlot;
            itemIconsToSave[i] = inventorySlots[i].slotIcon;
            itemGameObjectsToSave[i] = inventorySlots[i].itemGameObject;
        }

        SaveData.SaveInventory();
    }
}

[Serializable]
class SaveData
{
    public Item[] savedItemsInSlots;
    public Image[] savedIcons;
    public GameObject[] savedItemGameObjects;

    public static void SaveInventory()
    {
        BinaryFormatter binaryFormatter = new BinaryFormatter();
        FileStream file = File.Create(Application.persistentDataPath + "/Inventory.dat");
        SaveData data = new SaveData();

        data.savedItemsInSlots = Inventory.itemsInSlotsToSave;
        data.savedIcons = Inventory.itemIconsToSave;
        data.savedItemGameObjects = Inventory.itemGameObjectsToSave;

        binaryFormatter.Serialize(file, data);
        file.Close();
        Debug.Log("Успешное сохранение");
    }
}