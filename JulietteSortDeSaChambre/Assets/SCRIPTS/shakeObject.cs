using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class shakeObject : MonoBehaviour {

	public bool canShake;
	public float magnitudeRange;
	private Transform trans;
	public float timerMax = 0.2f;
	private float timer = 0;
	private Vector3 initialPos;

	void Start(){
		trans = GetComponent<Transform> ();
		initialPos = trans.position;
	}
	
	// Update is called once per frame
	void Update () {
		if (timer >= timerMax) {
			if (canShake) {
				Vector3 magnitude = new Vector3 (Random.Range (-magnitudeRange, magnitudeRange), 0, Random.Range (-magnitudeRange, magnitudeRange));
				trans.position = initialPos + magnitude;
			}
			timer = 0;
			return;
		}
		timer += Time.deltaTime;


	}

	public void Shake(){
		canShake = true;
	}
	public void UnShake(){
		canShake = false;
	}
}
