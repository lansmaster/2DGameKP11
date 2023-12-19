using UnityEngine;

public class FloorDoor : MonoBehaviour
{
    [SerializeField] private Sprite _default;

    private SpriteRenderer _spriteRenderer;

    private void Start()
    {
        _spriteRenderer = GetComponent<SpriteRenderer>();
    }

    private void FixedUpdate()
    {
        _spriteRenderer.sprite = _default;
    }
}
