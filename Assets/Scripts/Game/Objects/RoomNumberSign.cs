using UnityEngine;
using TMPro;

public class RoomNumberSign : MonoBehaviour
{
    [SerializeField] private string _roomNumber;
    [SerializeField] private GameObject _roomNumberSign;

    private TextMeshProUGUI _roomNumberSignText;
    private SpriteRenderer _roomNumberSignImage;
    private Animator _roomNumberSignAnimator;

    private void Start()
    {
        _roomNumberSignText = _roomNumberSign.transform.GetChild(0).GetChild(0).GetComponent<TextMeshProUGUI>();
        _roomNumberSignImage = _roomNumberSign.GetComponent<SpriteRenderer>();
        _roomNumberSignAnimator = _roomNumberSign.GetComponent<Animator>();

        _roomNumberSignText.text = _roomNumber;
    }

    public void Show(bool show)
    {
        if (!show) 
        { 
            _roomNumberSignAnimator.StartPlayback();  
        } 
        else 
        { 
            _roomNumberSignAnimator.StopPlayback(); 
        }

        _roomNumberSignImage.enabled = show;
        _roomNumberSignText.enabled = show;
    }
}
