using System;
using System.Collections.Generic;
using UnityEngine;

public class Items : MonoBehaviour
{
    [SerializeField] private GameObject[] _itemPrefabs;

    private readonly Dictionary<string, GameObject> _items = new();

    public static Items instance { get; private set; }

    private void Awake()
    {
        if (instance == null)
            instance = this;
    }

    private void Start()
    {
        foreach (var itemPrefab in _itemPrefabs)
        {
            Item item = itemPrefab.GetComponent<Item>();
            _items.Add(item.Name, itemPrefab);
        }
    }

    public GameObject GetItem(string name)
    {
        return _items[name];
    }
}
