using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CannonballCollision : MonoBehaviour
{
    public float raycastLength = 0.2f;
    public LayerMask collisionLayer;
    public delegate void CollisionEvent(RaycastHit hit);
    public event CollisionEvent OnCollision;
    

    private void Update()
    {
        
        Vector3[] rayDirections =
            { transform.forward, -transform.forward, transform.right, -transform.right, transform.up, -transform.up };

        foreach (Vector3 direction in rayDirections)
        {
            if (Physics.Raycast(transform.position, direction, out RaycastHit hit, raycastLength, collisionLayer))
            {
                // Check if the bullet has already collided, and ignore subsequent collisions
                

                
                // A collision occurred with an object in the collision layer.
                if (OnCollision != null) OnCollision(hit);
                
                
            }
        }
    }


}