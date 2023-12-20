using UnityEngine;

public class AudioController : MonoBehaviour
{
    private AudioSource _audioSource;

    private void Start()
    {
        _audioSource = GetComponent<AudioSource>();

        if (!PlayerPrefs.HasKey("GlobalVolume"))
        {
            _audioSource.volume = 1.0f;
        }
    }

    private void Update()
    {
        if (PlayerPrefs.HasKey("GlobalVolume"))
        {
            _audioSource.volume = PlayerPrefs.GetFloat("GlobalVolume");
        }
    }
}
