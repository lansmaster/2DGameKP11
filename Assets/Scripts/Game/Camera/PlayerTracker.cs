using UnityEngine;

public class PlayerTracker : MonoBehaviour
{
    private void LateUpdate()
    {
        transform.position = new Vector3(Player.Instance.Position.x, Player.Instance.Position.y + 0.5f, transform.position.z);
    }
}
