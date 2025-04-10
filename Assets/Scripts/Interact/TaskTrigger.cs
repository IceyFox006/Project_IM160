/* 
 * TaskTrigger.cs
 * Marlow Greenan
 * 3/23/2025
 */
using UnityEngine;

public class TaskTrigger : MonoBehaviour
{
    [SerializeField] private Task _task;
    [SerializeField] private Enums.InteractType _interactType;

    private void OnTriggerEnter(Collider collision)
    {

        if (_interactType == Enums.InteractType.Trigger && collision.gameObject == GameManager.Instance.PlayerAvatar)
            TriggerTask();
    }
    public void OnMouseDown()
    {
        if (_interactType == Enums.InteractType.Click)
            TriggerTask();
    }
    public void TriggerTask()
    {
        if (!GameManager.Instance.InUI)
        {
            bool isForCurrentQuest = false;
            foreach (Task task in LevelManager.Instance.Levels[LevelManager.Instance.CurrentLevel].SideQuest.Tasks)
            {
                if (task == _task)
                {
                    isForCurrentQuest = true;
                    break;
                }
            }
            if (isForCurrentQuest)
            {
                bool prerequisiteComplete = true;
                if (_task.Prerequisites != null)
                {
                    foreach (Task prerequisite in _task.Prerequisites)
                    {
                        if (!prerequisite.IsComplete)
                        {
                            prerequisiteComplete = false;
                            break;
                        }
                    }
                }
                if (prerequisiteComplete)
                {
                    if (LevelManager.Instance.Levels[LevelManager.Instance.CurrentLevel].SideQuest.IsActive)
                    {
                        if (!_task.IsComplete)
                        {
                            GameManager.Instance.Message.NewMessage("Quest updated.");
                            _task.IsComplete = true;
                            if (_task.Dialogue != null)
                                GameManager.Instance.DialogueManager.StartDialogue(_task.Dialogue);
                        }
                        else
                        {
                            if (_task.RepeatDialogue != null)
                                GameManager.Instance.DialogueManager.StartDialogue(_task.RepeatDialogue);
                        }
                    }
                    else if (_task.DeniedDialogue != null)
                        GameManager.Instance.DialogueManager.StartDialogue(_task.DeniedDialogue);
                }
            }
            else
            {
                if (_task.UnactiveDialogue != null)
                    GameManager.Instance.DialogueManager.StartDialogue(_task.UnactiveDialogue);
            }
        }
    }
}
