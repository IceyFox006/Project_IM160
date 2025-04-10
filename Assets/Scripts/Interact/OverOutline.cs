/*
 * OverOutline.cs
 * Marlow Greenan
 * 3/19/2025
 * 
 * Highlights the gameObject when the mouse is over it.
 */
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

[RequireComponent(typeof(Collider))]
[RequireComponent(typeof(Rigidbody))]
public class OverOutline : MonoBehaviour
{
    private void OnMouseEnter()
    {
        foreach (MeshRenderer renderer in GetComponentsInChildren<MeshRenderer>())
        {
            List<Material> materials = renderer.materials.ToList();
            materials.Add(GameManager.Instance.OverOutline);
            renderer.materials = materials.ToArray();
        }

    }
    private void OnMouseExit()
    {
        foreach (MeshRenderer renderer in GetComponentsInChildren<MeshRenderer>())
        {
            List<Material> materials = renderer.materials.ToList();
            materials.RemoveAt(materials.Count - 1);
            renderer.materials = materials.ToArray();
        }
    }
}
