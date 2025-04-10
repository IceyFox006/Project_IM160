/* 
 * CoderMethods.cs
 * Marlow Greenan
 * 2/20/2025
 */
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class CoderMethods : MonoBehaviour
{
    private static bool isMoving = false;

    public static bool IsMoving { get => isMoving; set => isMoving = value; }

    public static bool LayerEqualsLayerMask(GameObject layerObject, LayerMask layerMask)
    {
        if (((1 << layerObject.layer) & layerMask.value) != 0)
            return true;
        return false;
    }
    public static IEnumerator AnimatePositionMove(float speed, Transform transform, Enums.Axis axis, float amount)
    {
        isMoving = true;
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
        isMoving = false;
    }
    public static IEnumerator AnimateRotate(float speed, Transform transform, Enums.Axis axis, float degrees)
    {
        isMoving = true;
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
        isMoving = false;
    }
    public static void DestroyChildren(Transform parent)
    {
        foreach (Transform child in parent)
        {
            Destroy(child.gameObject);
        }
    }
    public static void AddMaterialToGameObject(GameObject gameObject, Material material)
    {
        foreach (MeshRenderer renderer in gameObject.GetComponentsInChildren<MeshRenderer>())
        {
            List<Material> materials = renderer.materials.ToList();
            materials.Add(material);
            renderer.materials = materials.ToArray();
        }
    }
    public static void RemoveMaterialFromGameObject(GameObject gameObject, int index)
    {
        foreach (MeshRenderer renderer in gameObject.GetComponentsInChildren<MeshRenderer>())
        {
            List<Material> materials = renderer.materials.ToList();
            materials.RemoveAt(index);
            renderer.materials = materials.ToArray();
        }
    }
}
