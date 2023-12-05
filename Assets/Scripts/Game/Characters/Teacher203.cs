using UnityEngine;
using UnityEngine.UI;
using TMPro;
using Unity.VisualScripting;
using UnityEditor.Search;

public class Teacher203 : MonoBehaviour
{
    [SerializeField] Sprite _dialogImage, _playerDialogImage;

    [SerializeField] private GameObject _dialogPanel;

    private Image _dialogIcon;
    private TextMeshProUGUI _text;
    private Button _yesButton;
    private TextMeshProUGUI _yesButtonText;
    private Button _notButton;
    private TextMeshProUGUI _notButtonText;

    public bool dialogIsStart = false;

    private void Start()
    {
        _dialogIcon = _dialogPanel.transform.GetChild(0).GetComponent<Image>();
        _text = _dialogPanel.transform.GetChild(1).GetComponent<TextMeshProUGUI>();
        _yesButton = _dialogPanel.transform.GetChild(2).GetComponent<Button>();
        _yesButtonText = _yesButton.GetComponentInChildren<TextMeshProUGUI>();
        _notButton = _dialogPanel.transform.GetChild(3).GetComponent<Button>();
        _notButtonText = _notButton.GetComponentInChildren<TextMeshProUGUI>();

        ShowButtons(false);
    }

    private void Update()
    {

    }

    public void StartDialog()
    {
        if (Input.GetKeyDown(KeyCode.E))
        {
            dialogIsStart = true;

            GetDialog();
        }
    }

    private void GetDialog()
    {
        ShowDialog(true);

        _dialogIcon.sprite = _dialogImage;

        _text.text = "Я расскажу историю о том как я...";

        if (Input.GetKeyDown(KeyCode.Space)) {

            _text.gameObject.SetActive(false);
            _dialogIcon.sprite = _playerDialogImage;

            ShowButtons(true);
            _yesButtonText.text = "Окей...";

            _notButtonText.text = "*Отойти подальше*";

            _notButton.onClick.AddListener(() => ShowDialog(false));
        }

        
    }

    private void ShowDialog(bool show)
    {
        _dialogPanel.SetActive(show);
    }

    private void ShowButtons(bool show)
    {
        _yesButton.gameObject.SetActive(show);
        _notButton.gameObject.SetActive(show);
    }
}
