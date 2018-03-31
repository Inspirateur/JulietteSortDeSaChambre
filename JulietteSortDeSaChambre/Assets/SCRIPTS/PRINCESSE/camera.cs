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

	void Awake() {
		cible = GameObject.FindGameObjectWithTag ("FocusCamera");
		princesse = GameObject.FindGameObjectWithTag("Player");
		anim = princesse.GetComponent<Animator>();
		// skinPrincesse = GameObject.FindGameObjectWithTag ("PrincesseBody").GetComponent<SkinnedMeshRenderer>();

		// On place la caméra à son point de départ pour éviter un mauvais effet au démarrage du jeu

		this.horizontal = 180.0f;

		this.transform.position = cible.transform.position + Quaternion.Euler(vertical, horizontal, 0) * new Vector3 (0, 0, -distanceMax);

		this.fov = Camera.main.fieldOfView;

		this.cinematiqueEnCours = false;

		this.lookAtPoint = cible.transform.position;

		cinematiqueManager = GetComponent<CinematiqueManager> ();

		//playerPref
		sensibiliteManetteX=PlayerPrefs.GetFloat("sensibiliteManetteX",sensibiliteManetteX);
		sensibiliteManetteY=PlayerPrefs.GetFloat("sensibiliteManetteY",sensibiliteManetteY);

		sensibiliteSourisX=PlayerPrefs.GetFloat("sensibiliteSourisX",sensibiliteSourisX);
		sensibiliteSourisY=PlayerPrefs.GetFloat("sensibiliteSourisY",sensibiliteSourisY);
	}

	/* On utilise LateUpdate afin que tout les autres éléments de la scène
	 * est été mis à jour avant de positionner la caméra car sa position 
	 * dépend de celle de la princesse.
	 */
	void LateUpdate() {

		if(this.cinematiqueManager.isInCinematique){

		}
		else{
			this.gererCameraClassique();
		}
	}

	public void setCinematiqueEnCours(bool vraiSiUneCinematiqueEstEnCours){
		this.cinematiqueEnCours = vraiSiUneCinematiqueEstEnCours;
	}

	private void gererCameraClassique(){
		float distance = this.calculerDistanceFocusCamera();
		// mise à jour des entrées manettes et souris
		if(anim.GetBool("isPushing")){
			this.transform.position = cible.transform.position + princesse.transform.rotation * new Vector3 (0, 0, -distanceMax);
			this.transform.LookAt (this.cible.transform.position);
			princesse.GetComponent<Rigidbody>().constraints = RigidbodyConstraints.FreezeRotation;	
		}else{
			this.miseAJourInput();
			this.placerCamera(distance);
		}
		

		// on cache le curseur

		Cursor.visible = false;

		// on récupère la distance max à laquelle on peut placer la caméra de son point de focus

		


		// on place la caméra

		


	/* 
		// .... Transparence ....

		if (distance < distanceAvantTransparence) {
			float alpha = Mathf.Clamp(distance / (distanceAvantTransparence * 0.66f), 0.0f, 1.0f);
			for (int i=0; i<skinPrincesse.materials.Length; i++) {
				skinPrincesse.materials[i].color = new Color (skinPrincesse.materials[i].color.r, skinPrincesse.materials[i].color.g, skinPrincesse.materials[i].color.b, 0.0f);
			}
		} else {
			for (int i=0; i<skinPrincesse.materials.Length; i++) {
				skinPrincesse.materials[i].color = new Color (skinPrincesse.materials[i].color.r, skinPrincesse.materials[i].color.g, skinPrincesse.materials[i].color.b, 1.0f);
			}
		}
	*/
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
}
