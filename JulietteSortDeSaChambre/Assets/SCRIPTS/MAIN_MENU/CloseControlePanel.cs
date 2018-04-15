using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CloseControlePanel : MonoBehaviour {

	public MenuPrincipal mp;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		if (Input.GetButtonDown("Pause") || Input.GetButtonDown("Interagir")) {
			mp.RetourMenuPrincipal();
		}
	}
}
