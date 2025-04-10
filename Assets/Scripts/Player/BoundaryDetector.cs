using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoundaryDetector : MonoBehaviour
{
    [SerializeField] private string _boundaryTag = "Boundary";
    private static bool boundaryAhead = false;

    public static bool BoundaryAhead { get => boundaryAhead; set => boundaryAhead = value; }

    private void OnTriggerEnter(Collider collision)
    {
        if (collision.gameObject.CompareTag(_boundaryTag))
            boundaryAhead = true;
    }
    private void OnTriggerExit(Collider collision)
    {
        if (collision.gameObject.CompareTag(_boundaryTag))
            boundaryAhead = false;
    }
}
