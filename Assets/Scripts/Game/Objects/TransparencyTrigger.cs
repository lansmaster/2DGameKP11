using UnityEngine;

[RequireComponent(typeof(SpriteRenderer))]
public class TransparencyTrigger : MonoBehaviour
{
    private SpriteRenderer _spriteRenderer;

    private void Start()
    {
        _spriteRenderer = GetComponent<SpriteRenderer>();
    }

    private void OnTriggerStay2D(Collider2D collider2D)
    {
        if (collider2D.gameObject.tag == "Player")
        {
            SetTransparency(0.3f);
        }
    }

    private void OnTriggerExit2D(Collider2D collider2D)
    {
        if (collider2D.gameObject.tag == "Player")
        {
            SetTransparency(1f);
        }
    }

    private void SetTransparency(float alpha)
    {
        Color color = _spriteRenderer.color; 
        
        color.a = alpha;

        _spriteRenderer.color = color;
    }
}