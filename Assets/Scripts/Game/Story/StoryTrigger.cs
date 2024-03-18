using System;
using UnityEngine;

public class StoryTrigger : MonoBehaviour
{
    [SerializeField] private int _stage;

    public static event Action<int> StoryTriggered;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.CompareTag("Player"))
            StoryTriggered?.Invoke(_stage);
    }
}
