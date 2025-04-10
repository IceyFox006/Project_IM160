/*
 * HelpMenu.cs
 * Marlow Greenan
 * 3/21/2025
 * 
 * Buttons and functions for the HelpMenu
 */
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class HelpMenu : MonoBehaviour
{
    [Header("Main")]
    [SerializeField] private Transform _mainIconsParent;
    [SerializeField] private GameObject _mainQuestIconPrefab;
    [SerializeField] private Sprite _fullSquirrelSprite;

    [Header("Side")]
    [SerializeField] private GameObject _sideQuestUI;
    [SerializeField] private Transform _sideQuestTasksTextParent;
    [SerializeField] private TMP_Text _sideQuestNameText;
    [SerializeField] private TMP_Text _sideQuestDescriptionText;
    [SerializeField] private GameObject _questTaskTextPrefab;
    [SerializeField] private TMP_Text _questProgressText;
    [SerializeField] private Image _questProgressBar;
    private void OnEnable()
    {
        UpdateProgressIcons();
        UpdateSideQuest(GameManager.Instance.LevelManager.Levels[GameManager.Instance.LevelManager.CurrentLevel].SideQuest);
    }
    /// <summary>
    /// Returns to pause menu.
    /// </summary>
    public void Back()
    {
        GameManager.Instance.PauseMenu.GetComponent<Animator>().Play("ENABLE");
        gameObject.SetActive(false);
    }
    /// <summary>
    /// Updates side quest information in HelpMenu.
    /// </summary>
    /// <param name="quest"></param>
    public void UpdateSideQuest(Quest quest)
    {
        if (quest.IsActive)
        {
            _sideQuestUI.SetActive(true);
            _sideQuestNameText.text = quest.Name;
            _sideQuestDescriptionText.text = quest.Description;
            CoderMethods.DestroyChildren(_sideQuestTasksTextParent);
            float tasksCompleted = 0;
            for (int index = 0; index < quest.Tasks.Count; index++)
            {
                GameObject taskText = Instantiate(_questTaskTextPrefab, _sideQuestTasksTextParent);
                taskText.GetComponent<TMP_Text>().text = quest.Tasks[index].Description;
                if (quest.Tasks[index].IsComplete)
                    tasksCompleted++;
            }
            UpdateProgressBar(tasksCompleted / quest.Tasks.Count);
        }
        else
            _sideQuestUI.SetActive(false);
    }
    /// <summary>
    /// Updates progress icons to match quest progress.
    /// </summary>
    public void UpdateProgressIcons()
    {
        CoderMethods.DestroyChildren(_mainIconsParent);
        for (int index = 0; index < GameManager.Instance.LevelManager.Levels[GameManager.Instance.LevelManager.CurrentLevel].CompletetionScore; index++)
        {
            GameObject squirrelIcon = Instantiate(_mainQuestIconPrefab, _mainIconsParent);
            if (index < GameManager.Instance.LevelManager.ProgressionScore)
                squirrelIcon.GetComponent<Image>().sprite = _fullSquirrelSprite;
        }
    }
    public void UpdateProgressBar(float normalizedProgress)
    {
        _questProgressBar.fillAmount = normalizedProgress;
        _questProgressText.text = Mathf.RoundToInt(normalizedProgress * 100) + "%";
    }
}
