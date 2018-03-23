using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class Grimper : ObjetEnvironnemental {

	private bool activate;
	private Animator princesseAnimator;
	private GameObject princesse;
	private Vector3 princessetemp;

	// Use this for initialization
	void Start () {
		princesseAnimator = GameObject.FindGameObjectWithTag("Player").GetComponent<Animator>();
		princesse = GameObject.FindGameObjectWithTag("Player");
		activate = false;
	}

	public override void Activation(){

		if(!activate){
			activate = true;
			princesse.transform.position = new Vector3(princesse.transform.position.x,princesse.transform.position.y+1,princesse.transform.position.z);
			princesse.transform.LookAt(this.transform);
            princessetemp = princesse.transform.forward;
            princessetemp.y = 0;
            princesse.transform.forward = princessetemp;
			princesse.GetComponent<Rigidbody>().useGravity = false;
			princesseAnimator.SetBool("IsClimbing", true);
			princesseAnimator.SetBool("IsIdle", false);
			
			
		}
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
