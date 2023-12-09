using System;
using UnityEngine;

public class Player : MonoBehaviour
{
    private PlayerMover _mover;
    private PlayerActions _actions;

    private Rigidbody2D _rigidbody;
    private Animator _animator;
    private const float _speedChangeRate = 6;
    private const float _animationChangeRate = 2;

    private Inventory _inventory;
    private bool _inventoryIsOpened = false;

    private void Start()
    {
        _mover = GetComponent<PlayerMover>();
        _actions = GetComponent<PlayerActions>();

        _rigidbody = GetComponent<Rigidbody2D>();
        _animator = GetComponent<Animator>();

        _inventory = FindObjectOfType<Inventory>();

        _inventory.InventoryIsOpened += SetActiveMover;
    }

    private void Update()
    {
        if(_inventoryIsOpened == true)
        {
            SlowingDownPlayer();
        }
    }

    private void SetActiveMover(bool active)
    {
        _mover.enabled = !active;
        _inventoryIsOpened = true;
    }

    private void SlowingDownPlayer()
    {
            _rigidbody.velocity = new Vector2(Mathf.MoveTowards(_rigidbody.velocity.x, 0, _speedChangeRate * Time.deltaTime), Mathf.MoveTowards(_rigidbody.velocity.y, 0, _speedChangeRate * Time.deltaTime));

            _animator.SetFloat("Horizontal", Mathf.MoveTowards(_animator.GetFloat("Horizontal"), 0, _animationChangeRate * Time.deltaTime));
            _animator.SetFloat("Vertical", Mathf.MoveTowards(_animator.GetFloat("Vertical"), 0, _animationChangeRate * Time.deltaTime));
            _animator.SetFloat("Magnitude", Mathf.MoveTowards(_animator.GetFloat("Magnitude"), 0, _animationChangeRate * Time.deltaTime));
    }
}
