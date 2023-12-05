using UnityEngine;
using UnityEngine.UI;
using TMPro;
using Unity.VisualScripting;

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

    public void ShowDialog()
    {
        if (Input.GetKeyDown(KeyCode.E))
        {
            GetDialog();
        }
    }

    private void GetDialog()
    {
        _dialogPanel.SetActive(true);

        _dialogIcon.sprite = _dialogImage;

        _text.text = "Я расскажу историю о том как я...";

        if (Input.GetKeyDown(KeyCode.R)) {

            _text.gameObject.SetActive(false);
            _dialogIcon.sprite = _playerDialogImage;

            ShowButtons(true);
            _yesButtonText.text = "Окей...";

            _notButtonText.text = "*Отойти подальше*";

            if (_yesButton.onClick != null)
            {
                _dialogPanel.SetActive(false);
            }
            if (_notButton.onClick != null)
            {
                _dialogPanel.SetActive(false);
            }
        }

        
    }

    private void ShowButtons(bool show)
    {
        _yesButton.gameObject.SetActive(show);
        _notButton.gameObject.SetActive(show);
    }
}
