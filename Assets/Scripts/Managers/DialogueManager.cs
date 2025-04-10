/*
 * DialogueManager.cs
 * Marlow Greenan
 * 3/4/2025
 * 
 * Manages information within a dialogue.
 */
using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class DialogueManager : MonoBehaviour
{
    [SerializeField] private GameManager _gameManager;
    [SerializeField] private GameObject _dialogueUI;
    [SerializeField] private Queue<DialogueLine> _dialogueLines;
    [SerializeField] private Image _speakerSprite;
    [SerializeField] private TMP_Text _speakerNameText;
    [SerializeField] private TMP_Text _dialogueText;
    [SerializeField] private GameObject _responses;
    [SerializeField] private GameObject _responseButtonPrefab;
    [SerializeField] private Button _continueButton;
    [SerializeField] private Animator _relationshipPointAnimator;
    [Range(0, 1)] [SerializeField] private float _typeSpeed = 0.1f;
    [SerializeField] private Character _noSpeaker;

    private DialogueLine currentLine;

    public GameObject DialogueUI { get => _dialogueUI; set => _dialogueUI = value; }

    private void Start()
    {
        _dialogueLines = new Queue<DialogueLine>();
        _continueButton.gameObject.SetActive(false);
        _responses.SetActive(false);
    }

    /// <summary>
    /// Begins a dialogue.
    /// </summary>
    /// <param name="dialogue"></param>
    public void StartDialogue(Dialogue dialogue)
    {
        if (dialogue.ActiveQuest != null) //Activates quest
        {
            dialogue.ActiveQuest.IsActive = true;
            GameManager.Instance.Message.NewMessage("New quest added.");
        }

        GameManager.Instance.InUI = true;
        _gameManager.HUD.DisableHUD();
        _dialogueUI.GetComponent<Animator>().Play("ENABLE");
        _speakerNameText.text = dialogue.DialogueLines[0].SpeakerCharacter.Name;
        _dialogueLines.Clear();

        //for (int index = 0; index < dialogue.DialogueLines.Length; index++)
        //{
        //    _dialogueLines.Enqueue(dialogue.DialogueLines[index]);
        //}
        AddDialogue(dialogue);
        DisplayNextLine();
    }

    /// <summary>
    /// Displays the next line of text.
    /// </summary>
    public void DisplayNextLine()
    {
        //Resets relationship point animations and deactives continue button and responses.
        _relationshipPointAnimator.Play("IDLE");
        _continueButton.gameObject.SetActive(false);
        _responses.SetActive(false);

        //Check if there are more lines.
        if (_dialogueLines.Count <= 0)
        {
            EndDialogue();
            return;
        }

        currentLine = _dialogueLines.Dequeue();
        CheckDialogueRequirement();

        _speakerNameText.transform.parent.gameObject.SetActive(true);
        _speakerNameText.text = currentLine.SpeakerCharacter.Name;

        //Sets speaker sprite to match character expression.
        Sprite speakerExpressionSprite = currentLine.SpeakerCharacter.CharacterExpressions[0].Sprite;
        if (currentLine.SpeakerCharacter != _noSpeaker)
        {
            foreach (CharacterExpression characterExpression in currentLine.SpeakerCharacter.CharacterExpressions)
            {
                if (characterExpression.Expression == currentLine.CharacterExpression)
                {
                    speakerExpressionSprite = characterExpression.Sprite;
                    break;
                }
            }
        }
        else
            _speakerNameText.transform.parent.gameObject.SetActive(false);
        _speakerSprite.sprite = speakerExpressionSprite;

        //Check character relations and play animation if there is a change.
        if (currentLine.CharacterRelations != null && currentLine.CharacterRelations.Count > 0)
        {
            for (int index = 0; index < currentLine.CharacterRelations.Count; index++)
            {
                currentLine.CharacterRelations[index].Character.RelationshipScore
                    += currentLine.CharacterRelations[index].RelationshipChangeValue;
                
                if (currentLine.CharacterRelations[index].Character == currentLine.SpeakerCharacter)
                {
                    if (currentLine.CharacterRelations[index].RelationshipChangeValue > 0)
                        _relationshipPointAnimator.Play("GAIN");
                    else if (currentLine.CharacterRelations[index].RelationshipChangeValue < 0)
                        _relationshipPointAnimator.Play("LOSS");
                }
            }
        }
        StopAllCoroutines();
        StartCoroutine(TypeText(currentLine.Text));
    }

    /// <summary>
    /// Types text out over a period of time.
    /// </summary>
    /// <param name="text"></param>
    /// <returns></returns>
    IEnumerator TypeText(string text)
    {
        _dialogueText.text = "";
        foreach (char letter in  text.ToCharArray())
        {
            _dialogueText.text += letter;
            yield return new WaitForSeconds(_typeSpeed);
        }
        AtEndOfTypeText();
    }
    /// <summary>
    /// Allows player to continue dialogue or respond.
    /// </summary>
    private void AtEndOfTypeText()
    {
        if (currentLine.Responses.Length <= 0) //If player can't respond
            _continueButton.gameObject.SetActive(true);
        else
        {
            foreach (Transform child in _responses.transform) //Destroys all old responses.
            {
                GameObject.Destroy(child.gameObject);
            }
            _responses.SetActive(true);
            for (int index = currentLine.Responses.Length - 1; index >= 0; index--)
            {
                GenerateResponse(currentLine.Responses[index]);
            }
        }
    }

    /// <summary>
    /// Generates new responses.
    /// </summary>
    /// <param name="response"></param>
    private void GenerateResponse(DialogueResponse response)
    {
        GameObject responseButton = Instantiate(_responseButtonPrefab, _responses.transform);
        responseButton.transform.GetChild(0).GetComponent<TMP_Text>().text = response.Text;
        responseButton.GetComponent<DialogueTrigger>().Dialogue = response.DialogueBranch;
    }

    private void CheckDialogueRequirement()
    {
        //Check if currentLine meets the requirement, if it doesn't skip to next line.
        if (currentLine.Requirement != null && currentLine.Requirement.MeetsRequirement() == false)
        {
            currentLine = _dialogueLines.Dequeue();
            CheckDialogueRequirement();
            return;
        }
    }

    /// <summary>
    /// Ends a dialogue and hides the UI.
    /// </summary>
    private void EndDialogue()
    {
        GameManager.Instance.LevelManager.CheckQuestProgress
            (GameManager.Instance.LevelManager.Levels[GameManager.Instance.LevelManager.CurrentLevel].SideQuest);
        
        if (_dialogueLines.Count > 0)
                return;

        GameManager.Instance.InUI = false;
        _dialogueUI.GetComponent<Animator>().Play("DISABLE");
        _gameManager.HUD.EnableHUD();
        if (LevelManager.Instance.CompletedLevel)
        {
            GameManager.Instance.BlackScreen.GetComponent<Animator>().Play("ENABLE");
            LevelManager.Instance.CurrentLevel++;
            StartCoroutine(LevelManager.Instance.LoadLevel(1));
        }
    }

    /// <summary>
    /// Adds a new dialogue to the end of the current dialogue.
    /// </summary>
    /// <param name="dialogue"></param>
    public void AddDialogue(Dialogue dialogue)
    {
        for (int index = 0; index < dialogue.DialogueLines.Length; index++)
        {
            _dialogueLines.Enqueue(dialogue.DialogueLines[index]);
        }
    }
}
