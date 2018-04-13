using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class lancerEvenement : MonoBehaviour {

	// Use this for initialization
	void Start () {
		GetComponent<EventManager>().activation();
		this.gameObject.SetActive(false);
	}

}
