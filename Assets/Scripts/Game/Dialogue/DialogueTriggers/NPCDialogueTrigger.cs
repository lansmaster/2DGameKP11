using UnityEngine;

public class NPCDialogueTrigger : MonoBehaviour
{
    [SerializeField] private TextAsset _inkJSON;

    private DialogueController _dialogueController;
    private DialogueWindow _dialogueWindow;

    private void Start()
    {
        _dialogueController = FindObjectOfType<DialogueController>();
        _dialogueWindow = FindObjectOfType<DialogueWindow>();
    }

    public void TriggerAction()
    {
        if (Input.GetKeyDown(KeyCode.E) && !_dialogueWindow.IsPlaying)
        {
            _dialogueController.EnterDialogueMode(_inkJSON);
        }
    }
}
