using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class iceCheckIfGrounded : MonoBehaviour {

	// Use this for initialization
	private MeshRenderer mesh;
	void Start () {
		mesh=GetComponent<MeshRenderer>();
		mesh.enabled=false;
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	void OnTriggerEnter(Collider other) {
		if(other.tag=="sol"){
			mesh.enabled=true;
		}
		// Debug.Log(other.tag);
    }
	
}
