using UnityEngine;

public class PlayerTracker : MonoBehaviour
{
    private Transform _transform;

    private Player _player;

    private void Start()
    {
        _transform = GetComponent<Transform>();
    }

    private void LateUpdate()
    {
        _transform.position = new Vector3(_player.Position.x, _player.Position.y + 0.5f, _transform.position.z);
    }
}
