using UnityEngine;

public class PlayerTracker : MonoBehaviour
{
    private Player _player;

    private void Start()
    {
        _player = FindObjectOfType<Player>();

        DontDestroyOnLoad(gameObject);
    }

    private void LateUpdate()
    {
        transform.position = new Vector3(_player.Position.x, _player.Position.y + 0.5f, transform.position.z);
    }
}
