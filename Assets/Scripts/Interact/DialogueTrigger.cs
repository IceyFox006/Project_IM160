/*
 * DialogueTrigger.cs
 * Marlow Greenan
 * 3/4/2025
 * 
 * Different methods for initiating a dialogue.
 */
using UnityEngine;

public class DialogueTrigger : MonoBehaviour
{
    [SerializeField] private Dialogue _dialogue;

    public Dialogue Dialogue { get => _dialogue; set => _dialogue = value; }

    /// <summary>
    /// Starts a dialogue.
    /// </summary>
    public void TriggerDialogue()
    {
        FindObjectOfType<DialogueManager>().StartDialogue(_dialogue);
    }
}
