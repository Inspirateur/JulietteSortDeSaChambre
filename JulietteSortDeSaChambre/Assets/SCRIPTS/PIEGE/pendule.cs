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

	[Header("Temps de demarage :")]
	public float TimeBegin = 0;

	private float ActivationTime;

	[Header("Reset Position Gauche ? (default = Droite) :")]
	public bool PositionResetGauche;

	[Header("Vitesse :")]
	public float speed = 1.5f;
	private bool stop;
	private bool canStart;

    private AudioSource audioSource;

    [Header("La lame peut s'arreter :")]
    public bool canStop = true;

	void Start () {
        audioSource = GetComponentInChildren<AudioSource> ();
        angleZInitial = this.transform.rotation.eulerAngles.z;
		angleZInitialInvert = 360 - angleZInitial;
		ActivationTime = Time.time + TimeBegin;
		// StartCoroutine(tranquille());
	}

	void Update (){
		if (!stop && Time.time > ActivationTime) 
		{
			if (PositionResetGauche)
			{
                RotationDroite();
			} else
			{
                RotationGauche();
			}
		} else {
			if (PositionResetGauche)
			{
                SetPenduleInitialPositionGauche();
			} else
			{
                SetPenduleInitialPositionDroite();
			}
		}
	}

    private void RotationGauche() {
		transform.rotation = Quaternion.Lerp (
			Quaternion.Euler(this.transform.rotation.x,angleY,-angleZ), 
			Quaternion.Euler(this.transform.rotation.x,angleY,angleZ),
			(Mathf.Sin(Time.time * speed + Mathf.PI/2) + 1.0f)/ 2.0f
		);
	}

    private void RotationDroite() {
		transform.rotation = Quaternion.Lerp (
			Quaternion.Euler(this.transform.rotation.x,angleY,angleZ), 
			Quaternion.Euler(this.transform.rotation.x,angleY,-angleZ),
			(Mathf.Sin(Time.time * speed + Mathf.PI/2) + 1.0f)/ 2.0f
		);
	}

	public void SetVitesse(float vitesse) {
		speed = vitesse;
	}

	public void StopPendule() {
        audioSource.Stop();
        canStart = false;
		stop = true;
	}

    private void SetPenduleInitialPositionGauche() {
		if (this.transform.rotation.eulerAngles.z < angleZInitial || this.transform.rotation.eulerAngles.z > angleZInitialInvert) {
			this.transform.Rotate (Vector3.forward * speed * Time.deltaTime);
		} else {
			transform.rotation = Quaternion.Euler(this.transform.rotation.x,angleY,angleZInitial);
			canStart = true;
		}
	}

    private void SetPenduleInitialPositionDroite() {
		if (this.transform.rotation.eulerAngles.z < angleZInitialInvert || this.transform.rotation.eulerAngles.z > angleZInitial) {
			this.transform.Rotate (Vector3.back * speed * Time.deltaTime);
		} else {
			transform.rotation = Quaternion.Euler(this.transform.rotation.x,angleY,angleZInitial);
			canStart = true;
		}
	}

	public void StartPendule() {
		if (canStart) {
            audioSource.Play();
            stop = false;
		} else {
			Invoke ("StartPendule", 0f);
		}
	}

    public void ActiveCannotStop()
    {

    }
}
