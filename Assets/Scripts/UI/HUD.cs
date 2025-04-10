/*
 * HUD.cs
 * Marlow Greenan
 * 2/19/2025
 * 
 * Buttons and functions for the HUD
 */
using System.Collections;
using UnityEngine;

public class HUD : MonoBehaviour
{
    [SerializeField] private GameManager _gameManager;

    [SerializeField] private GameObject _characterMovement;
    [SerializeField] private GameObject _inventory;
    [SerializeField] private GameObject _pauseButton;
    private Enums.Direction faceDirection = Enums.Direction.Forward;

    public Enums.Direction FaceDirection { get => faceDirection; set => faceDirection = value; }

    /// <summary>
    /// Moves player in the direction that they are facing.
    /// </summary>
    public void Button_Move()
    {
        if (!CoderMethods.IsMoving && !BoundaryDetector.BoundaryAhead)
        {
            switch (faceDirection)
            {
                case Enums.Direction.Forward:
                    StartCoroutine(AnimatePositionMove(_gameManager.MoveSpeed,
                        _gameManager.PlayerAvatar.transform, Enums.Axis.z, _gameManager.CellSize.z)); break;
                case Enums.Direction.Right:
                    StartCoroutine(AnimatePositionMove(_gameManager.MoveSpeed,
                        _gameManager.PlayerAvatar.transform, Enums.Axis.x, _gameManager.CellSize.x)); break;
                case Enums.Direction.Backwards:
                    StartCoroutine(AnimatePositionMove(_gameManager.MoveSpeed,
                        _gameManager.PlayerAvatar.transform, Enums.Axis.z, -_gameManager.CellSize.z)); break;
                case Enums.Direction.Left:
                    StartCoroutine(AnimatePositionMove(_gameManager.MoveSpeed,
                        _gameManager.PlayerAvatar.transform, Enums.Axis.x, -_gameManager.CellSize.x)); break;
            }
        }
    }
    /// <summary>
    /// Rotates the camera to the player's right.
    /// </summary>
    public void Button_TurnRight()
    {
        if (!CoderMethods.IsMoving)
            StartCoroutine(AnimateRotate(_gameManager.TurnSpeed, _gameManager.PlayerAvatar.transform, Enums.Axis.y, 90));
    }

    /// <summary>
    /// Rotates the camera to the player's left.
    /// </summary>
    public void Button_TurnLeft()
    {
        if (!CoderMethods.IsMoving)
            StartCoroutine(AnimateRotate(_gameManager.TurnSpeed, _gameManager.PlayerAvatar.transform, Enums.Axis.y, -90));
    }
    private IEnumerator AnimatePositionMove(float speed, Transform transform, Enums.Axis axis, float amount)
    {
        DisableHUD();
        CoderMethods.IsMoving = true;
        float moveAmount = 1;
        if (amount < 0)
            moveAmount *= -1;
        float amountMoved = 0;
        while (amountMoved < Mathf.Abs(amount))
        {
            switch (axis)
            {
                case Enums.Axis.x: transform.position += new Vector3(moveAmount, 0, 0); break;
                case Enums.Axis.y: transform.position += new Vector3(0, moveAmount, 0); break;
                case Enums.Axis.z: transform.position += new Vector3(0, 0, moveAmount); break;
            }
            amountMoved += Mathf.Abs(moveAmount);
            yield return new WaitForSeconds((Mathf.Abs(moveAmount) / speed) * Time.deltaTime);
        }
        CoderMethods.IsMoving = false;
        if (!GameManager.Instance.InUI)
            EnableHUD();
    }

    /// <summary>
    /// Shows the player's slowly rotating in a certain direction.
    /// </summary>
    /// <param name="speed"></param>
    /// <param name="transform"></param>
    /// <param name="axis"></param>
    /// <param name="degrees"></param>
    /// <returns></returns>
    private IEnumerator AnimateRotate(float speed, Transform transform, Enums.Axis axis, float degrees)
    {
        DisableHUD();
        CoderMethods.IsMoving = true;
        float rotateAmount = 1;
        if (degrees < 0)
            rotateAmount *= -1;
        float degreesRotated = 0;
        while (degreesRotated < Mathf.Abs(degrees))
        {
            switch (axis)
            {
                case Enums.Axis.x: transform.Rotate(new Vector3(rotateAmount, 0, 0)); break;
                case Enums.Axis.y: transform.Rotate(new Vector3(0, rotateAmount, 0)); break;
                case Enums.Axis.z: transform.Rotate(new Vector3(0, 0, rotateAmount)); break;
            }
            degreesRotated += Mathf.Abs(rotateAmount);
            yield return new WaitForSeconds((Mathf.Abs(rotateAmount) / speed) * Time.deltaTime);
        }
        CoderMethods.IsMoving = false;
        UpdateFaceDirection();
        if (!GameManager.Instance.InUI)
            EnableHUD();
    }

    /// <summary>
    /// Updates the player's face direction variable.
    /// </summary>
    private void UpdateFaceDirection()
    {
        float yRotation = _gameManager.PlayerAvatar.transform.localRotation.eulerAngles.y;
        switch (yRotation)
        {
            case > -5 and < 5: faceDirection = Enums.Direction.Forward; break;
            case > 85 and < 95: faceDirection = Enums.Direction.Right; break;
            case > 175 and < 185: faceDirection = Enums.Direction.Backwards; break;
            case > 265 and < 275: faceDirection = Enums.Direction.Left; break;
        }
    }

    /// <summary>
    /// Plays HUD enable animations, putting them on screen.
    /// </summary>
    public void EnableHUD()
    {
        _characterMovement.GetComponent<Animator>().Play("ENABLE");
        _inventory.GetComponent<Animator>().Play("ENABLE");
        _pauseButton.GetComponent<Animator>().Play("ENABLE");
    }

    /// <summary>
    /// Plays HUD disable animations, taking them off screen.
    /// </summary>
    public void DisableHUD()
    {
        _characterMovement.GetComponent<Animator>().Play("DISABLE");
        _inventory.GetComponent<Animator>().Play("DISABLE");
        _pauseButton.GetComponent<Animator>().Play("DISABLE");
    }
}
