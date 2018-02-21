using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class lameBalance : MonoBehaviour {
	public float vitesse = 0;

	// Use this for initialization
	void Start () {
	}

	// Update is called once per frame
	void Update () {
		vitesse -= Mathf.Sin(Mathf.Deg2Rad*(this.transform.eulerAngles.z))*Time.deltaTime;
		//Debug.Log (this.transform.eulerAngles.z+" -> "+Mathf.Cos(Mathf.Deg2Rad*(this.transform.eulerAngles.z))*Time.deltaTime);
		this.transform.Rotate(Vector3.forward * vitesse);
	}
}
