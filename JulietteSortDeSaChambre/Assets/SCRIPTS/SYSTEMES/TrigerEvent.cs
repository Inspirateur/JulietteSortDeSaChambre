using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrigerEvent : MonoBehaviour {

	void OnTriggerEnter(Collider other){
		GetComponent<EventManager> ().activation ();
		foreach(Collider collider in GetComponents<Collider>()){
			collider.enabled = false;
		}
	}
}
