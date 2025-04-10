/* 
 * Task.cs
 * Marlow Greenan
 * 3/26/2025
 */
using UnityEngine;

[CreateAssetMenu(fileName = "Task", menuName = "Task")]
[System.Serializable]
public class Task : ScriptableObject
{
    [SerializeField] private string _description;
    [SerializeField] private Task[] _prerequisites;
    private bool isComplete = false;
    [SerializeField] private Dialogue _dialogue;
    [SerializeField] private Dialogue _repeatDialogue;
    [SerializeField] private Dialogue _deniedDialogue;
    [SerializeField] private Dialogue _unactiveDialogue;

    public string Description { get => _description; set => _description = value; }
    public bool IsComplete { get => isComplete; set => isComplete = value; }
    public Dialogue Dialogue { get => _dialogue; set => _dialogue = value; }
    public Dialogue RepeatDialogue { get => _repeatDialogue; set => _repeatDialogue = value; }
    public Dialogue UnactiveDialogue { get => _unactiveDialogue; set => _unactiveDialogue = value; }
    public Task[] Prerequisites { get => _prerequisites; set => _prerequisites = value; }
    public Dialogue DeniedDialogue { get => _deniedDialogue; set => _deniedDialogue = value; }
}
