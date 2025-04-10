/*
 * FollowObjectAI.cs
 * Marlow Greenan
 * 3/10/2025
 * 
 * Controls game settings with SerializeFields.
 */
using UnityEngine;
using UnityEngine.AI;

[RequireComponent(typeof(Rigidbody))]
[RequireComponent(typeof(NavMeshAgent))]
public class FollowObjectAI : MonoBehaviour
{
    private GameManager gameManager;
    [SerializeField] private NavMeshAgent _navMeshAgent;
    [SerializeField] private string _followObjectTag;
    [SerializeField] private bool _followWhenHolding = true;
    [SerializeField] private Vector3 _followerOffset;
    private GameObject currentFollowObject = null;
    [Range(0.01f, 2)] [SerializeField] private float _followSpeedMultiplier = 1;
    private bool isFollowing = false;

    public bool FollowWhenHolding { get => _followWhenHolding; set => _followWhenHolding = value; }
    public bool IsFollowing { get => isFollowing; set => isFollowing = value; }

    private void Start()
    {
        gameManager = GameObject.FindObjectOfType<GameManager>();
    }
    public void OnTriggerEnter(Collider collision)
    {
        if (collision.gameObject.CompareTag(_followObjectTag))
        {
            if (!_followWhenHolding || (_followWhenHolding && InventoryManager.CurrentHoldItem != null ))
                FollowObject(collision.gameObject);
        }
    }
    public void OnTriggerExit(Collider collision)
    {
        if (collision.gameObject.CompareTag(_followObjectTag))
            LeaveObject();
    }
    public void FollowObject(GameObject followObject)
    {
        isFollowing = true;
        currentFollowObject = followObject.gameObject;
        if (gameObject.GetComponent<WanderAI>() != null)
            gameObject.GetComponent<WanderAI>().IsWandering = false;
        _navMeshAgent.SetDestination(new Vector3(currentFollowObject.transform.position.x,
            currentFollowObject.transform.position.y, 1) + (_followerOffset)); //
    }
    public static void AllLeaveObject()
    {
        foreach (FollowObjectAI followerObject in GameObject.FindObjectsOfType<FollowObjectAI>())
        {
            if (followerObject.FollowWhenHolding)
                followerObject.LeaveObject();
        }
    }
    public void LeaveObject()
    {
        isFollowing = false;
        currentFollowObject = null;
        if (gameObject.GetComponent<WanderAI>() != null)
            gameObject.GetComponent<WanderAI>().IsWandering = true;
    }
}
