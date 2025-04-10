/*
 * FirstPersonCamera.cs
 * Marlow Greenan
 * 2/19/2025
 * 
 * Makes the camera rotate with the mouse screen position.
 */
using System;
using UnityEngine;

public class FirstPersonCamera : MonoBehaviour
{
    [SerializeField] private Transform _playerAvatar;
    [SerializeField] private float _mouseSensitivity = 2f;
    [SerializeField] private Vector2 _rotationLimits = new Vector2(0, 0);
    private float cameraVerticalRotation = 0f;

    [SerializeField] private bool lockedCursor = false;

    /// <summary>
    /// Makes cursor invisible and locks mouse in the center of the screen.
    /// </summary>
    private void Start()
    {
        Cursor.visible = false;
        if (lockedCursor)
            Cursor.lockState = CursorLockMode.Locked;
    }

    /// <summary>
    /// Rotates the camera based on the mouses screen location.
    /// </summary>
    private void LateUpdate()
    {
        float inputX = Input.GetAxis("Mouse X") * _mouseSensitivity;
        float inputY = Input.GetAxis("Mouse Y") * _mouseSensitivity;

        //Rotate Camera on X-Axis (vertical)
        cameraVerticalRotation -= inputY;
        cameraVerticalRotation = Mathf.Clamp(cameraVerticalRotation, -_rotationLimits.x, _rotationLimits.x);
        transform.localEulerAngles = Vector3.right * cameraVerticalRotation;

        //Rotate Player on Y-Axis (horizontal)
        inputX = Mathf.Clamp(inputX, -_rotationLimits.y, _rotationLimits.y);
        _playerAvatar.Rotate(Vector3.up * inputX);
    }
}
