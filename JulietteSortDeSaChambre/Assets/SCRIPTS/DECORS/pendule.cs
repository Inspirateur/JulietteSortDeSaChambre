using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class pendule : MonoBehaviour {

	[Range(0,359)]
	public int angleY;

	[Range(0,90)]
	public int angleZ;
	private float angleZInitial;

	public float speed = 1.5f;
	private float startTime;

	private bool stop;
	private bool canStart;

	void Start () {
		angleZInitial = this.transform.rotation.eulerAngles.z;
		StartCoroutine (Wait());
	}

	void Update (){
		startTime += Time.deltaTime;
		if (!stop) {
			rotation ();
		}
	}

	void rotation() {
		transform.rotation = Quaternion.Lerp (
			Quaternion.Euler(this.transform.rotation.x,angleY,angleZ), 
			Quaternion.Euler(this.transform.rotation.x,angleY,-angleZ),
			(Mathf.Sin(startTime * speed + Mathf.PI/2) + 1.0f)/ 2.0f
		);
	}

	void setVitesse(float vitesse) {
		speed = vitesse;
	}

	void stopPendule() {
		canStart = false;
		stop = true;
		setPenduleInitialPosition ();
	}

	void setPenduleInitialPosition() {
		Debug.Log (angleZInitial);
		if (this.transform.rotation.eulerAngles.z <= angleZInitial) {
			this.transform.Rotate (Vector3.forward*speed*2*Time.deltaTime);
			Invoke ("setPenduleInitialPosition",0f);
		} else {
			transform.rotation = Quaternion.Euler(this.transform.rotation.x,angleY,angleZInitial);
			canStart = true;
		}
	}

	void startPendule() {
		if (canStart) {
			startTime = 0;
			stop = false;
		} else {
			Invoke ("startPendule",0f);
		}
	}

	IEnumerator Wait() {
		yield return new WaitForSeconds (4f);
		stopPendule ();
		yield return new WaitForSeconds (4f);
		startPendule ();
	}
}
