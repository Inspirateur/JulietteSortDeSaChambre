using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Poele : MonoBehaviour {

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	void OnTriggerEnter(Collider other){

		// if (attaqueReversEnCours) {
		Debug.Log(other.gameObject.name);
			// Debug.Log("REVERS");
            // if (other.tag.Equals ("Mob")) {
				
			// 	IA_Agent mobTouche = other.gameObject.GetComponent<IA_Agent> ();

			// 	if (!listeMobsTouches.Contains (mobTouche) && mobTouche.estEnVie()) {
					
			// 		listeMobsTouches.Add (mobTouche);

			// 		Vector3 hitPoint = other.ClosestPoint (this.transform.position);

			// 		mobTouche.subirDegats (degatschargeArmeActuelle, hitPoint);

            //         bool MobTouch = true;

            //         gameObject.GetComponent<ArmesParticulesEffect>().ParticulePlay(GameControl.control.ArmeCourante, hitPoint, MobTouch);
			// 	}
			// }
            if (other.tag.Equals ("Projectile")) {
				// Debug.Log("POELE TOUCHE");
				Projectile proj = other.gameObject.GetComponent<Projectile> ();
				if(! proj.ami){
					proj.renvoyer();
				}
				
			}
            // else if (other.tag.Equals("wall")) {

            //     Vector3 hitPoint = other.ClosestPoint(this.transform.position);
            //     bool MobTouch = false;
            //     gameObject.GetComponent<ArmesParticulesEffect>().ParticulePlay(GameControl.control.ArmeCourante, hitPoint, MobTouch);
            // }
        // }
	}
}
