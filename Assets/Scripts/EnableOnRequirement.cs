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
    [SerializeField] private int _levelRequirement = -1;
    [SerializeField] private CharacterRequirement[] _characterRequirements;

    private void Start()
    {
        
    }

    /// <summary>
    /// Returns true if all requirements are met.
    /// </summary>
    /// <returns></returns>
    public bool MeetsRequirements()
    {
        if (_levelRequirement > -1 && _levelRequirement != LevelManager.Instance.CurrentLevel)
            return false;
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
