using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TRO_SuitTete : MonoBehaviour {
	public IA_Agent cerveau;
	private float taillePrincesse = 2.0f;

	// Use this for initialization
	void Start () {
	}
	
	// Update is called once per frame
	void Update () {
		if (cerveau.getPerception ().aRepere (cerveau.getPrincesse (), 1.0f)) {
			transform.LookAt (cerveau.getPrincesse ().transform.position + cerveau.getPrincesse ().transform.up * taillePrincesse);
			transform.rotation = Quaternion.FromToRotation (transform.up, transform.forward) * transform.rotation;
			transform.RotateAround (transform.up, 135);
		}
	}
}
