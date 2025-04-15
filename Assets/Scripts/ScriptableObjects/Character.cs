/*
 * Character.cs
 * Marlow Greenan
 * 3/4/2025
 * 
 * Contains information for a character.
 */
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Creates a character scriptable objects. Contains name, relationship score, and expressions.
/// </summary>
[CreateAssetMenu(fileName = "Character", menuName = "Character")]
[System.Serializable]
public class Character : ScriptableObject
{
    [SerializeField] private string _name;
    [SerializeField] private int _relationshipScore = 0;
    [SerializeField] private List<CharacterExpression> _characterExpressions;

    public int RelationshipScore { get => _relationshipScore; set => _relationshipScore = value; }
    public string Name { get => _name; set => _name = value; }
    public List<CharacterExpression> CharacterExpressions { get => _characterExpressions; set => _characterExpressions = value; }
}

/// <summary>
/// Contains the relationship change value and the character's whose relationship is being effected.
/// </summary>
[System.Serializable]
public class CharacterRelation
{
    [SerializeField] private Character _character;
    [SerializeField] private int _relationshipChangeValue;

    public Character Character { get => _character; set => _character = value; }
    public int RelationshipChangeValue { get => _relationshipChangeValue; set => _relationshipChangeValue = value; }
}

/// <summary>
/// Contains Enum data and sprite for a character expression.
/// </summary>
[System.Serializable]
public class CharacterExpression
{
    [SerializeField] private Enums.CharacterExpression _expression = Enums.CharacterExpression.Default;
    [SerializeField] private Sprite _sprite;
    [SerializeField] private Vector2 _size = new Vector2(500, 500);

    public Enums.CharacterExpression Expression { get => _expression; set => _expression = value; }
    public Sprite Sprite { get => _sprite; set => _sprite = value; }
    public Vector2 Size { get => _size; set => _size = value; }
}

/// <summary>
/// Contains character info, the inequality being used to check the character's score, and the score requirement. 
/// </summary>
[System.Serializable]
public class CharacterRequirement
{
    [SerializeField] private Character _character;
    [SerializeField] private Enums.Inequalities _inequalityCheck;
    [SerializeField] private int _score;

    public Character Character { get => _character; set => _character = value; }
    public int Score { get => _score; set => _score = value; }
    public Enums.Inequalities InequalityCheck { get => _inequalityCheck; set => _inequalityCheck = value; }

    /// <summary>
    /// Checks if the character's points meets the requirement and returns true if they do.
    /// </summary>
    /// <returns></returns>
    public bool MeetsRequirement()
    {
        switch (_inequalityCheck)
        {
            case Enums.Inequalities.None: return true;
            case Enums.Inequalities.GreaterThan:
                if (_character.RelationshipScore > _score) return true; break;
            case Enums.Inequalities.LessThan:
                if (_character.RelationshipScore < _score) return true; break;
            case Enums.Inequalities.Equal:
                if (_character.RelationshipScore == _score) return true; break;
        }
        return false;
    }
}
