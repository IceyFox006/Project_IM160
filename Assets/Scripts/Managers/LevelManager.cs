/* 
 * LevelManager.cs
 * Marlow Greenan
 * 3/23/2025
 */
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelManager : MonoBehaviour
{
    private static LevelManager instance;

    [SerializeField] private float _loadTime;
    [SerializeField] private List<Level> _levels = new List<Level>();
    [SerializeField] private List<GameObject> _levelBoundaries = new List<GameObject>();
    [SerializeField] private int _currentLevel = 0;
    private bool completedLevel = false;

    private int _progressionScore = 0;

    public int ProgressionScore { get => _progressionScore; set => _progressionScore = value; }
    public List<Level> Levels { get => _levels; set => _levels = value; }
    public int CurrentLevel { get => _currentLevel; set => _currentLevel = value; }
    public static LevelManager Instance { get => instance; set => instance = value; }
    public bool CompletedLevel { get => completedLevel; set => completedLevel = value; }


    private void Start()
    {
        instance = this;
        if (_currentLevel > -1 && _currentLevel < _levels.Count)
            StartCoroutine(LoadLevel());
    }

    /// <summary>
    /// Enables/disables proper level boundaries and starts intro dialogue.
    /// </summary>
    /// <returns></returns>
    public IEnumerator LoadLevel(float loadTime = 0)
    {
        yield return new WaitForSeconds(loadTime);
        completedLevel = false;
        GameManager.Instance.PlayerAvatar.transform.position = Vector3.zero;
        for (int index = 0; index < _levels.Count; index++)
        {
            if (index <= CurrentLevel)
                _levelBoundaries[index].SetActive(false);
            else
                _levelBoundaries[index].SetActive(true);
        }
        EnableOnRequirement.SetActiveOnRequirement();

        GameManager.Instance.BlackScreen.GetComponent<Animator>().Play("DISABLE");
        GameManager.Instance.DialogueBlackScreen.GetComponent<Animator>().Play("DISABLE");

        GameManager.Instance.DialogueManager.StartDialogue(_levels[_currentLevel].IntroDialogue);
    }
    public void InstantLoadLevel()
    {
        completedLevel = false;
        GameManager.Instance.PlayerAvatar.transform.position = Vector3.zero;
        for (int index = 0; index < _levels.Count; index++)
        {
            if (index <= CurrentLevel)
                _levelBoundaries[index].SetActive(false);
            else
                _levelBoundaries[index].SetActive(true);
        }
        EnableOnRequirement.SetActiveOnRequirement();

        GameManager.Instance.BlackScreen.GetComponent<Animator>().Play("DISABLE");
        GameManager.Instance.DialogueBlackScreen.GetComponent<Animator>().Play("DISABLE");

        GameManager.Instance.DialogueManager.StartDialogue(_levels[_currentLevel].IntroDialogue);
    }

    /// <summary>
    /// Updates level progress.
    /// </summary>
    /// <param name="progressionValue"></param>
    public void UpdateProgression(int progressionValue = 1)
    {
        _progressionScore += progressionValue;
        if (_progressionScore >= _levels[_currentLevel].CompletetionScore)
            CompleteLevel();
    }

    /// <summary>
    /// Plays outro dialogue and progresses to next level.
    /// </summary>
    public void CompleteLevel()
    {
        completedLevel= true;
        GameManager.Instance.DialogueManager.StartDialogue(_levels[_currentLevel].OutroDialogue);
    }

    /// <summary>
    /// Checks quest progression and plays intro and outro dialogue accordingly.
    /// </summary>
    /// <param name="quest"></param>
    public void CheckQuestProgress(Quest quest)
    {
        int completedTasks = 0;
        foreach (Task task in quest.Tasks)
        {
            if (task != null && task.IsComplete)
                completedTasks++;
        }
        if (completedTasks <= 0 && !quest.HasPlayedIntroText)
        {
            GameManager.Instance.DialogueManager.StartDialogue(quest.IntroDialgoue);
            quest.HasPlayedIntroText = true;
        }
        if (completedTasks >= quest.Tasks.Count && !quest.HasPlayedOutroText)
        {
            GameManager.Instance.DialogueManager.StartDialogue(quest.OutroDialgoue);
            quest.HasPlayedOutroText = true;
        }
    }

    /// <summary>
    /// Resets level data.
    /// </summary>
    public void ResetLevel(Level level)
    {
        ResetQuest(level.SideQuest);
        ResetCharacters(GameManager.Instance.Characters);
    }

    /// <summary>
    /// Reset quest data.
    /// </summary>
    public void ResetQuest(Quest quest)
    {
        quest.IsActive = false;
        quest.HasPlayedIntroText = false;
        quest.HasPlayedOutroText = false;
        for (int index = 0; index < quest.Tasks.Count; index++)
        {
            if (quest.Tasks[index] != null)
                ResetTask(quest.Tasks[index]);
        }
    }

    /// <summary>
    /// Reset task data.
    /// </summary>
    public void ResetTask(Task task)
    {
        task.IsComplete = false;
    }

    public void ResetCharacters(Character[] characters)
    {
        foreach (Character character in characters)
        {
            character.RelationshipScore = 0;
        }
    }
}
