/*
 * Feed.cs
 * Marlow Greenan
 * 3/19/2025
 * 
 * Allows the player to feed the gameObject a certain tag object when held.
 */
using UnityEngine;
[RequireComponent(typeof(Collider))]
[RequireComponent(typeof(Rigidbody))]
public class Feed : MonoBehaviour
{
    [SerializeField] private string _foodObjectTag;
    [SerializeField] private bool _requireFollowing = true;

    public void OnMouseDown()
    {
        if (InventoryManager.CurrentHoldItem != null && InventoryManager.CurrentHoldItem.CompareTag(_foodObjectTag))
        {
            if (!_requireFollowing || (_requireFollowing && transform.parent.GetComponent<FollowObjectAI>() != null
                && transform.parent.GetComponent<FollowObjectAI>().IsFollowing))
            {
                Destroy(InventoryManager.CurrentHoldItem);
                FollowObjectAI.AllLeaveObject();
                InventoryManager.Instance.RemoveItem(InventoryManager.CurrentSelectedIndex);
                GameManager.Instance.Message.NewMessage("You fed the squirrel.");
                GameManager.Instance.LevelManager.UpdateProgression();
                StartCoroutine(GameManager.Instance.Fade3DObject(gameObject, 5));
            }
            else
                GameManager.Instance.Message.NewMessage("It is not interested.");
        }
        else
            GameManager.Instance.Message.NewMessage("You are not holding the right item.");
    }
}
