using UnityEngine;

public class NPCTrigger : MonoBehaviour
{
    [SerializeField] private TextAsset _inkJSON;

    private DialogueController _dialogueController;
    private DialogueWindow _dialogueWindow;

    private void Start()
    {
        _dialogueController = FindObjectOfType<DialogueController>();
        _dialogueWindow = FindObjectOfType<DialogueWindow>();
    }

    public void OnTrigger()
    {
        if (Input.GetKeyDown(KeyCode.E))
        {
            _dialogueController.EnterDialogueMode(_inkJSON);
        }
    }
}
