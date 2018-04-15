using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class camera : MonoBehaviour {

	[Header("Paramètres généraux")]
	public float distanceMax;
	public float smooth;
	public float facteurZoom;

	[Header("Sensibilités manette")]

    public float sensibiliteManetteX;
    public float sensibiliteManetteY;
	public float inputMinimumManette;

	[Header("Sensibilités souris")]

    public float sensibiliteSourisX;
    public float sensibiliteSourisY;

	private float ANGLE_MIN_Y = -3.0f;
	private float ANGLE_MAX_Y = 80.0f;
	private GameObject cible;
	private Vector3 velocity = Vector3.zero;
	private GameObject princesse;
	private float horizontal;
	private float vertical;
	private float fov;
	private float velocityFOV = 0.0f;
	private Vector3 lookAtPoint;
	private Vector3 velocityLookAtPoint = Vector3.zero;
	private bool cinematiqueEnCours;
	private Animator anim;

	private CinematiqueManager cinematiqueManager;
	public float shakeDuration ;
	public float shakeAmount ;
	public float decreaseFactor;
	private float shakeDurationTemp ;
	private float shakeAmountTemp ;
	private float decreaseFactorTemp;
	public bool shakingIsActive;
	private Vector3 originalPos;

	public bool active;

	void Awake() {
		active = true;
		cible = GameObject.FindGameObjectWithTag ("FocusCamera");
		princesse = GameObject.FindGameObjectWithTag("Player");
		anim = princesse.GetComponent<Animator>();
		// skinPrincesse = GameObject.FindGameObjectWithTag ("PrincesseBody").GetComponent<SkinnedMeshRenderer>();

		// On place la caméra à son point de départ pour éviter un mauvais effet au démarrage du jeu

		centrerCamera();

		this.fov = Camera.main.fieldOfView;

		// this.cinematiqueEnCours = false;

		cinematiqueManager = GetComponent<CinematiqueManager> ();
		shakeDurationTemp = shakeDuration;
		shakeAmountTemp = shakeAmount;
		decreaseFactorTemp = decreaseFactor;
		
		//playerPref
		sensibiliteManetteX=PlayerPrefs.GetFloat("sensibiliteManetteX",sensibiliteManetteX);
		sensibiliteManetteY=PlayerPrefs.GetFloat("sensibiliteManetteY",sensibiliteManetteY);

		sensibiliteSourisX=PlayerPrefs.GetFloat("sensibiliteSourisX",sensibiliteSourisX);
		sensibiliteSourisY=PlayerPrefs.GetFloat("sensibiliteSourisY",sensibiliteSourisY);

		Cursor.visible=false;
	}

	/* On utilise LateUpdate afin que tout les autres éléments de la scène
	 * est été mis à jour avant de positionner la caméra car sa position 
	 * dépend de celle de la princesse.
	 */



	void LateUpdate() {
		if(active){
			if(this.cinematiqueManager.isInCinematique){
				if (shakingIsActive) {
					shaking ();
				}
			}
			else{
				this.gererCameraClassique();
			}

		}



	}

	public void activeShaking(){
		originalPos = GetComponent<Transform> ().localPosition;
		shakeDuration = shakeDurationTemp;
		shakeAmount = shakeAmountTemp;
		decreaseFactor = decreaseFactorTemp;
		shakingIsActive = true;
	}

	private void shaking(){
		if (shakeDuration > 0f) {

			GetComponent<Transform> ().localPosition = originalPos + Random.insideUnitSphere * shakeAmount;

			shakeDuration -= Time.deltaTime;
		} else if (decreaseFactor > 0f) {
			decreaseFactor -= Time.deltaTime;

			GetComponent<Transform> ().localPosition = originalPos + Random.insideUnitSphere * shakeAmount * decreaseFactor;


		} else {
			GetComponent<Transform> ().localPosition = originalPos;
			shakingIsActive = false;

		}
	}

	// public void setCinematiqueEnCours(bool vraiSiUneCinematiqueEstEnCours){
	// 	this.cinematiqueEnCours = vraiSiUneCinematiqueEstEnCours;
	// }

	private void gererCameraClassique(){
		if(anim.GetBool("isPushing")){
			this.transform.position = cible.transform.position + princesse.transform.rotation * new Vector3 (0, 0, -distanceMax);
			this.transform.LookAt (this.cible.transform.position);
			princesse.GetComponent<Rigidbody>().constraints = RigidbodyConstraints.FreezeRotation;	
		}else{
			float distance = this.calculerDistanceFocusCamera();
			// mise à jour des entrées manettes et souris
			this.miseAJourInput();
			this.placerCamera(distance);
		}
		

		// on cache le curseur

		//Cursor.visible = false;
	}

	private void miseAJourInput(){

		if (this.vraiSiManetteBranchee()){ // manette

			this.miseAJourManette();
		}
		else { // souris

			this.miseAJourSouris();
		}

		// bornage des valeurs

		horizontal = horizontal % 360.0f;
		vertical = Mathf.Clamp(vertical, ANGLE_MIN_Y, ANGLE_MAX_Y);
	}

	private bool vraiSiManetteBranchee(){
		return Input.GetJoystickNames().Length > 0;
	}

	private void miseAJourManette(){

		if (InputManager.GetKeyAxis("Joystick X") > this.inputMinimumManette){

			horizontal += ((InputManager.GetKeyAxis("Joystick X") - this.inputMinimumManette) / (1.0f - this.inputMinimumManette)) * sensibiliteManetteX;

		} else if(InputManager.GetKeyAxis("Joystick X") < - this.inputMinimumManette) {

			horizontal += ((InputManager.GetKeyAxis("Joystick X") + this.inputMinimumManette) / (1.0f - this.inputMinimumManette)) * sensibiliteManetteX;

		}

		if (InputManager.GetKeyAxis("Joystick Y") > this.inputMinimumManette){

			vertical += ((InputManager.GetKeyAxis("Joystick Y") - this.inputMinimumManette) / (1.0f - this.inputMinimumManette)) * sensibiliteManetteY;
			
		} else if (InputManager.GetKeyAxis("Joystick Y") < -this.inputMinimumManette){

			vertical += ((InputManager.GetKeyAxis("Joystick Y") + this.inputMinimumManette) / (1.0f - this.inputMinimumManette)) * sensibiliteManetteY;
			
		}
	}

	private void miseAJourSouris(){

		horizontal += InputManager.GetKeyAxis("Mouse X") * sensibiliteSourisX;
		vertical += InputManager.GetKeyAxis("Mouse Y") * sensibiliteSourisY;
	}

	private float calculerDistanceFocusCamera(){

		Vector3 direction = this.transform.position - cible.transform.position;

		RaycastHit[] hitInfos = Physics.RaycastAll(cible.transform.position, direction, this.distanceMax);

		float distance = this.distanceMax;

		foreach (RaycastHit info in hitInfos){
			if(!info.collider.gameObject.Equals(princesse)){
				distance = Mathf.Min(info.distance, distance);
			}
		}

		return distance;
	}

	private void placerCamera(float distance){

		Vector3 dir = new Vector3 (0, 0, - distance);

		Quaternion rotation = Quaternion.Euler(this.vertical, this.horizontal, 0);

		Vector3 temp = Vector3.SmoothDamp(this.transform.position, cible.transform.position + rotation * dir, ref velocity, this.smooth);

		temp.y = (cible.transform.position + rotation * dir).y;

		this.transform.position = temp;

		this.lookAtPoint = Vector3.SmoothDamp(this.lookAtPoint, cible.transform.position, ref velocityLookAtPoint, this.smooth);

		this.lookAtPoint.y = cible.transform.position.y;

		this.transform.LookAt (this.lookAtPoint);


		Camera.main.fieldOfView = Mathf.SmoothDamp(Camera.main.fieldOfView, fov, ref velocityFOV, this.smooth);
	}

	public void zoomer(){
		this.fov /= this.facteurZoom;
		this.distanceMax /= this.facteurZoom;
		this.ANGLE_MIN_Y *= 10.0f * this.facteurZoom;
		this.sensibiliteSourisX /= this.facteurZoom;
		this.sensibiliteSourisY /= this.facteurZoom;
		this.sensibiliteManetteX /= this.facteurZoom;
		this.sensibiliteManetteY /= this.facteurZoom;
	}

	public void dezoomer(){
		this.fov *= this.facteurZoom;
		this.distanceMax *= this.facteurZoom;
		this.ANGLE_MIN_Y /= 10.0f * this.facteurZoom;
		this.sensibiliteSourisX *= this.facteurZoom;
		this.sensibiliteSourisY *= this.facteurZoom;
		this.sensibiliteManetteX *= this.facteurZoom;
		this.sensibiliteManetteY *= this.facteurZoom;
	}

	public void centrerCamera(){
		this.transform.position = cible.transform.position + cible.transform.rotation * new Vector3 (0, 0, -distanceMax);

		this.lookAtPoint = cible.transform.position;

		this.transform.LookAt (this.lookAtPoint);

		Vector3 v = this.transform.rotation.eulerAngles;
		this.vertical = v.x;
		this.horizontal = v.y;

		this.velocity = Vector3.zero;
		this.velocityLookAtPoint = Vector3.zero;
	}
}
