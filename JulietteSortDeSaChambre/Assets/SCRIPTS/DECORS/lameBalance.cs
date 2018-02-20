using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class lameBalance : MonoBehaviour {

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		transform.RotateAround(this.gameObject.transform.GetChild(0).position, Vector3.left, 500 * Time.deltaTime);
	}
}
