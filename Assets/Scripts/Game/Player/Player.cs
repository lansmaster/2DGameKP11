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
    private DialogueWindow _dialogueWindow;

    public Vector3 Position
    {
        get => transform.position;
        private set { }
    }

    private void Start()
    {
        _mover = GetComponent<PlayerMover>();
        _actions = GetComponent<PlayerActions>();

        _rigidbody = GetComponent<Rigidbody2D>();
        _animator = GetComponent<Animator>();

        _inventory = FindObjectOfType<Inventory>();
        _dialogueWindow = FindObjectOfType<DialogueWindow>();
    }

    private void Update()
    {
        if (_dialogueWindow.IsPlaying || _inventory.IsOpened || FloorChanger.IsOpend)
        {
            SetActiveMover(false);
            SlowingDownPlayer();
        }
        else
        {
            SetActiveMover(true);
        }
    }

    private void SetActiveMover(bool active)
    {
        _mover.enabled = active;
    }

    private void SlowingDownPlayer()
    {
        _rigidbody.velocity = new Vector2(Mathf.MoveTowards(_rigidbody.velocity.x, 0, _speedChangeRate * Time.deltaTime), Mathf.MoveTowards(_rigidbody.velocity.y, 0, _speedChangeRate * Time.deltaTime));

        _animator.SetFloat("Horizontal", Mathf.MoveTowards(_animator.GetFloat("Horizontal"), 0, _animationChangeRate * Time.deltaTime));
        _animator.SetFloat("Vertical", Mathf.MoveTowards(_animator.GetFloat("Vertical"), 0, _animationChangeRate * Time.deltaTime));
        _animator.SetFloat("Magnitude", Mathf.MoveTowards(_animator.GetFloat("Magnitude"), 0, _animationChangeRate * Time.deltaTime));
    }
}
