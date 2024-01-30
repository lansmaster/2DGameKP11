using UnityEngine;

public class PlayerTracker : MonoBehaviour
{
    private void LateUpdate()
    {
        transform.position = new Vector3(Player.instance.position.x, Player.instance.position.y + 0.5f, transform.position.z);
    }
}
