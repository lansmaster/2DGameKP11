using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using System;
using UnityEngine;
using UnityEngine.UI;

public class Inventory : MonoBehaviour
{
    [SerializeField] private GameObject _inventory;
    [SerializeField] private Transform _slotsTransform;
    private InventorySlot[] inventorySlots = new InventorySlot[16];

    private bool _isOpened = false;

    private void Start()
    {
        _inventory.transform.localScale = Vector3.zero;

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
                _inventory.transform.localScale = Vector3.zero;
                _isOpened = false;
            }
            else
            {
                _inventory.transform.localScale = Vector3.one;
                _isOpened = true;
            }
        }
    }
}