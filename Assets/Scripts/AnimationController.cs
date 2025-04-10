/* 
 * AnimationController.cs
 * Marlow Greenan
 * 3/30/2025
 * 
 * Controls animations.
 */
using UnityEngine;

[RequireComponent(typeof(Animator))]
public class AnimationController : MonoBehaviour
{
    private Enums.AnimationActions currentAction = Enums.AnimationActions.None;
    [SerializeField] private Enums.AnimationActions[] _animations;

    private void FixedUpdate()
    {
        CheckAnimation();
        UpdateAnimation();
    }
    public void CheckAnimation()
    {
        if (transform.GetComponent<Rigidbody>() != null)
        {
            if (transform.GetComponent<Rigidbody>().velocity.x != 0 || transform.GetComponent<Rigidbody>().velocity.z != 0)
                currentAction = Enums.AnimationActions.Run;
            else
                currentAction = Enums.AnimationActions.Idle;
        }
    }
    public void UpdateAnimation()
    {
        foreach (Enums.AnimationActions animation in _animations)
        {
            if (animation == currentAction)
            {
                GetComponent<Animator>().Play(animation.ToString().ToUpper());
                break;
            }
        }
    }
}
