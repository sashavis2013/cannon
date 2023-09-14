using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;
[RequireComponent(typeof(Transform))]
[RequireComponent(typeof(PowerTextManager))]
public class CannonController : MonoBehaviour
{
    public GameObject cannonballPrefab;
     [SerializeField] private Transform cylinderTransform;
     private PowerTextManager _powerTextManager;

    private static Camera _mainCam;
    public float recoilOffset=0.5f;
    
    public float timeBtwShots = 0.5f;  //Time between shots
    private float _timeOfLastShot;  
    
    public Transform firePoint;
    public float fireForce = 15f;
    private readonly float _maxFireForce = 100f;
    
    private readonly float _scale = 0.5f;

    [SerializeField] private BulletTrajectoryPrediction prediction;

    private void Start()
    {
        if (TryGetComponent(out PowerTextManager powerTextManager))
        {
            _powerTextManager = powerTextManager;
        }
    }

    private void Awake()
    {
        _mainCam=Camera.main;
        
    }
    
    public void FireCannon()
    {
        GameObject cannonball = GameObject.Instantiate(cannonballPrefab, firePoint.position,Quaternion.identity,firePoint);
        CannonballPhysics cannonballScript = cannonball.GetComponent<CannonballPhysics>();
        cannonballScript.position = firePoint.position;
        
        // Calculate the initial velocity vector based on the cannon's rotation
        Vector3 fireDirection = transform.rotation * Vector3.right;

        if (cannonballScript)
        {
            cannonballScript.Launch(fireDirection * fireForce); // Example initial velocity
        }
    }

    private static IEnumerator Shake(float duration, float magnitude) {

        float elapsed = 0.0f;

        Vector3 originalCamPos = _mainCam.transform.position;

        while (elapsed < duration) {

            elapsed += Time.deltaTime;          

            float percentComplete = elapsed / duration;         
            float damper = 1.0f - Mathf.Clamp(4.0f * percentComplete - 3.0f, 0.0f, 1.0f);
            
            float x = Random.value * 2.0f - 1.0f;
            float y = Random.value * 2.0f - 1.0f;
            x *= magnitude * damper;
            y *= magnitude * damper;

            _mainCam.transform.position = new Vector3(x + originalCamPos.x, y + originalCamPos.y, originalCamPos.z); 

            yield return null;
        }

        _mainCam.transform.position = originalCamPos;
    }
    
    private IEnumerator SmoothLerp (float time)
    {
        Vector3 startingPos  = cylinderTransform.position;
        Vector3 finalPos = cylinderTransform.position + (cylinderTransform.up * recoilOffset);

        float elapsedTime = 0;
        
        while (elapsedTime < time)
        {
            cylinderTransform.position = Vector3.Lerp(startingPos, finalPos, (elapsedTime / time));
            elapsedTime += Time.deltaTime;
            yield return null;
        }

        elapsedTime = 0f;
        while (elapsedTime < time)
        {
            cylinderTransform.position = Vector3.Lerp(finalPos, startingPos, (elapsedTime / time));
            elapsedTime += Time.deltaTime;
            yield return null;
        }
    }
    

    
    private void Update()
    {
        if (Input.mouseScrollDelta.y != 0)
        {
            fireForce=Mathf.Clamp(fireForce + Input.mouseScrollDelta.y * _scale,0,_maxFireForce);
            prediction.fireForce = fireForce;
            _powerTextManager.UpdateUI((int)fireForce);
        }
        
        if (Input.GetMouseButtonDown(0))
        {
            
            if (Time.time - _timeOfLastShot >= timeBtwShots ) //If the time elapsed is more than the fire rate, allow a shot
            {
                FireCannon();
                StartCoroutine(Shake(0.1f, 0.05f));
                StartCoroutine (SmoothLerp (0.2f));
                _timeOfLastShot = Time.time;   //set new time of last shot
            }  
            
        }
    }
}