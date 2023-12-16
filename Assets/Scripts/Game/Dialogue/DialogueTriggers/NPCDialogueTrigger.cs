using UnityEngine;

public class NPCDialogueTrigger : MonoBehaviour
{
    [SerializeField] private TextAsset _inkJSON;

    private DialogueController _dialogueController;
    private DialogueWindow _dialogueWindow;

    private Animator _animator;
    private void Start()
    {
        _animator = GetComponent<Animator>();

        _dialogueController = FindObjectOfType<DialogueController>();
        _dialogueWindow = FindObjectOfType<DialogueWindow>();
    }

    private void FixedUpdate()
    {
        _animator.SetBool("Emission", false);
    }

    public void TriggerAction()
    {
        _animator.SetBool("Emission", true);

        if (Input.GetKeyDown(KeyCode.E) && !_dialogueWindow.IsPlaying)
        {
            _dialogueController.EnterDialogueMode(_inkJSON);
        }
    }
}
