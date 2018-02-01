using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParticuleGenerator : MonoBehaviour
{
    public Transform hitEffect;

    // Use this for initialization
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    public void PlayParticule (Vector3 hitPoint)
    {
        if (hitEffect != null)
        {
            Instantiate(hitEffect, hitPoint, hitEffect.transform.rotation);
        }
    }

}