using System.Collections.Generic;
using UnityEngine;

public class Items : MonoBehaviour
{
    [SerializeField] private GameObject[] _itemPrefabs;
    
    private readonly Dictionary<string, GameObject> _items = new();

    private void Start()
    {
        foreach (var itemPrefab in _itemPrefabs)
        {
            Item item = itemPrefab.GetComponent<Item>();
            _items.Add(item.Name, itemPrefab);
        }

        DontDestroyOnLoad(gameObject);
    }

    public GameObject GetItem(string name)
    {
        return _items[name];
    }
}
