using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(SpriteRenderer))]
[RequireComponent(typeof(BoxCollider2D))]
public class OpeningTheDoor : MonoBehaviour
{
    [SerializeField] private PlayerMove _player;
    [SerializeField] private Sprite OpenedDoor, ClosedDoor, OpenedDoor_Emission, ClosedDoor_Emission;
    
    private const float _interactionDistance = 1.5f;
    private bool _isOpened;
    private SpriteRenderer _spriteRenderer;
    private BoxCollider2D _boxCollider;

    private void Awake()
    {
        _spriteRenderer = GetComponent<SpriteRenderer>();
        _boxCollider = GetComponent<BoxCollider2D>();
    }

    private void Update()
    {
        float currentDistance = Vector3.Distance(transform.position, _player.transform.position);
        if (currentDistance < _interactionDistance)
        {
            if (_boxCollider.enabled == true)
            {
                _spriteRenderer.sprite = ClosedDoor_Emission;
                if (Input.GetKeyDown(KeyCode.E))
                {
                    MoveDoor();
                }
            }
            else
            {
                _spriteRenderer.sprite = OpenedDoor_Emission;
                if (Input.GetKeyDown(KeyCode.E))
                {
                    MoveDoor();
                }
            }
        }
        else
        {
            if (_boxCollider.enabled == true)
            {
                _spriteRenderer.sprite = ClosedDoor;
            }
            else
            {
                _spriteRenderer.sprite = OpenedDoor;
            }
        }
    }

    private void MoveDoor()
    {
        if (_boxCollider.enabled == true)
        {
            _spriteRenderer.sprite = OpenedDoor;
            _boxCollider.enabled = false;
        }
        else
        {
            _spriteRenderer.sprite = ClosedDoor;
            _boxCollider.enabled = true;
        }
    }
}
