using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

[RequireComponent(typeof(TilemapRenderer))]
public class CeilingSwitch : MonoBehaviour
{
    private TilemapRenderer _tilemapRenderer;

    private void Start()
    {
        _tilemapRenderer = GetComponent<TilemapRenderer>();
    }

    private void OnTriggerStay2D(Collider2D collider2D)
    {
        _tilemapRenderer.sortingOrder = -5;
    }

    private void OnTriggerExit2D(Collider2D collider2D)
    {
        _tilemapRenderer.sortingOrder = 1;
    }
}
