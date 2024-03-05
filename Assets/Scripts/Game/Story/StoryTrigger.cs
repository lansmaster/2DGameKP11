using UnityEngine;

public class StoryTrigger : MonoBehaviour
{
    [SerializeField] private int _stage;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.CompareTag("Player"))
            StoryTriggerManager.Trigger(_stage);
    }
}
