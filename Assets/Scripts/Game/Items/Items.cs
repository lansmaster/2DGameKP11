using System;
using System.Collections.Generic;
using UnityEngine;

public class Items : MonoBehaviour
{
    [SerializeField] private GameObject[] _itemPrefabs;

    private readonly Dictionary<string, GameObject> _items = new();

    public static Items Instance { get; private set; }

    private void Awake()
    {
        if (Instance == null)
            Instance = this;
    }

    private void Start()
    {
        foreach (var itemPrefab in _itemPrefabs)
        {
            Item item = itemPrefab.GetComponent<Item>();
            _items.Add(item.itemAsset.Name, itemPrefab);
        }
    }

    public GameObject GetItemPrefab(string name)
    {
        return _items[name];
    }

    public ItemAsset GetItemAsset(string name)
    {
        return _items[name].GetComponent<Item>().itemAsset;
    }
}
