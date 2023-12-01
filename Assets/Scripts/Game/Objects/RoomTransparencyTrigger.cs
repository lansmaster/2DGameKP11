using UnityEngine;
using UnityEngine.Tilemaps;

[RequireComponent(typeof(TilemapRenderer))]
public class RoomTransparencyTrigger : MonoBehaviour
{
    private TilemapRenderer _tilemapRenderer;
    private SpriteRenderer[] _spriteRenderers;

    private void Start()
    {
        _tilemapRenderer = GetComponent<TilemapRenderer>();
        _spriteRenderers = GetComponentsInChildren<SpriteRenderer>();
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            _tilemapRenderer.sortingOrder = -5;

            foreach (var wall in _spriteRenderers)
            {
                SetTransparency(0.3f, wall);
            }
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            _tilemapRenderer.sortingOrder = 1;

            foreach (var wall in _spriteRenderers)
            {
                SetTransparency(1f, wall);
            }
        }
    }

    private void SetTransparency(float alpha, SpriteRenderer wall)
    {
        Color color = wall.color;

        color.a = alpha;

        wall.color = color;
    }
}
