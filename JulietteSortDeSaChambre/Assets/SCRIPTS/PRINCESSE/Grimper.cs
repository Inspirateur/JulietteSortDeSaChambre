using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class Grimper : ObjetEnvironnemental {

	private bool activate;
	private Animator princesseAnimator;
	private GameObject princesse;
	private Vector3 princessetemp;

	private PrincesseDeplacement rb;
	private float speed;


	// Use this for initialization
	void Start () {
		princesseAnimator = GameObject.FindGameObjectWithTag("Player").GetComponent<Animator>();
		princesse = GameObject.FindGameObjectWithTag("Player");
		speed = GameObject.FindGameObjectWithTag("Player").GetComponent<PrincesseDeplacement>().vitesse;
		rb = GameObject.FindGameObjectWithTag("Player").GetComponent<PrincesseDeplacement>();
		activate = false;
	}

	public override void Activation(){

		if(!activate){
			activate = true;
			//princesse.transform.position = Vector3.MoveTowards(princesse.transform.position, transform.position, speed * Time.deltaTime);
			princesse.transform.position = new Vector3(transform.position.x + 0.6f,princesse.transform.position.y + 0.1f,transform.position.z);
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
		if(activate && rb.isGrounded){
			activate = false;
			princesse.GetComponent<Rigidbody>().useGravity = true;
		}
	}
}
