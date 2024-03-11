using UnityEngine;
using UnityEngine.UI;

public class ChangingVolume : MonoBehaviour
{
    [SerializeField] private Slider _volumeSlider;

    private float _oldVolume;

    private void Start()
    {
        if (PlayerPrefs.HasKey("GlobalVolume"))
        {
            _oldVolume = _volumeSlider.value;
            _volumeSlider.value = PlayerPrefs.GetFloat("GlobalVolume");
        }
        else
        {
            _volumeSlider.value = 1;
        }
    }

    private void Update()
    {
        if (_oldVolume != _volumeSlider.value)
        {
            PlayerPrefs.SetFloat("GlobalVolume", _volumeSlider.value);
            PlayerPrefs.Save();
            _oldVolume = _volumeSlider.value;
        }
    }
}
