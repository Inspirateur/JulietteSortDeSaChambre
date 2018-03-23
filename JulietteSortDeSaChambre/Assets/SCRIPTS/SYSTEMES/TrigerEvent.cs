using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrigerEvent : MonoBehaviour {

	void OnTriggerEnter(Collider other){

		/*if (other.gameObject.tag.Equals ("Player")) {
			GameObject.FindGameObjectWithTag ("MainCamera").GetComponent<CinematiqueManager> ().lanceCinématique (0);
		}
*/

		GetComponent<EventManager> ().activation ();
		foreach(Collider collider in GetComponents<Collider>()){
			collider.enabled = false;
		}
	}
}
