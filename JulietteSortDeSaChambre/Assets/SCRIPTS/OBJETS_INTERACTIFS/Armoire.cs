using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Armoire : ObjetEnvironnemental {


	private GameObject julietteBody;


	void Start () {
		julietteBody = GameObject.FindGameObjectWithTag ("Player").transform.GetChild (1).gameObject;

	}
	public override void Activation(){
		julietteBody.GetComponent<reskinJuliette> ().Reskin();
	}


}
