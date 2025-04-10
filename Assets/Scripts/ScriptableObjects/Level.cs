/*
 * Level.cs
 * Marlow Greenan
 * 3/22/2025
 * 
 * Contains information for levels.
 */
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Level", menuName = "Level")]
[System.Serializable]
public class Level : ScriptableObject
{
    [SerializeField] private int _completetionScore = 1;
    [SerializeField] private Dialogue _introDialogue;
    [SerializeField] private Dialogue _outroDialogue;
    [SerializeField] private Quest _sideQuest;

    public Dialogue IntroDialogue { get => _introDialogue; set => _introDialogue = value; }
    public Dialogue OutroDialogue { get => _outroDialogue; set => _outroDialogue = value; }
    public int CompletetionScore { get => _completetionScore; set => _completetionScore = value; }
    public Quest SideQuest { get => _sideQuest; set => _sideQuest = value; }
}


