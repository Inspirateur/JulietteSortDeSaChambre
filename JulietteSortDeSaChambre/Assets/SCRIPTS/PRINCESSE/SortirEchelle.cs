using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SortirEchelle : MonoBehaviour {

	private Animator princesseAnimator;
	private Vector3 position;
	private GameObject princesse;
	private bool activate;
	private float time;
	private PrincesseDeplacement rb;
	public Vector3 positionjuliette;
	private Vector3 temp;
	private float x;
	private float y;
	private float z;


	// Use this for initialization
	void Start () {
		princesseAnimator = GameObject.FindGameObjectWithTag("Player").GetComponent<Animator>();
		princesse = GameObject.FindGameObjectWithTag("Player");
		rb = GameObject.FindGameObjectWithTag("Player").GetComponent<PrincesseDeplacement>();
		activate=false;
	}
	
	// Update is called once per frame
	void Update () {
		if(activate){
			if(time > Time.time){
			//	Debug.Log("je passe ici");
				princesse.transform.position = new Vector3(princesse.transform.position.x + x * Time.deltaTime, princesse.transform.position.y + y * Time.deltaTime, princesse.transform.position.z + z * Time.deltaTime );
			}

			if(princesseAnimator.GetBool("EndClimbing") && princesse.transform.position == positionjuliette){
				Debug.Log("je passe ici");
				princesse.GetComponent<Rigidbody>().useGravity = true;
			}
		}
		
	}

	void OnTriggerEnter(){
		
		princesseAnimator.SetBool("EndClimbing", true);
		princesseAnimator.SetBool("IsClimbing", false);
		time = Time.time + 2;
		position = princesse.transform.position;
		temp = positionjuliette - position;
		x = temp.x / (time - Time.time);
		y = temp.y / (time - Time.time);
		z = temp.z / (time - Time.time);
		//Debug.Log(x);
		activate=true;
		rb.canMove = false;
	}
}
