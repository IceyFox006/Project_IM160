/*
 * RotationRange.cs
 * Marlow Greenan
 * 2/23/2025
 * 
 * Assigns a random rotation within a range to gameObject on the chosen axis.
 */
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class RotatingAxis
{
    [SerializeField] private Enums.Axis _axis;
    [SerializeField] private float _minRotation = 0f;
    [SerializeField] private float _maxRotation = 360f;

    public Enums.Axis Axis { get => _axis; set => _axis = value; }
    public float MinRotation { get => _minRotation; set => _minRotation = value; }
    public float MaxRotation { get => _maxRotation; set => _maxRotation = value; }
}
public class RotationRange : MonoBehaviour
{
    [SerializeField] private List<RotatingAxis> _rotatingAxes = new List<RotatingAxis>();

    void Start()
    {
        RandomRotationTransform();
    }
    private void RandomRotationTransform()
    {
        foreach (RotatingAxis rotatingAxis in _rotatingAxes)
        {
            float randomRotation = Random.Range(rotatingAxis.MinRotation, rotatingAxis.MaxRotation);
            switch (rotatingAxis.Axis)
            {
                case Enums.Axis.x: transform.Rotate(new Vector3(transform.rotation.x + randomRotation, 0, 0)); break;
                case Enums.Axis.y: transform.Rotate(new Vector3(0, transform.rotation.y + randomRotation, 0)); break;
                case Enums.Axis.z: transform.Rotate(new Vector3(0, 0, transform.rotation.z + randomRotation)); break;
            }
        }
    }
}

