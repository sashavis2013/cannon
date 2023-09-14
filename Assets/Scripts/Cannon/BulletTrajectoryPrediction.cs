using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletTrajectoryPrediction : MonoBehaviour
{
    public LineRenderer lineRenderer;
    public int numPoints = 100;
    public float predictionTime = 2.0f;
    public LayerMask collisionLayer;
    public float fireForce = 15f; 
    private Vector3[] _linePositions;

    private void Start()
    {
        if (!lineRenderer)
            lineRenderer = gameObject.GetComponent<LineRenderer>();
        lineRenderer.positionCount = numPoints;
        _linePositions = new Vector3[numPoints];
    }

    private void Update()
    {
        SimulateTrajectory();
    }

    private void SimulateTrajectory()
    {
        Vector3 currentPosition = transform.position;
        Vector3 initialVelocity = transform.up * (-1 * fireForce);

        float timeStep = predictionTime / numPoints;

        for (int i = 0; i < numPoints; i++)
        {
            float currentTime = i * timeStep;
            _linePositions[i] = currentPosition + initialVelocity * currentTime +
                               Physics.gravity * (0.5f * currentTime * currentTime);

            // Check for collisions
            if (Physics.Raycast(currentPosition, initialVelocity.normalized, out RaycastHit hit, initialVelocity.magnitude * currentTime, collisionLayer))
            {
                lineRenderer.positionCount = i + 1;
                _linePositions[i] = hit.point;

                break;
            }
        }

        lineRenderer.SetPositions(_linePositions);
    }
}