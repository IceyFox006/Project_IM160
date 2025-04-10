/*
 * RegionApplication.cs
 * Marlow Greenan
 * 2/23/2025
 * 
 * Applies color from material gradients to their regions on the shader graph.
 */
using UnityEngine;

public class RegionApplication : MonoBehaviour
{
    [SerializeField] private Gradient _region0Gradient;
    [SerializeField] private Gradient _region1Gradient;
    [SerializeField] private Gradient _region2Gradient;
    [SerializeField] private Gradient _region3Gradient;
    [SerializeField] private Gradient _region4Gradient;
    [SerializeField] private Gradient _region5Gradient;

    private Color[] _regionColors = new Color[6];

    private void Start()
    {
        CreateColors();
        ColorRegions();
    }

    /// <summary>
    /// Generates colors from within the regions gradient range.
    /// </summary>
    private void CreateColors()
    {
        _regionColors[0] = _region0Gradient.Evaluate(Random.Range(0.0f, 1.0f));
        _regionColors[1] = _region1Gradient.Evaluate(Random.Range(0.0f, 1.0f));
        _regionColors[2] = _region2Gradient.Evaluate(Random.Range(0.0f, 1.0f));
        _regionColors[3] = _region3Gradient.Evaluate(Random.Range(0.0f, 1.0f));
        _regionColors[4] = _region4Gradient.Evaluate(Random.Range(0.0f, 1.0f));
        _regionColors[5] = _region5Gradient.Evaluate(Random.Range(0.0f, 1.0f));
    }

    /// <summary>
    /// Colors in the regions with the region colors.
    /// </summary>
    private void ColorRegions()
    {
        foreach (MeshRenderer renderer in GetComponentsInChildren<MeshRenderer>())
        {
            renderer.material.SetColor("_Region0_Color", _regionColors[0]);
            renderer.material.SetColor("_Region1_Color", _regionColors[1]);
            renderer.material.SetColor("_Region2_Color", _regionColors[2]);
            renderer.material.SetColor("_Region3_Color", _regionColors[3]);
            renderer.material.SetColor("_Region4_Color", _regionColors[4]);
            renderer.material.SetColor("_Region5_Color", _regionColors[5]);
        }
    }
}
