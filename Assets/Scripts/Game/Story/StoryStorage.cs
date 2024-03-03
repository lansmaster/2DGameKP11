using System.Collections.Generic;
using UnityEngine;

public class StoryStorage : MonoBehaviour
{
    [SerializeField] private DialogueController _dialogueController;

    [Header("1 Стадия сюжета")]
    [SerializeField] private TextAsset _1InkJSON;

    private Dictionary<int, TextAsset> _storyStages;

    private void Start()
    {
        _storyStages = new Dictionary<int, TextAsset>()
        {
            { 1, _1InkJSON }
        };
    }

    private void OnEnable()
    {
        
    }

    private void OnDisable()
    {
        
    }

    private void PlayStory()
    {

    }
}
