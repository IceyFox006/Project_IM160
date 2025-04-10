/*
 * ScaleRange.cs
 * Marlow Greenan
 * 2/23/2025
 * 
 * Assigns a random scale within a range to gameObject.
 */
using UnityEngine;

public class ScaleRange : MonoBehaviour
{
    [SerializeField] private float minScale = 0.75f;
    [SerializeField] private float maxScale = 2.0f;
    void Start()
    {
        RandomScaleTransform();
    }
    private void RandomScaleTransform()
    {
        float randomScale = Random.Range(minScale, maxScale);
        transform.localScale = new Vector3(randomScale, randomScale, randomScale);
    }
}
