/*
 * EnableOnRequirement.cs
 * Marlow Greenan
 * 4/9/2025
 * 
 * Enables the gameObject's children if the requirements on met.
 */
using UnityEngine;

public class EnableOnRequirement : MonoBehaviour
{
    [SerializeField] private Enums.Inequalities _levelInequalityCheck;
    [SerializeField] private int _levelRequirement = -1;
    [SerializeField] private CharacterRequirement[] _characterRequirements;

    /// <summary>
    /// Returns true if all requirements are met.
    /// </summary>
    /// <returns></returns>
    public bool MeetsRequirements()
    {
        //if (_levelRequirement > -1 && _levelRequirement != LevelManager.Instance.CurrentLevel)
        //    return false;

        //Checks level requirement
        if (_levelRequirement > -1)
        {
            switch (_levelInequalityCheck)
            {
                case Enums.Inequalities.None:
                    break;
                case Enums.Inequalities.LessThan:
                    if (_levelRequirement >= LevelManager.Instance.CurrentLevel)
                        return false;
                    break;
                case Enums.Inequalities.GreaterThan:
                    if (_levelRequirement <= LevelManager.Instance.CurrentLevel)
                        return false;
                    break;
                case Enums.Inequalities.Equal:
                    if (_levelRequirement != LevelManager.Instance.CurrentLevel)
                        return false;
                    break;
            }
        }

        //Checks character relationship score requirement.
        foreach (CharacterRequirement requirement in  _characterRequirements)
        {
            if (!requirement.MeetsRequirement())
                return false;
        }
        return true;
    }

    /// <summary>
    /// Enables or disables the gameObject's children based on whether the conditions are met.
    /// </summary>
    public static void SetActiveOnRequirement()
    {
        foreach (EnableOnRequirement onRequirement in GameObject.FindObjectsOfType<EnableOnRequirement>())
        {
            for (int index = 0; index < onRequirement.transform.childCount; index++)
            {
                if (onRequirement.MeetsRequirements())
                    onRequirement.transform.GetChild(index).gameObject.SetActive(true);
                else
                    onRequirement.transform.GetChild(index).gameObject.SetActive(false);
            }
        }
    }
}
