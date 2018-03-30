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
	private PrincesseArme arme;
	private float speed;
	private float time;


	// Use this for initialization
	void Start () {
		princesseAnimator = GameObject.FindGameObjectWithTag("Player").GetComponent<Animator>();
		princesse = GameObject.FindGameObjectWithTag("Player");
		arme = GameObject.FindGameObjectWithTag("Player").GetComponent<PrincesseArme>();
		speed = GameObject.FindGameObjectWithTag("Player").GetComponent<PrincesseDeplacement>().vitesse;
		rb = GameObject.FindGameObjectWithTag("Player").GetComponent<PrincesseDeplacement>();
		activate = false;
	}

	public override void Activation(){

		if(!activate && !princesseAnimator.GetBool("IsJumping")){
			time = Time.time + 1f;
			activate = true;
			princesse.transform.position = new Vector3(transform.position.x + 0.6f,princesse.transform.position.y + 0.2f,transform.position.z);
			princesse.transform.LookAt(this.transform);
            princessetemp = princesse.transform.forward;
            princessetemp.y = 0;
            princesse.transform.forward = princessetemp;
			princesse.GetComponent<Rigidbody>().useGravity = false;
			//arme.armeActive
			rb.canMove = false;
			princesseAnimator.Play("idle1");
			rb.gererAnim("IsClimbing");
			rb.canMove = true;
		/*	princesseAnimator.Play("idle1");
			rb.gererAnim("IsClimbing");
			rb.canMove = true;*/
			
		}
		
	}
	
	// Update is called once per frame
	void Update () {

		if(activate && rb.isGrounded && time < Time.time){
			activate = false;
			princesse.GetComponent<Rigidbody>().useGravity = true;
			princesseAnimator.Play("idle1");
		}
	}
}
