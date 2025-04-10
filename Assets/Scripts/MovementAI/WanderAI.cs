/*
 * WanderAI.cs
 * Marlow Greenan
 * 2/22/2025
 */
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.AI;

[RequireComponent(typeof(Rigidbody))]
[RequireComponent(typeof(NavMeshAgent))]
public class WanderAI : MonoBehaviour
{
    [SerializeField] private NavMeshAgent _navMeshAgent;
    [Range(0, 100)][SerializeField] private float _speed;
    [Range(1, 500)][SerializeField] private float _walkRadius;
    private bool _isWandering = true;

    public bool IsWandering { get => _isWandering; set => _isWandering = value; }

    private void Start()
    {
        _navMeshAgent = GetComponent<NavMeshAgent>();
        if (_navMeshAgent != null)
        {
            _navMeshAgent.speed = _speed;
            _navMeshAgent.SetDestination(RandomNavMeshLocation());
        }
    }
    private void Update()
    {
        if (!_isWandering)
            return;
        if (_navMeshAgent != null && _navMeshAgent.remainingDistance <= _navMeshAgent.stoppingDistance) //If gameObject has arrived at the destination, choose a new destination.
            _navMeshAgent.SetDestination(RandomNavMeshLocation());
    }
    /// <summary>
    /// Creates a new random destination within wander range.
    /// </summary>
    /// <returns></returns>
    private Vector3 RandomNavMeshLocation()
    {
        Vector3 finalPosition = Vector3.zero;
        Vector3 randomPosition = Random.insideUnitSphere * _walkRadius;
        randomPosition += transform.position;
        if (NavMesh.SamplePosition(randomPosition, out NavMeshHit hit, _walkRadius, 1))
            finalPosition = hit.position;
        return finalPosition;
    }
}
