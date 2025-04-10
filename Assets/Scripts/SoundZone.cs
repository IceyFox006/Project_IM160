/* 
 * SoundZone.cs
 * Marlow Greenan
 * 4/3/2025
 */
using UnityEngine;

[RequireComponent(typeof(AudioSource))]
public class SoundZone : MonoBehaviour
{
    private void OnTriggerEnter(Collider collision)
    {
        if (collision.CompareTag("Player"))
        {
            GetComponent<AudioSource>().Play();
            GetComponent<AudioSource>().mute = false;
        }

    }
    private void OnTriggerExit(Collider collision)
    {
        if (collision.CompareTag("Player"))
        {
            GetComponent<AudioSource>().Stop();
            GetComponent<AudioSource>().mute = true;
        }
    }
}
