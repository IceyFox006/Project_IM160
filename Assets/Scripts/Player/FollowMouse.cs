/*
 * FollowMouse.cs
 * Marlow Greenan
 * 2/13/2025
 * 
 * Makes gameObject follow the mouse's x,y screen location.
 */
using UnityEngine;

public class FollowMouse : MonoBehaviour
{
    private Vector3 position;
    [SerializeField] private float _speed;

    /// <summary>
    /// Updates gameObject's position to the mouse's screen position.
    /// </summary>
    private void FixedUpdate()
    {
        position = Input.mousePosition;
        position.z = _speed;
        transform.position = Camera.main.ScreenToWorldPoint(position);
    }
}
