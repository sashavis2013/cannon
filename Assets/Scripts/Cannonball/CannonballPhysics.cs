using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CannonballPhysics : MonoBehaviour
{
    public Vector3 position;
    private Vector3 _velocity;
    private readonly Vector3 _acceleration = new Vector3(0, -9.81f, 0); // Gravity
    private const float TimeStep = 0.01f;
    private const float BounceFactor = 0.7f;
    private bool _hasCollided;
    private const float MinSlopeForBounce = 30f;
    [SerializeField] private GameObject explosionVFX;
    private CannonballCollision _cannonballCollision;
    [SerializeField] private GameObject decalPrefab;

    private void Start()
    {
        _cannonballCollision = GetComponent<CannonballCollision>();
        if (_cannonballCollision != null)
        {
            _cannonballCollision.OnCollision += HandleCollision;
        }
    }

    void Update()
    {
        _velocity += _acceleration * TimeStep;
        position += _velocity * TimeStep;
        transform.position = position;
    }
    
    
    public void Launch(Vector3 initialVelocity)
    {
        _velocity = initialVelocity;
    }
    
    private void HandleCollision(RaycastHit hit)
    {

        if (hit.transform.gameObject.layer == LayerMask.NameToLayer("Panel")) 
        {
            Vector3 pointOfImpact = hit.point;
            
            // Create a new GameObject for the decal
            GameObject decalObject = Instantiate(decalPrefab, pointOfImpact, Quaternion.identity);
            decalObject.transform.forward = -hit.normal;

            

        }
        
        float slopeAngle = Vector3.Angle(hit.normal, Vector3.up);

        
        if (_hasCollided)
        {
            DestroyCannonball(); 
            return;
        }
        
        // Calculate the new velocity after bouncing off the surface
        Vector3 surfaceNormal = hit.normal;
        _velocity = Vector3.Reflect(_velocity, surfaceNormal) * BounceFactor;

        if(slopeAngle >= MinSlopeForBounce)
        // Update the position to the point of collision to prevent tunneling
            transform.position += hit.point + surfaceNormal * 0.01f;
        else
        {
            transform.position = hit.point + surfaceNormal * 0.01f;
        }

        // Handle the collision here (e.g., apply damage, play particle effects)
        // You can access raycast hit information via the 'hit' parameter
        _hasCollided = true;
    }

    private void DestroyCannonball()
    {
        Instantiate(explosionVFX, transform.position, Quaternion.identity);
        _cannonballCollision.OnCollision -= HandleCollision;
        Destroy(gameObject);
    }

}
