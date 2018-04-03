﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrigerEvent : MonoBehaviour {

	void OnTriggerEnter(Collider other){

		GetComponent<EventManager> ().activation ();
		this.gameObject.SetActive(false);
	}
}
