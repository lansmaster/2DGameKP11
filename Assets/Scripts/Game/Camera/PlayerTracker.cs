using UnityEngine;

public class PlayerTracker : MonoBehaviour
{
    private Player _player;

    private void Start()
    {
        _player = Player.instance;
    }

    private void LateUpdate()
    {
        transform.position = new Vector3(_player.position.x, _player.position.y + 0.5f, transform.position.z);
    }
}
