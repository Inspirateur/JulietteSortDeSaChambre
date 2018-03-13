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
	private float angleZInitialInvert;

	[Header("Reset Position Gauche ? (default = Droite) :")]
	public bool PositionResetGauche;

	[Header("Vitesse :")]
	public float speed = 1.5f;
	private bool stop;
	private bool canStart;

    private AudioSource audioSource;

	void Start () {
        audioSource = GetComponentInChildren<AudioSource> ();
        angleZInitial = this.transform.rotation.eulerAngles.z;
		angleZInitialInvert = 360 - angleZInitial;
		// StartCoroutine(tranquille());
	}

	void Update (){
		if (!stop) 
		{
			if (PositionResetGauche)
			{
				rotationDroite ();
			} else
			{
				rotationGauche ();
			}
		} else {
			if (PositionResetGauche)
			{
				setPenduleInitialPositionGauche ();
			} else
			{
				setPenduleInitialPositionDroite ();
			}
		}
	}

	void rotationGauche() {
		transform.rotation = Quaternion.Lerp (
			Quaternion.Euler(this.transform.rotation.x,angleY,-angleZ), 
			Quaternion.Euler(this.transform.rotation.x,angleY,angleZ),
			(Mathf.Sin(Time.time * speed + Mathf.PI/2) + 1.0f)/ 2.0f
		);
	}

	void rotationDroite() {
		transform.rotation = Quaternion.Lerp (
			Quaternion.Euler(this.transform.rotation.x,angleY,angleZ), 
			Quaternion.Euler(this.transform.rotation.x,angleY,-angleZ),
			(Mathf.Sin(Time.time * speed + Mathf.PI/2) + 1.0f)/ 2.0f
		);
	}

	public void setVitesse(float vitesse) {
		speed = vitesse;
	}

	public void stopPendule() {
        audioSource.Stop();
        canStart = false;
		stop = true;
	}

	void setPenduleInitialPositionGauche() {
		if (this.transform.rotation.eulerAngles.z < angleZInitial || this.transform.rotation.eulerAngles.z > angleZInitialInvert) {
			this.transform.Rotate (Vector3.forward * speed * Time.deltaTime);
			Invoke ("setPenduleInitialPositionGauche",0f);
		} else {
			transform.rotation = Quaternion.Euler(this.transform.rotation.x,angleY,angleZInitial);
			canStart = true;
		}
	}

	void setPenduleInitialPositionDroite() {
		if (this.transform.rotation.eulerAngles.z < angleZInitialInvert || this.transform.rotation.eulerAngles.z > angleZInitial) {
			this.transform.Rotate (Vector3.back * speed * Time.deltaTime);
			Invoke ("setPenduleInitialPositionDroite",0f);
		} else {
			transform.rotation = Quaternion.Euler(this.transform.rotation.x,angleY,angleZInitial);
			canStart = true;
		}
	}

	public void startPendule() {
		if (canStart) {
            audioSource.Play();
            stop = false;
		} else {
			Invoke ("startPendule",0f);
		}
	}

	IEnumerator tranquille() {
		yield return new WaitForSeconds(10f);
		stopPendule();
		yield return new WaitForSeconds(4f);
		startPendule();
	}
}
