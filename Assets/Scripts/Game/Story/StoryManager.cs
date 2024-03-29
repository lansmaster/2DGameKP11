using System.Collections.Generic;
using System.Data;
using UnityEngine;

public class StoryManager : MonoBehaviour
{
    [SerializeField] private DialogueController _dialogueController;
    
    [Header("������ ������")]
    [SerializeField] private TextAsset _1InkJSON;
    [SerializeField] private TextAsset _2InkJSON;
    [SerializeField] private TextAsset _3InkJSON;
    [SerializeField] private TextAsset _4InkJSON;
    [SerializeField] private TextAsset _5InkJSON;

    private Dictionary<int, TextAsset> _storyStages;
    public int CurrentStage { get; private set; }

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
        StoryTrigger.StoryTriggered += PlayStory;
    }

    private void OnDisable()
    {
        StoryTrigger.StoryTriggered -= PlayStory;
    }

    private void OnDestroy()
    {
        SaveStage();
    }

    private void PlayStory(int stage)
    {
        if(CurrentStage + 1 != stage)
        {
            return;
        }
        CurrentStage = stage;

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

            CurrentStage = stage;
        }
        else
        {
            CurrentStage = 0;

            DataBase.ExecuteQueryWithoutAnswer($"INSERT INTO Story (Stage) VALUES ({CurrentStage})");
        }
    }
    
    private void SaveStage()
    {
        DataBase.ExecuteQueryWithoutAnswer(string.Format("UPDATE Story SET Stage = {0} WHERE id = 1", CurrentStage));
    }
}
