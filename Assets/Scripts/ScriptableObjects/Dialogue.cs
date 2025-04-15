/*
 * Dialogue.cs
 * Marlow Greenan
 * 3/4/2025
 * 
 * Contains information for a dialogue.
 */
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Contains the dialogue lines that will be played and the quest that will become active when the dialogue is played.
/// </summary>
[CreateAssetMenu(fileName = "Dialogue", menuName = "Dialogue")]
[System.Serializable]
public class Dialogue : ScriptableObject
{
    [SerializeField] private DialogueLine[] _dialogueLines;
    [SerializeField] private Quest _activeQuest;
    public DialogueLine[] DialogueLines { get => _dialogueLines; set => _dialogueLines = value; }
    public Quest ActiveQuest { get => _activeQuest; set => _activeQuest = value; }
}

/// <summary>
/// Contains the requirement for the dialogue line to play, the charcter speaking, the expression of the speaker, the
/// text played, the changes to character relationship points, and the responses for the player.
/// </summary>
[System.Serializable]
public class DialogueLine
{
    [SerializeField] private CharacterRequirement _requirement;
    [SerializeField] private Enums.EffectFlag _effectFlag = Enums.EffectFlag.None;
    [SerializeField] private Character _speakerCharacter;
    [SerializeField] private Enums.CharacterExpression _characterExpression;
    [TextArea(3, 10)] [SerializeField] private string _text;
    [SerializeField] private List<CharacterRelation> _characterRelations;
    [SerializeField] private DialogueResponse[] _responses;
    public string Text { get => _text; set => _text = value; }
    public DialogueResponse[] Responses { get => _responses; set => _responses = value; }
    public List<CharacterRelation> CharacterRelations { get => _characterRelations; set => _characterRelations = value; }
    public Character SpeakerCharacter { get => _speakerCharacter; set => _speakerCharacter = value; }
    public Enums.CharacterExpression CharacterExpression { get => _characterExpression; set => _characterExpression = value; }
    public CharacterRequirement Requirement { get => _requirement; set => _requirement = value; }
    public Enums.EffectFlag EffectFlag { get => _effectFlag; set => _effectFlag = value; }
}

/// <summary>
/// Contains the text for the player's response and the dialogue that will play based on the characer's response.
/// </summary>
[System.Serializable]
public class DialogueResponse
{
    [SerializeField] private string _text;
    [SerializeField] private Dialogue _dialogueBranch;

    public string Text { get => _text; set => _text = value; }
    public Dialogue DialogueBranch { get => _dialogueBranch; set => _dialogueBranch = value; }
}

