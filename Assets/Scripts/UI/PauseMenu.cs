/*
 * PauseMenu.cs
 * Marlow Greenan
 * 3/19/2025
 * 
 * Buttons for the Pause Menu
 */
using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseMenu : MonoBehaviour
{
    private static bool isEnabled = false;

    public static bool IsEnabled { get => isEnabled; set => isEnabled = value; }

    public void ResumeGame()
    {
        Time.timeScale = 1;
        isEnabled = false;
        GetComponent<Animator>().Play("DISABLE");
        GameManager.Instance.HUD.EnableHUD();
        StartCoroutine(AfterDisable());
    }
    IEnumerator AfterDisable()
    {
        yield return new WaitForSeconds(GetComponent<Animator>().GetCurrentAnimatorClipInfo(0).Length);
        gameObject.SetActive(false);
    }
    public void OpenHelpMenu()
    {
        GameManager.Instance.HelpMenu.gameObject.SetActive(true);
    }
    public void QuitGame()
    {
        Time.timeScale = 1;
        SceneManager.LoadScene("TitleScreen");
    }
}
