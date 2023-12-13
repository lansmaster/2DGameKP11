using UnityEngine;
using TMPro;

public class RoomNumberSign : MonoBehaviour
{
    [SerializeField] private string _roomNumber;

    [SerializeField] private GameObject _roomNumberSign;
    [SerializeField] private TextMeshProUGUI _roomNuberSignText;

    private void Start()
    {
        _roomNuberSignText.text = _roomNumber;
    }

    public void Show(bool show)
    {
        _roomNumberSign.SetActive(show);
    }
}
