using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EffetVieUp : MonoBehaviour {

	public float speedRotation;
	public float stopMeAfter;

	private float timer;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		transform.Rotate(0.0f, speedRotation * Time.deltaTime, 0.0f);
		if(Time.time >= timer){
			this.gameObject.SetActive(false);
		}
	}

	public void startEffect(){
		timer = Time.time + stopMeAfter;
		this.gameObject.SetActive(true);
	}
}
