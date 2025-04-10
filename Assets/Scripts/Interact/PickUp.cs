/*
 * PickUp.cs
 * Marlow Greenan
 * 2/19/2025
 * 
 * Placed on a gameObject, allowing a player to put it in their hand.
 */
using UnityEngine;

public class PickUp : MonoBehaviour
{
    [SerializeField] private Item _item;

    public void OnMouseDown()
    {
        if (_item != null)
        {
            GameManager.Instance.InventoryManager.AddItem(_item);
            AudioManager.Instance.PlayPickUpAudio();
            Destroy(gameObject);
        }
    }
}
