using UnityEngine;
using TMPro;

public class RoomNumberSign : MonoBehaviour
{
    [SerializeField] private string _roomNumber;
    [SerializeField] private GameObject _roomNumberSign;

    private TextMeshProUGUI _text;
    private SpriteRenderer _spriteRenderer;
    private Animator _animator;
    private GameObject _canvas;

    private void Start()
    {
        _text = _roomNumberSign.transform.GetChild(0).GetChild(0).GetComponent<TextMeshProUGUI>();
        _spriteRenderer = _roomNumberSign.GetComponent<SpriteRenderer>();
        _animator = _roomNumberSign.GetComponent<Animator>();
        _canvas = _roomNumberSign.transform.GetChild(0).gameObject;

        _text.text = _roomNumber;

        Show(false);
    }

    public void Show(bool show)
    {
        if (!show) 
        { 
            _animator.StartPlayback();  
        } 
        else 
        { 
            _animator.StopPlayback(); 
        }

        _canvas.SetActive(show);
        _spriteRenderer.enabled = show;
    }
}
