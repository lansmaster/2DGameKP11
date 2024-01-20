
using UnityEngine;

public class DoorBehaviorOpenedEmission : IDoorBehavior
{
    private Sprite _sprite;
    private SpriteRenderer _spriteRenderer;
    private BoxCollider2D _boxCollider;
    private PolygonCollider2D _polygonCollider;
    private RoomNumberSign _roomNumberSign;

    public DoorBehaviorOpenedEmission(Sprite sprite, SpriteRenderer spriteRenderer, BoxCollider2D boxCollider, PolygonCollider2D polygonCollider, RoomNumberSign roomNumberSign)
    {
        _sprite = sprite;
        _spriteRenderer = spriteRenderer;
        _boxCollider = boxCollider;
        _polygonCollider = polygonCollider;
        _roomNumberSign = roomNumberSign;
    }
    public void Enter()
    {
        _spriteRenderer.sprite = _sprite;
        _boxCollider.enabled = false;
        _polygonCollider.enabled = true;

        if (_roomNumberSign != null)
        {
            _roomNumberSign.Show(true);
        }
    }

    public void Exit()
    {
        if (_roomNumberSign != null)
        {
            _roomNumberSign.Show(false);
        }
    }

    public void Update()
    {
        
    }
}
