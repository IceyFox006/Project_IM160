using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Credits : MonoBehaviour
{
    void Start()
    {
        GetComponent<Animator>().Play("ROLL");
    }
    public void Button_TitleScreen()
    {
        SceneManager.LoadScene("TitleScreen");
    }
}
