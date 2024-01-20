using System;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(SpriteRenderer))]
[RequireComponent(typeof(BoxCollider2D))]
[RequireComponent(typeof(PolygonCollider2D))]
public class Door : MonoBehaviour
{
    [SerializeField] private Sprite _closedDoorDefault, _openedDoorDefault, _closedDoorEmission, _openedDoorEmission;
    
    public bool isOpened { get; private set; }

    private PlayerActions _playerActions;
    private SpriteRenderer _spriteRenderer;
    private BoxCollider2D _boxCollider;
    private PolygonCollider2D _polygonCollider;
    private RoomNumberSign _roomNumberSign;

    private Dictionary<Type, IDoorBehavior> _behaviorsMap;
    private IDoorBehavior _behaviorCurrent;

    private void Start()
    {
        _playerActions = Player.instance.actions;
        _spriteRenderer = GetComponent<SpriteRenderer>();
        _boxCollider = GetComponent<BoxCollider2D>();
        _polygonCollider = GetComponent<PolygonCollider2D>();
        _roomNumberSign = GetComponent<RoomNumberSign>();

        InitBehaviors();
        SetBehaviorByDefault();
    }

    public void Actions()
    {
        _playerActions.PlayerApproachedTheDoor += SwitchBehavior;

        if (Input.GetKeyDown(KeyCode.E))
        {
            OpeningSwitch();
        }
    }

    private void OpeningSwitch()
    {
        if (isOpened)
        {
            isOpened = false;
        }
        else
        {
            isOpened = true;
        }
    }

    private void SwitchBehavior(bool enable)
    {
        if (enable)
        {
            if (isOpened)
                SetBehaviorOpenedEmission();
            else 
                SetBehaviorClosedEmission();
        }
        else
        {
            if (isOpened)
                SetBehaviorOpenedDefault();
            else
                SetBehaviorClosedDefault();

            _playerActions.PlayerApproachedTheDoor -= SwitchBehavior;
        }
    }

    private void InitBehaviors()
    {
        _behaviorsMap = new Dictionary<Type, IDoorBehavior>();

        _behaviorsMap[typeof(DoorBehaviorClosedDefault)] = new DoorBehaviorClosedDefault(_closedDoorDefault, _spriteRenderer, _boxCollider, _polygonCollider);
        _behaviorsMap[typeof(DoorBehaviorOpenedDefault)] = new DoorBehaviorOpenedDefault(_openedDoorDefault, _spriteRenderer, _boxCollider, _polygonCollider);
        _behaviorsMap[typeof(DoorBehaviorClosedEmission)] = new DoorBehaviorClosedEmission(_closedDoorEmission, _spriteRenderer, _boxCollider, _polygonCollider, _roomNumberSign);
        _behaviorsMap[typeof(DoorBehaviorOpenedEmission)] = new DoorBehaviorOpenedEmission(_openedDoorEmission, _spriteRenderer, _boxCollider, _polygonCollider, _roomNumberSign);
    }

    private void SetBehavior(IDoorBehavior newBehavior)
    {
        if(_behaviorCurrent != null)
            _behaviorCurrent.Exit();

        _behaviorCurrent = newBehavior;
        _behaviorCurrent.Enter();
    }

    private void SetBehaviorByDefault()
    {
        SetBehaviorClosedDefault();
    }

    private IDoorBehavior GetBehavior<T>() where T : IDoorBehavior
    {
        var type = typeof(T);
        return _behaviorsMap[type];
    }

    private void Update()
    {
        if (_behaviorCurrent != null)
            _behaviorCurrent.Update();
    }

    public void SetBehaviorClosedDefault()
    {
        var behavior = GetBehavior<DoorBehaviorClosedDefault>();
        SetBehavior(behavior);
    }
    
    public void SetBehaviorOpenedDefault()
    {
        var behavior = GetBehavior<DoorBehaviorOpenedDefault>();
        SetBehavior(behavior);
    }
    
    public void SetBehaviorClosedEmission()
    {
        var behavior = GetBehavior<DoorBehaviorClosedEmission>();
        SetBehavior(behavior);
    }
    
    public void SetBehaviorOpenedEmission()
    {
        var behavior = GetBehavior<DoorBehaviorOpenedEmission>();
        SetBehavior(behavior);
    } 
}
