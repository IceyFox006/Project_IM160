/* 
 * Quest.cs
 * Marlow Greenan
 * 3/23/2025
 */
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Quest", menuName = "Quest")]
[System.Serializable]
public class Quest : ScriptableObject
{
    [SerializeField] private string _name;
    [TextArea(2, 10)][SerializeField] private string _description;
    private bool isActive = false;
    [SerializeField] private List<Task> _tasks;
    [SerializeField] private Dialogue _introDialgoue;
    private bool hasPlayedIntroText = false;
    [SerializeField] private Dialogue _outroDialgoue;
    private bool hasPlayedOutroText = false;

    public List<Task> Tasks { get => _tasks; set => _tasks = value; }
    public string Name { get => _name; set => _name = value; }
    public string Description { get => _description; set => _description = value; }
    public Dialogue IntroDialgoue { get => _introDialgoue; set => _introDialgoue = value; }
    public Dialogue OutroDialgoue { get => _outroDialgoue; set => _outroDialgoue = value; }
    public bool HasPlayedOutroText { get => hasPlayedOutroText; set => hasPlayedOutroText = value; }
    public bool HasPlayedIntroText { get => hasPlayedIntroText; set => hasPlayedIntroText = value; }
    public bool IsActive { get => isActive; set => isActive = value; }
}
