using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TransparencyTrigger : MonoBehaviour
{
    private SpriteRenderer _spriteRenderer;

    private void Start()
    {
        _spriteRenderer = GetComponent<SpriteRenderer>();
    }

    private void OnTriggerEnter2D(Collider2D collider2D)
    {
        SetTransparency(0.5f);
    }

    private void OnTriggerExit2D(Collider2D collider2D)
    {
        SetTransparency(1f);
    }

    private void SetTransparency(float alpha)
    {
        Color color = _spriteRenderer.color; 
        
        color.a = alpha;

        _spriteRenderer.color = color;
    }
}
