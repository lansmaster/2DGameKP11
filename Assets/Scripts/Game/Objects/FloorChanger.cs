using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class FloorChanger : MonoBehaviour
{
    [SerializeField] private GameObject _floorChangerWindow;
    [SerializeField] private GameObject[] _choices;
    [SerializeField] private Sprite _floorDoor_Emission;

    private TextMeshProUGUI[] _choicesText;
    private Button[] _choicesButtons;
    private Player _player;
    private int _currentSceneIndex;
    private string _upperText = "�������� �� ���� ����";
    private string _downerText = "��������� �� ���� ����";
    private string _stayerText = "������� �� ������� �����";

    public static Vector3 LastPlayerPositionBeforeTeleportation { get; private set; }
    public static bool isOpend { get; private set; }

    private void Start()
    {
        _player = Player.instance;

        InitChoises();
    }

    public void Actions(GameObject floorDoor)
    {
        _currentSceneIndex = SceneManager.GetActiveScene().buildIndex;

        SpriteRenderer spriteRenderer = floorDoor.GetComponent<SpriteRenderer>();

        spriteRenderer.sprite = _floorDoor_Emission;

        if (Input.GetKeyDown(KeyCode.E))
        {
            _floorChangerWindow.SetActive(true);

            isOpend = true;

            LastPlayerPositionBeforeTeleportation = _player.position;

            switch (_currentSceneIndex)
            {
                case 1:
                    _choices[0].SetActive(true);
                    _choicesText[0].text = _upperText;
                    _choicesButtons[0].onClick.AddListener(UpFloor);

                    _choices[1].SetActive(true);
                    _choicesText[1].text = _stayerText;
                    _choicesButtons[1].onClick.AddListener(StayFloor);

                    break;
                case 2:
                    _choices[0].SetActive(true);
                    _choicesText[0].text = _upperText;
                    _choicesButtons[0].onClick.AddListener(UpFloor);

                    _choices[1].SetActive(true);
                    _choicesText[1].text = _downerText;
                    _choicesButtons[1].onClick.AddListener(DownFloor);

                    _choices[2].SetActive(true);
                    _choicesText[2].text = _stayerText;
                    _choicesButtons[2].onClick.AddListener(StayFloor);

                    break;
                case 3:
                    _choices[0].SetActive(true);
                    _choicesText[0].text = _upperText;
                    _choicesButtons[0].onClick.AddListener(UpFloor);

                    _choices[1].SetActive(true);
                    _choicesText[1].text = _downerText;
                    _choicesButtons[1].onClick.AddListener(DownFloor);

                    _choices[2].SetActive(true);
                    _choicesText[2].text = _stayerText;
                    _choicesButtons[2].onClick.AddListener(StayFloor);

                    break;
                case 4:
                    _choices[0].SetActive(true);
                    _choicesText[0].text = _downerText;
                    _choicesButtons[0].onClick.AddListener(DownFloor);

                    _choices[1].SetActive(true);
                    _choicesText[1].text = _stayerText;
                    _choicesButtons[1].onClick.AddListener(StayFloor);
                    break;
            }
        }
    }
    
    //

    private void InitChoises()
    {
        _choicesText = new TextMeshProUGUI[_choices.Length];
        _choicesButtons = new Button[_choices.Length];

        ushort index = 0;
        foreach(GameObject choice in _choices)
            _choicesText[index++] = choice.GetComponentInChildren<TextMeshProUGUI>();

        index = 0;
        foreach(GameObject choice in _choices)
            _choicesButtons[index++] = choice.GetComponent<Button>();

        HideChoises();
    }

    private void HideChoises()
    {
        foreach (var button in _choices)
            button.SetActive(false);
    }
    
    //

    private void UpFloor()
    {
        isOpend = false;

        HideChoises();

        _floorChangerWindow.SetActive(false);

        SceneManager.LoadScene(_currentSceneIndex + 1);
    }

    private void DownFloor()
    {
        isOpend = false;

        HideChoises();

        _floorChangerWindow.SetActive(false);

        SceneManager.LoadScene(_currentSceneIndex - 1);
    }

    private void StayFloor()
    {
        isOpend = false;

        HideChoises();

        _floorChangerWindow.SetActive(false);

        LastPlayerPositionBeforeTeleportation = Vector3.zero;
    }
}