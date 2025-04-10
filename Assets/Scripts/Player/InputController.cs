using System.Collections;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

public class InputController : MonoBehaviour
{
    [SerializeField] private PlayerInput _playerInput;

    private void Start()
    {
        _playerInput.currentActionMap.Enable();
    }
    
    public void OnRESET(InputValue inputValue)
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        LevelManager.Instance.ResetLevel(LevelManager.Instance.Levels[LevelManager.Instance.CurrentLevel]);
    }
    public void OnQUIT(InputValue inputValue)
    {
        GameManager.Instance.Quit();
    }
}
