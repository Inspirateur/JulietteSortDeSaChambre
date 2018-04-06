using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PiegeMort : MonoBehaviour
{
	PrincesseVie juliette;
	// Use this for initialization
	void Start ()
	{
		juliette = GameObject.FindGameObjectWithTag ("Player").GetComponent<PrincesseVie>();
	}
	
	// Update is called once per frame
	void Update ()
	{
	
	}

	void OnTriggerEnter (Collider col) {
		if (col.gameObject.tag == "Player")
		{
			Debug.Log ("Test");
			if (juliette.enVie()) {
				juliette.mourir();
			}
		}
		if (col.gameObject.tag == "Mob")
		{
			Debug.Log ("Mob touché");
			col.gameObject.GetComponent<IA_Agent>().mourir();
		}
	}
}


