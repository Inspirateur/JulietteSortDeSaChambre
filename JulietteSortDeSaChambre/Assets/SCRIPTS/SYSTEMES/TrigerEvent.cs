using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrigerEvent : MonoBehaviour {

	void OnTriggerEnter(Collider other){

		if (other.gameObject.tag.Equals ("Player")) {
			GetComponent<EventManager> ().activation ();
			foreach(Collider collider in GetComponents<Collider>()){
				collider.enabled = false;
			}
		}

	}
}
