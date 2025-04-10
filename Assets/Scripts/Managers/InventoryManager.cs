/*
 * InventoryManager.cs
 * Marlow Greenan
 * 3/9/2025
 * 
 * Manages inventory and items.
 */
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InventoryManager : MonoBehaviour
{
    private static InventoryManager instance;

    [SerializeField] private GameObject _inventory;
    [SerializeField] private Transform _holdItemSpawnPoint;
    private List<GameObject> inventorySlotObjects = new List<GameObject>();
    private List<Item> inventoryItems = new List<Item>();
    private static GameObject currentHoldItem = null;
    private static int currentSelectedIndex = -1;   

    public static GameObject CurrentHoldItem { get => currentHoldItem; set => currentHoldItem = value; }
    public static InventoryManager Instance { get => instance; set => instance = value; }
    public static int CurrentSelectedIndex { get => currentSelectedIndex; set => currentSelectedIndex = value; }

    /// <summary>
    /// Adds children of _inventory to inventorySlotObjects
    /// </summary>
    private void Start()
    {
        instance = this;
        for (int index = 0; index < _inventory.transform.childCount; index++)
        {
            inventorySlotObjects.Add(_inventory.transform.GetChild(index).gameObject);
        }
    }
    /// <summary>
    /// If the player's inventory isn't full, adds an item.
    /// </summary>
    /// <param name="item"></param>
    public void AddItem(Item item)
    {
        if (CheckFull())
            GameManager.Instance.Message.NewMessage("Your inventory is full.");
        else
        {
            inventoryItems.Add(item);
            UpdateVisuals();
            GameManager.Instance.Message.NewMessage(item.Name + " was added to you inventory.");
        }
    }
    public void RemoveItem(int itemIndex)
    {
        if (itemIndex < inventorySlotObjects.Count)
        {
            inventoryItems.RemoveAt(itemIndex);
            UpdateVisuals();
        }
    }
    private void SelectItem(int itemIndex)
    {
        currentSelectedIndex = itemIndex;
    }
    /// <summary>
    /// Checks if the player's inventory is full and returns a bool based on the result.
    /// </summary>
    public bool CheckFull()
    {
        if (inventoryItems.Count < inventorySlotObjects.Count)
            return false;
        else
            return true;
    }
    /// <summary>
    /// Updates the inventory slot visuals based on items in inventory.
    /// </summary>
    public void UpdateVisuals()
    {
        foreach (GameObject inventorySlot in inventorySlotObjects)
        {
            inventorySlot.transform.GetChild(0).GetComponent<Image>().sprite = GameManager.Instance.EmptyPixel;
        }
        for (int index = 0; index < inventoryItems.Count; index++)
        {
            inventorySlotObjects[index].transform.GetChild(0).GetComponent<Image>().sprite = inventoryItems[index].IconSprite;
        }
    }
    /// <summary>
    /// Makes the player hold the item in the inventory slot selected.
    /// </summary>
    /// <param name="itemIndex"></param>
    public void HoldItem(int itemIndex = 0)
    {
        SelectItem(itemIndex);
        if (currentHoldItem != null) //Makes followers leave position & destroys object
        {
            FollowObjectAI.AllLeaveObject();
            Destroy(currentHoldItem);
        }
        if (itemIndex < inventoryItems.Count)
        {
            currentHoldItem = Instantiate(inventoryItems[itemIndex].OverworldPrefab, _holdItemSpawnPoint.position + inventoryItems[itemIndex].HoldOffset, Quaternion.identity, _holdItemSpawnPoint);
            currentHoldItem.GetComponentInChildren<Rigidbody>().isKinematic = true;
            if (currentHoldItem.GetComponentInChildren<PickUp>() != null) //Makes it so player cannot pick up held item
                Destroy(currentHoldItem.GetComponentInChildren<PickUp>());
            GameManager.Instance.Message.NewMessage("You are holding a " + inventoryItems[itemIndex].Name + ".");
        }
        else
        {
            currentHoldItem = null;
            GameManager.Instance.Message.NewMessage("You are holding nothing.");
        }
    }
}

