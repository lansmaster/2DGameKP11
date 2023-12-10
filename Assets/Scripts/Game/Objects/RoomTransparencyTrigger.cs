using UnityEngine;
using UnityEngine.Rendering.Universal;

[RequireComponent(typeof(BoxCollider2D))]
public class RoomTransparencyTrigger : MonoBehaviour
{
    private SpriteRenderer[] _spriteRenderers;
    private Light2D _light;

    private void Start()
    {
        _light = GetComponentInChildren<Light2D>();
        _spriteRenderers = GetComponentsInChildren<SpriteRenderer>();

        _light.enabled = false;
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            _light.enabled = true;

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
            _light.enabled = false;

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
