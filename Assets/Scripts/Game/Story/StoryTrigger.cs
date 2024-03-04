using UnityEngine;

public class StoryTrigger : MonoBehaviour
{
    [SerializeField] private int _stage;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        StoryTriggerManager.Trigger(_stage);
    }
}
