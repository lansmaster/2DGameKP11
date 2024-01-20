
using UnityEngine;

public class DoorBehaviorOpenedDefault : IDoorBehavior
{
    private Sprite _sprite;
    private SpriteRenderer _spriteRenderer;
    private BoxCollider2D _boxCollider;
    private PolygonCollider2D _polygonCollider;

    public DoorBehaviorOpenedDefault(Sprite sprite, SpriteRenderer spriteRenderer, BoxCollider2D boxCollider, PolygonCollider2D polygonCollider)
    {
        _sprite = sprite;
        _spriteRenderer = spriteRenderer;
        _boxCollider = boxCollider;
        _polygonCollider = polygonCollider;
    }

    public void Enter()
    {
        _spriteRenderer.sprite = _sprite;
        _boxCollider.enabled = false;
        _polygonCollider.enabled = true;
    }

    public void Exit()
    {
        
    }

    public void Update()
    {
        
    }
}
