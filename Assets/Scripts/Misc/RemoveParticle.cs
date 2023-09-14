using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RemoveParticle : MonoBehaviour
{
    private void Start()
    {
        Destroy(gameObject,2f);
    }
}
