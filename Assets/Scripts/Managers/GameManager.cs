/*
 * GameManager.cs
 * Marlow Greenan
 * 2/19/2025
 * 
 * Controls game settings with SerializeFields.
 */
using System.Collections;
using TMPro;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    private static GameManager instance;
    [SerializeField] private GameObject _playerAvatar;
    private bool inUI = false;

    [Header("Movement")]
    [SerializeField] private Vector3 _cellSize = new Vector3(25, 0, 25);
    [SerializeField] private float _moveSpeed = 1;
    [SerializeField] private float _turnSpeed = 1;

    [Header("Audio")]
    [SerializeField] private AudioManager _audioManager;

    [Header("Visuals")]
    [SerializeField] private Sprite _emptyPixel;
    [SerializeField] private Material _overOutline;
    [SerializeField] private Material _fade;

    [Header("UI")]
    [SerializeField] private Message _message;
    [SerializeField] private float _messageTime;
    [SerializeField] private DialogueManager _dialogueManager;
    [SerializeField] private HUD _HUD;
    [SerializeField] private PauseMenu _pauseMenu;
    [SerializeField] private HelpMenu _helpMenu;
    [SerializeField] private GameObject _blackScreen;
    [SerializeField] private GameObject _dialogueBlackScreen;

    [Header("Inventory")]
    [SerializeField] private InventoryManager _inventoryManager;

    [Header("Progression")]
    [SerializeField] private LevelManager _levelManager;
    [SerializeField] private Character[] _characters;

    public static GameManager Instance { get => instance; set => instance = value; }
    public GameObject PlayerAvatar { get => _playerAvatar; set => _playerAvatar = value; }
    public Vector3 CellSize { get => _cellSize; set => _cellSize = value; }
    public float MoveSpeed { get => _moveSpeed; set => _moveSpeed = value; }
    public float TurnSpeed { get => _turnSpeed; set => _turnSpeed = value; }
    public Sprite EmptyPixel { get => _emptyPixel; set => _emptyPixel = value; }
    public Message Message { get => _message; set => _message = value; }
    public float MessageTime { get => _messageTime; set => _messageTime = value; }
    public DialogueManager DialogueManager { get => _dialogueManager; set => _dialogueManager = value; }
    public HUD HUD { get => _HUD; set => _HUD = value; }
    public InventoryManager InventoryManager { get => _inventoryManager; set => _inventoryManager = value; }
    public Material OverOutline { get => _overOutline; set => _overOutline = value; }
    public PauseMenu PauseMenu { get => _pauseMenu; set => _pauseMenu = value; }
    public HelpMenu HelpMenu { get => _helpMenu; set => _helpMenu = value; }
    public GameObject BlackScreen { get => _blackScreen; set => _blackScreen = value; }
    public LevelManager LevelManager { get => _levelManager; set => _levelManager = value; }
    public bool InUI { get => inUI; set => inUI = value; }
    public AudioManager AudioManager { get => _audioManager; set => _audioManager = value; }
    public Character[] Characters { get => _characters; set => _characters = value; }
    public GameObject DialogueBlackScreen { get => _dialogueBlackScreen; set => _dialogueBlackScreen = value; }

    private void Start()
    {
        instance = this;
        _levelManager.ResetLevel(_levelManager.Levels[_levelManager.CurrentLevel]);
    }

    /// <summary>
    /// Enables the pause menu.
    /// </summary>
    public void Quit()
    {
        if (!PauseMenu.IsEnabled && !GameManager.Instance.InUI)
        {
            GameManager.Instance.PauseMenu.gameObject.SetActive(true);
            Time.timeScale = 0;
            GameManager.Instance.PauseMenu.GetComponent<Animator>().Play("ENABLE");
            GameManager.Instance.HUD.DisableHUD();
        }
    }

    /// <summary>
    /// Makes a 3D object fade out and destroy after the fadeTime.
    /// </summary>
    /// <param name="gameObject"></param>
    /// <param name="fadeTime"></param>
    /// <returns></returns>
    public IEnumerator Fade3DObject(GameObject gameObject, int fadeTime)
    {
        CoderMethods.AddMaterialToGameObject(gameObject, _fade);
        int gameObjectOpacity = 100;
        while (gameObjectOpacity > 0)
        {
            _fade.color = new Color(_fade.color.r, _fade.color.g, _fade.color.b, gameObjectOpacity);
            gameObjectOpacity--;
            yield return new WaitForSeconds(fadeTime / 100);
        }
        Destroy(gameObject);
        _fade.color = new Color(_fade.color.r, _fade.color.g, _fade.color.b, 100f);
    }
}
