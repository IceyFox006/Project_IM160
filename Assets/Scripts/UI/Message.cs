using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Message : MonoBehaviour
{
    [SerializeField] private GameManager _gameManager;
    [SerializeField] private TMP_Text _messageText;
    public TMP_Text MessageText { get => _messageText; set => _messageText = value; }

    public void NewMessage(string message)
    {
        _messageText.text = message;
        gameObject.GetComponent<Animator>().Play("ENABLE");
        StartCoroutine(MessageInterval());
    }
    IEnumerator MessageInterval()
    {
        yield return new WaitForSeconds(_gameManager.MessageTime);
        gameObject.GetComponent<Animator>().Play("DISABLE");
    }
}
