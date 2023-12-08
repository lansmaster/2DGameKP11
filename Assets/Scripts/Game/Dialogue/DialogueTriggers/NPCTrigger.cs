using UnityEngine;

public class NPCTrigger : MonoBehaviour
{
    [SerializeField] private TextAsset _inkJSON;

    private DialogueController _dialogueController;

    private void Start()
    {
        _dialogueController = FindObjectOfType<DialogueController>();
    }

    public void TriggerAction()
    {
        _dialogueController.EnterDialogueMode(_inkJSON);
    }
}
