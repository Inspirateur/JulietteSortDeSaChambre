using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class camera : MonoBehaviour {

	[Header("Paramètres généraux")]
	public float distanceMax;

	[Header("Sensibilités manette")]

    public float sensibiliteManetteX;
    public float sensibiliteManetteY;
	public float inputMinimumManette;

	[Header("Sensibilités souris")]

    public float sensibiliteSourisX;
    public float sensibiliteSourisY;

	private const float ANGLE_MIN_Y = -3.0f;
	private const float ANGLE_MAX_Y = 80.0f;
	private GameObject cible;
	private Vector3 velocity = Vector3.zero;
	private GameObject princesse;
	private float horizontal;
	private float vertical;

	void Awake() {
		cible = GameObject.FindGameObjectWithTag ("FocusCamera");
		princesse = GameObject.FindGameObjectWithTag("Player");
		// skinPrincesse = GameObject.FindGameObjectWithTag ("PrincesseBody").GetComponent<SkinnedMeshRenderer>();

		// On place la caméra à son point de départ pour éviter un mauvais effet au démarrage du jeu

		this.horizontal = 180.0f;

		this.transform.position = cible.transform.position + Quaternion.Euler(vertical, horizontal, 0) * new Vector3 (0, 0, -distanceMax);
	}

	/* On utilise LateUpdate afin que tout les autres éléments de la scène
	 * est été mis à jour avant de positionner la caméra car sa position 
	 * dépend de celle de la princesse.
	 */
	void LateUpdate() {

		// mise à jour des entrées manettes et souris

		this.miseAJourInput();

		// on cache le curseur

		Cursor.visible = false;

		// on récupère la distance max à laquelle on peut placer la caméra de son point de focus

		float distance = this.calculerDistanceFocusCamera();

		// on place la caméra

		this.placerCamera(distance);


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

			horizontal += ((InputManager.GetKeyAxis("Mouse X") - this.inputMinimumManette) / (1.0f - this.inputMinimumManette)) * sensibiliteManetteX;

		} else if(InputManager.GetKeyAxis("Joystick X") < - this.inputMinimumManette) {

			horizontal += ((InputManager.GetKeyAxis("Mouse X") + this.inputMinimumManette) / (1.0f - this.inputMinimumManette)) * sensibiliteManetteX;

		}

		if (InputManager.GetKeyAxis("Joystick Y") > this.inputMinimumManette){

			vertical += ((InputManager.GetKeyAxis("Mouse Y") - this.inputMinimumManette) / (1.0f - this.inputMinimumManette)) * sensibiliteManetteY;
			
		} else if (InputManager.GetKeyAxis("Joystick Y") < -this.inputMinimumManette){

			vertical += ((InputManager.GetKeyAxis("Mouse Y") + this.inputMinimumManette) / (1.0f - this.inputMinimumManette)) * sensibiliteManetteY;
			
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

		Vector3 temp = Vector3.SmoothDamp(this.transform.position, cible.transform.position + rotation * dir, ref velocity, 0.15f);

		temp.y = (cible.transform.position + rotation * dir).y;

		this.transform.position = temp;

		this.transform.LookAt (cible.transform.position);
	}
}
