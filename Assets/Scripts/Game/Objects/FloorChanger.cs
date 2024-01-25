using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class FloorChanger : MonoBehaviour
{
    [SerializeField] private GameObject _floorChangerWindow;
    [SerializeField] private GameObject[] _choices;

    private TextMeshProUGUI[] _choicesText;
    private Button[] _choicesButtons;
    private int _currentSceneIndex;
    private string _upperText = "Поднятся на этаж выше";
    private string _downerText = "Опустится на этаж ниже";
    private string _stayerText = "Остатся на текущем этаже";

    public static Vector3 LastPlayerPositionBeforeTeleportation { get; private set; }
    public bool isOpend { get; private set; }

    private void Start()
    {
        InitChoises();
    }

    public void Launch()
    {
        if (Input.GetKeyDown(KeyCode.E))
        {
            _currentSceneIndex = SceneManager.GetActiveScene().buildIndex;

            _floorChangerWindow.SetActive(true);

            isOpend = true;

            LastPlayerPositionBeforeTeleportation = Player.instance.position;

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

        _choicesButtons[0].onClick.RemoveAllListeners();
        _choicesButtons[1].onClick.RemoveAllListeners();
        _choicesButtons[2].onClick.RemoveAllListeners();

        SceneManager.LoadScene(_currentSceneIndex + 1);
    }

    private void DownFloor()
    {
        isOpend = false;

        HideChoises();

        _floorChangerWindow.SetActive(false);

        _choicesButtons[0].onClick.RemoveAllListeners();
        _choicesButtons[1].onClick.RemoveAllListeners();
        _choicesButtons[2].onClick.RemoveAllListeners();

        SceneManager.LoadScene(_currentSceneIndex - 1);
    }

    private void StayFloor()
    {
        isOpend = false;

        HideChoises();

        _floorChangerWindow.SetActive(false);

        LastPlayerPositionBeforeTeleportation = Vector3.zero;

        _choicesButtons[0].onClick.RemoveAllListeners();
        _choicesButtons[1].onClick.RemoveAllListeners();
        _choicesButtons[2].onClick.RemoveAllListeners();
    }
}