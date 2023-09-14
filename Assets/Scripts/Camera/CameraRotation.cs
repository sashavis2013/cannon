using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraRotation : MonoBehaviour
{
    public float rotationSpeed = 25.0f;
    public float minRotationAngleY = -45.0f;
    public float maxRotationAngleY = 45.0f;
    // private Vector3 _initialRotation;
    private float _currentRotationY;
    
    private void Start()
    {
        // _initialRotation = transform.eulerAngles;
        _currentRotationY = transform.localEulerAngles.y;
    }
    
    private void LateUpdate()
    {
        // var eulerAngles = transform.eulerAngles;
        // float mouseX = Input.GetAxis("Mouse X") * rotationSpeed * Time.deltaTime;
        // float newRotationY = Mathf.Clamp(eulerAngles.y + mouseX, _initialRotation.y + minRotationAngleY, _initialRotation.y + maxRotationAngleY);
        // eulerAngles = new Vector3(_initialRotation.x, newRotationY, _initialRotation.z);
        // transform.eulerAngles = eulerAngles;
        //
        
        // var eulerAngles = transform.eulerAngles;
        // float mouseX = Input.GetAxis("Mouse X") * rotationSpeed * Time.deltaTime;
        // float newRotationY = Mathf.Clamp(
        //     eulerAngles.y + mouseX,
        //     _initialRotation.y + minRotationAngleY,
        //     _initialRotation.y + maxRotationAngleY
        // );
        //
        // // Convert angles to the -180 to 180 degree range
        // float deltaAngle = Mathf.DeltaAngle(_initialRotation.y, newRotationY);
        // float clampedDeltaAngle = Mathf.Clamp(deltaAngle, minRotationAngleY, maxRotationAngleY);
        // float finalRotationY = _initialRotation.y + clampedDeltaAngle;
        //
        // eulerAngles = new Vector3(_initialRotation.x, finalRotationY, _initialRotation.z);
        // transform.eulerAngles = eulerAngles;
        
        
        float mouseX = Input.GetAxis("Mouse X") * rotationSpeed * Time.deltaTime;
        _currentRotationY += mouseX;
        _currentRotationY = Mathf.Clamp(_currentRotationY, minRotationAngleY, maxRotationAngleY);

        // Apply the rotation to the camera's local rotation
        transform.localEulerAngles = new Vector3(0, _currentRotationY, 0);

    }
}