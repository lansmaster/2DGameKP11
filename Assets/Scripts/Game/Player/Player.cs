using System.Data;
using System.Globalization;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Player : MonoBehaviour
{
    [SerializeField] private DialogueWindow _dialogueWindow;
    [SerializeField] private PauseMenu _pauseMenu;
    [SerializeField] private FloorChanger _floorChanger;
    [SerializeField] private InventoryView _inventoryView;

    private Rigidbody2D _rigidbody;
    private Animator _animator;
    
    private const float _speedChangeRate = 6;
    private const float _animationChangeRate = 2;

    public static Player Instance { get; private set; }
    public PlayerActions Actions { get; private set; }
    public PlayerMover Mover { get; private set; }
    public Vector3 Position
    {
        get => transform.position;
        private set => transform.position = value;
    }

    private void Awake()
    {
        if (Instance == null)
            Instance = this;

        Mover = GetComponent<PlayerMover>();
        Actions = GetComponent<PlayerActions>();
    }

    private void Start()
    {
        _rigidbody = GetComponent<Rigidbody2D>();
        _animator = GetComponent<Animator>();
     
        Mover.Init();

        LoadPosition();
    }

    private void OnDestroy()
    {
        SavePosition();
    }

    private void OnDisable()
    {
        SavePosition();
    }

    private void Update()
    {
        if (_dialogueWindow.IsPlaying || _inventoryView.IsOpened || _floorChanger.IsOpened)
        {
            SetActiveMover(false);
            SlowingDownPlayer();
        }
        else
        {
            SetActiveMover(true);
        }

        if (_inventoryView.IsOpened)
            _pauseMenu.CanOpen = false;
        else
            _pauseMenu.CanOpen = true;

    }

    private void SetActiveMover(bool active)
    {
        Mover.enabled = active;
    }

    private void SlowingDownPlayer()
    {
        _rigidbody.velocity = new Vector2(Mathf.MoveTowards(_rigidbody.velocity.x, 0, _speedChangeRate * Time.deltaTime), Mathf.MoveTowards(_rigidbody.velocity.y, 0, _speedChangeRate * Time.deltaTime));

        _animator.SetFloat("Horizontal", Mathf.MoveTowards(_animator.GetFloat("Horizontal"), 0, _animationChangeRate * Time.deltaTime));
        _animator.SetFloat("Vertical", Mathf.MoveTowards(_animator.GetFloat("Vertical"), 0, _animationChangeRate * Time.deltaTime));
        _animator.SetFloat("Magnitude", Mathf.MoveTowards(_animator.GetFloat("Magnitude"), 0, _animationChangeRate * Time.deltaTime));
    }

    private void SavePosition()
    {
        int CurrentSceneIndex = SceneManager.GetActiveScene().buildIndex;

        if(CurrentSceneIndex == 0)
            return;

        string XAxisCurrentValue = Position.x.ToString("0.00", CultureInfo.GetCultureInfo("en-US"));
        string YAxisCurrentValue = Position.y.ToString("0.00", CultureInfo.GetCultureInfo("en-US"));

        DataBase.ExecuteQueryWithoutAnswer(string.Format("UPDATE PlayerPosition SET XAxis = {0}, YAxis = {1}, SceneIndex = {2} WHERE id = 1", XAxisCurrentValue, YAxisCurrentValue, CurrentSceneIndex));
    }

    private void LoadPosition()
    {
        if (DataBase.ExecuteQueryWithAnswer("SELECT EXISTS(SELECT * FROM PlayerPosition)") != "0")
        {
            DataTable PlayerPosition = DataBase.GetTable("SELECT * FROM PlayerPosition WHERE id = 1");

            float XAxisSavedValue = float.Parse(PlayerPosition.Rows[0][1].ToString());
            float YAxisSavedValue = float.Parse(PlayerPosition.Rows[0][2].ToString());
            int SavedSceneIndex = int.Parse(PlayerPosition.Rows[0][3].ToString());

            int CurrentSceneIndex = SceneManager.GetActiveScene().buildIndex;
            if (SavedSceneIndex != CurrentSceneIndex)
                SceneManager.LoadScene(SavedSceneIndex);

            Position = new Vector3(XAxisSavedValue, YAxisSavedValue, 0);
        }
        else
        {
            Position = new Vector3(-2.4f, 4.05f, 0f);

            int CurrentSceneIndex = SceneManager.GetActiveScene().buildIndex;

            if (CurrentSceneIndex == 0)
                return;

            string XAxisCurrentValue = Position.x.ToString("0.00", CultureInfo.GetCultureInfo("en-US"));
            string YAxisCurrentValue = Position.y.ToString("0.00", CultureInfo.GetCultureInfo("en-US"));

            DataBase.ExecuteQueryWithoutAnswer($"INSERT INTO PlayerPosition (XAxis, YAxis, SceneIndex) VALUES ({XAxisCurrentValue}, {YAxisCurrentValue}, {CurrentSceneIndex})");
        }
    }
}
