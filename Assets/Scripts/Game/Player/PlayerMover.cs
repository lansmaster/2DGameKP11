using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
[RequireComponent(typeof(Animator))]
public class PlayerMover : MonoBehaviour
{
    [SerializeField] private float _walkSpeed;
    [SerializeField] private float _runSpeed;

    private Animator _animator;
    private Rigidbody2D _rigidbody;

    private const float _speedChangeRate = 4.0f;

    private float _currentWalkSpeed;
    private float _currentRunSpeed;

    private void Start()
    {
        _animator = GetComponent<Animator>();
        _rigidbody = GetComponent<Rigidbody2D>();

        if(FloorChanger.lastPlayerPositionBeforeTeleportation != null)
        {
            transform.position = FloorChanger.lastPlayerPositionBeforeTeleportation;
        }

        _currentWalkSpeed = _walkSpeed;
        _currentRunSpeed = _runSpeed;
    }

    private void Update()
    {
        Vector3 movement = new Vector3(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical"), 0.0f);

        Animate(movement);

        Move(movement);
    }

    private void Move(Vector3 movement)
    {
        bool runMode = Input.GetKey(KeyCode.LeftShift);

        if (!runMode)
        {
            _currentWalkSpeed = Mathf.MoveTowards(_currentWalkSpeed, _walkSpeed, _speedChangeRate * Time.deltaTime);
            _rigidbody.velocity = new Vector2(movement.x, movement.y) * _currentWalkSpeed;
            _currentRunSpeed = _currentWalkSpeed;
        }
        else
        {
            _currentRunSpeed = Mathf.MoveTowards(_currentRunSpeed, _runSpeed, _speedChangeRate * Time.deltaTime);
            _rigidbody.velocity = new Vector2(movement.x, movement.y) * _currentRunSpeed;
            _currentWalkSpeed = _currentRunSpeed;
        }
    }

    private void Animate(Vector3 movement)
    {
        _animator.SetFloat("Horizontal", movement.x);
        _animator.SetFloat("Vertical", movement.y);
        _animator.SetFloat("Magnitude", movement.magnitude);
    }
}
