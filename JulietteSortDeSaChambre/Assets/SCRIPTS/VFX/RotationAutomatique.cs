using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotationAutomatique : MonoBehaviour {
	
	public float rotationParFrame;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		this.transform.Rotate(0.0f, this.rotationParFrame, 0.0f);
	}
}
