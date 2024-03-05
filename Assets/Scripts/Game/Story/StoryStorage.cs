using System.Collections.Generic;
using System.Data;
using System.Globalization;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UIElements;

public class StoryStorage : MonoBehaviour
{
    [SerializeField] private DialogueController _dialogueController;
    
    [Header("Стадии сюжета")]
    [SerializeField] private TextAsset _1InkJSON;
    [SerializeField] private TextAsset _2InkJSON;
    [SerializeField] private TextAsset _3InkJSON;
    [SerializeField] private TextAsset _4InkJSON;
    [SerializeField] private TextAsset _5InkJSON;

    private Dictionary<int, TextAsset> _storyStages;
    private int _currentStage;

    private void Start()
    {
        _storyStages = new Dictionary<int, TextAsset>()
        {
            { 1, _1InkJSON },
            { 2, _2InkJSON },
            { 3, _3InkJSON },
            { 4, _4InkJSON },
            { 5, _5InkJSON }
        };

        LoadStage();
    }

    private void OnEnable()
    {
        StoryTriggerManager.StoryTriggered += PlayStory;
    }

    private void OnDisable()
    {
        StoryTriggerManager.StoryTriggered -= PlayStory;
    }

    private void OnDestroy()
    {
        SaveStage();
    }

    private void PlayStory(int stage)
    {
        if(_currentStage + 1 != stage)
        {
            return;
        }
        _currentStage = stage;

        TextAsset textAsset;
        if (_storyStages.TryGetValue(stage, out textAsset))
        {
            _dialogueController.EnterDialogueMode(textAsset);
        }
    }

    private void LoadStage()
    {
        if (DataBase.ExecuteQueryWithAnswer("SELECT EXISTS(SELECT * FROM Story)") != "0")
        {
            DataTable PlayerPosition = DataBase.GetTable("SELECT * FROM Story WHERE id = 1");

            int stage = int.Parse(PlayerPosition.Rows[0][1].ToString());

            _currentStage = stage;
        }
        else
        {
            _currentStage = 0;

            DataBase.ExecuteQueryWithoutAnswer($"INSERT INTO Story (Stage) VALUES ({_currentStage})");
        }
    }
    
    private void SaveStage()
    {
        DataBase.ExecuteQueryWithoutAnswer(string.Format("UPDATE Story SET Stage = {0} WHERE id = 1", _currentStage));
    }
}
