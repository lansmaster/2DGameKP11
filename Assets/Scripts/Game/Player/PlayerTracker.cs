using UnityEngine;

public class PlayerTracker : MonoBehaviour
{
    [SerializeField] private PlayerMover _player;

    private Transform _transform;

    private void Start()
    {
        _transform = GetComponent<Transform>();
    }

    private void LateUpdate()
    {
        _transform.position = new Vector3(_player.transform.position.x, _player.transform.position.y, _transform.position.z);
    }
}
