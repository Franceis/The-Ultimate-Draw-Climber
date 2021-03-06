using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Wheel : MonoBehaviour
{
    /// <summary>
    /// Class to hold the Wheel components
    /// </summary>

    [SerializeField] private LineRenderer lineRenderer;
    public LineRenderer LineRenderer { get { return lineRenderer; } }

    [SerializeField] private EdgeCollider2D edgeCollider;
    public EdgeCollider2D EdgeCollider { get { return edgeCollider; } }

}
