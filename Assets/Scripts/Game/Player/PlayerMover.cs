using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class PlayerMover : MonoBehaviour
{
    [SerializeField] private float _speed;

    private Transform _transform;
    private Animator _animator;
    private Rigidbody2D _rigidbody;

    private void Start()
    {
        _transform = GetComponent<Transform>();
        _animator = GetComponent<Animator>();
        _rigidbody = GetComponent<Rigidbody2D>();

        if(FloorChanger.lastPlayerPositionBeforeTeleportation != null)
        {
            _transform.position = FloorChanger.lastPlayerPositionBeforeTeleportation;
        }
        
    }

    private void FixedUpdate()
    {
        Vector3 movement = new Vector3(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical"), 0.0f);
        

        _animator.SetFloat("Horizontal", movement.x);
        _animator.SetFloat("Vertical", movement.y);
        _animator.SetFloat("Magnitude", movement.magnitude);

        _rigidbody.velocity = new Vector2(movement.x, movement.y) * _speed;
    }
}
