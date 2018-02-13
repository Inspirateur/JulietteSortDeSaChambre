using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PrincessePouvoir : MonoBehaviour {

    private SphereCollider sphereCollider;
    public float icePowerDuration;

	// Use this for initialization
	void Start () {
        sphereCollider = GetComponent<SphereCollider>();
	}
	
	// Update is called once per frame
	void Update () {
        if (InputManager.GetButtonDown("pouvoirGlace"))
        {
            sphereCollider.enabled = true;
            StartCoroutine(WaitforIcePower());
        }
	}

    IEnumerator WaitforIcePower()
    {
        yield return new WaitForSeconds(0.05f);
        sphereCollider.enabled = false;

    }

}
