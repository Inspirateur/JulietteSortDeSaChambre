using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class pendule : MonoBehaviour {

	[Header("Direction de l'oscillation :")]
	[Range(0,359)]
	public int angleY;

	[Header("Amplitude de l'oscillation :")]
	[Range(0,90)]
	public int angleZ;
	private float angleZInitial;

	[Header("Reset Position Gauche ? (default = Droite) :")]
	public bool PositionResetGauche;

	[Header("Vitesse :")]
	public float speed = 1.5f;
	private float startTime;

	private bool stop;
	private bool canStart;

	void Start () {
		angleZInitial = this.transform.rotation.eulerAngles.z;
	}

	void Update (){
		startTime += Time.deltaTime;
		if (!stop) {
			if (PositionResetGauche)
			{
				rotationGauche ();
			} else if (!PositionResetGauche)
			{
				rotationDroite ();
			}
		}
	}

	void rotationGauche() {
		transform.rotation = Quaternion.Lerp (
			Quaternion.Euler(this.transform.rotation.x,angleY,-angleZ), 
			Quaternion.Euler(this.transform.rotation.x,angleY,angleZ),
			(Mathf.Sin(startTime * speed + Mathf.PI/2) + 1.0f)/ 2.0f
		);
	}

	void rotationDroite() {
		transform.rotation = Quaternion.Lerp (
			Quaternion.Euler(this.transform.rotation.x,angleY,angleZ), 
			Quaternion.Euler(this.transform.rotation.x,angleY,-angleZ),
			(Mathf.Sin(startTime * speed + Mathf.PI/2) + 1.0f)/ 2.0f
		);
	}

	public void setVitesse(float vitesse) {
		speed = vitesse;
	}

	public void stopPendule() {
		canStart = false;
		stop = true;
		if (PositionResetGauche)
		{
			setPenduleInitialPositionGauche ();
		} else if (!PositionResetGauche)
		{
			setPenduleInitialPositionDroite ();
		}
	}

	void setPenduleInitialPositionGauche() {
		Debug.Log(this.transform.rotation.eulerAngles.z);
		if (this.transform.rotation.eulerAngles.z <= (360-angleZInitial) || this.transform.rotation.eulerAngles.z > angleZInitial) {
			this.transform.Rotate (Vector3.forward*speed*2*Time.deltaTime);
			Invoke ("setPenduleInitialPositionGauche",0f);
		} else {
			transform.rotation = Quaternion.Euler(this.transform.rotation.x,angleY,360-angleZInitial);
			canStart = true;
		}
	}

	void setPenduleInitialPositionDroite() {
		if (this.transform.rotation.eulerAngles.z <= (360-angleZInitial) || this.transform.rotation.eulerAngles.z > angleZInitial) {
			this.transform.Rotate (Vector3.back*speed*2*Time.deltaTime);
			Invoke ("setPenduleInitialPositionDroite",0f);
		} else {
			transform.rotation = Quaternion.Euler(this.transform.rotation.x,angleY,angleZInitial);
			canStart = true;
		}
	}

	public void startPendule() {
		if (canStart) {
			startTime = 0;
			stop = false;
		} else {
			Invoke ("startPendule",0f);
		}
	}
}
