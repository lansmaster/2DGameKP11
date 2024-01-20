using UnityEngine;

public class Player : MonoBehaviour
{
    [SerializeField] private DialogueWindow _dialogueWindow;
    [SerializeField] private PauseMenu _pauseMenu;

    private const float _speedChangeRate = 6;
    private const float _animationChangeRate = 2;

    private Rigidbody2D _rigidbody;
    private Animator _animator;
    private Inventory _inventory;

    public static Player instance { get; private set; }
    public PlayerActions actions { get; private set; }
    public PlayerMover mover { get; private set; }
    public Vector3 position
    {
        get => transform.position;
        private set { }
    }

    private void Awake()
    {
        if (instance == null)
            instance = this;

        mover = GetComponent<PlayerMover>();
        actions = GetComponent<PlayerActions>();
    }

    private void Start()
    {
        _rigidbody = GetComponent<Rigidbody2D>();
        _animator = GetComponent<Animator>();

        _inventory = Inventory.instance;
    }

    private void Update()
    {
        if (_dialogueWindow.isPlaying || _inventory.isOpened || FloorChanger.isOpend)
        {
            SetActiveMover(false);
            SlowingDownPlayer();
        }
        else
        {
            SetActiveMover(true);
        }

        if(_inventory.isOpened)
            _pauseMenu.canOpen = false;
        else
            _pauseMenu.canOpen = true;

    }

    private void SetActiveMover(bool active)
    {
        mover.enabled = active;
    }

    private void SlowingDownPlayer()
    {
        _rigidbody.velocity = new Vector2(Mathf.MoveTowards(_rigidbody.velocity.x, 0, _speedChangeRate * Time.deltaTime), Mathf.MoveTowards(_rigidbody.velocity.y, 0, _speedChangeRate * Time.deltaTime));

        _animator.SetFloat("Horizontal", Mathf.MoveTowards(_animator.GetFloat("Horizontal"), 0, _animationChangeRate * Time.deltaTime));
        _animator.SetFloat("Vertical", Mathf.MoveTowards(_animator.GetFloat("Vertical"), 0, _animationChangeRate * Time.deltaTime));
        _animator.SetFloat("Magnitude", Mathf.MoveTowards(_animator.GetFloat("Magnitude"), 0, _animationChangeRate * Time.deltaTime));
    }
}
