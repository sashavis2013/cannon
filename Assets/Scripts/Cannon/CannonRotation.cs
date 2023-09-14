using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CannonRotation : MonoBehaviour
{
    public float rotationSpeed = 25.0f;
    public float minRotationAngleY = -45.0f;
    public float maxRotationAngleY = 45.0f;
    public float maxRotationAngleZ = 60f;
    public float minRotationAngleZ = 0f;
    private Vector3 _initialRotation;
    
    private void Start()
    {
        _initialRotation = transform.eulerAngles;
        Cursor.lockState = CursorLockMode.Locked;
    }
    
    private void Update()
    {
        var eulerAngles = transform.eulerAngles;
        float mouseX = Input.GetAxis("Mouse X") * rotationSpeed * Time.deltaTime;
        float mouseY = Input.GetAxis("Mouse Y") * rotationSpeed * Time.deltaTime;
        float newRotationY = Mathf.Clamp(eulerAngles.y + mouseX, _initialRotation.y + minRotationAngleY, _initialRotation.y + maxRotationAngleY);
        float newRotationX = Mathf.Clamp(eulerAngles.z + mouseY, _initialRotation.z + minRotationAngleZ, _initialRotation.z + maxRotationAngleZ);

        eulerAngles = new Vector3(_initialRotation.x, newRotationY, newRotationX);
        transform.eulerAngles = eulerAngles;
    }
    
}
