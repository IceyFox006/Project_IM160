/* 
 * StaticData.cs
 * Marlow Greenan
 * 3/30/2025
 */
using UnityEngine;

public static class StaticData
{
    [Header("Save")]
    private static int continueLevel = 0;

    [Header("Settings")]
    [Range(0, 1)] private static float volumeMaster = 1f;
    [Range(0, 1)] private static float volumeAmbient = 1f;
    [Range(0, 1)] private static float volumeSFX = 1f;


    public static int ContinueLevel { get => continueLevel; set => continueLevel = value; }
    public static float VolumeMaster { get => volumeMaster; set => volumeMaster = value; }
    public static float VolumeAmbient { get => volumeAmbient; set => volumeAmbient = value; }
    public static float VolumeSFX { get => volumeSFX; set => volumeSFX = value; }
}
