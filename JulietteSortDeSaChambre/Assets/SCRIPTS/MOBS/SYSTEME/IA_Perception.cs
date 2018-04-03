using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IA_Perception : MonoBehaviour {

	public float demiAngleVision;
	public float distanceVision;
	public float rayonAudition;
	public bool estAveugle;
	public bool estSourd;

	public bool aRepere(GameObject cible, float niveauAttention) {
		PrincesseVie princesseVie = cible.GetComponent<PrincesseVie>();
		if(princesseVie != null && !princesseVie.enVie()){
			return false;
		}
		Vector3 vecDistancePrincesse = cible.transform.position - this.transform.position;

		float distancePrincesse = vecDistancePrincesse.magnitude;

		if (!estSourd && distancePrincesse <= this.rayonAudition * niveauAttention) {
			return true;
		}

		if (!estAveugle) {
			RaycastHit hitInfo;

			Physics.Raycast (this.transform.position, vecDistancePrincesse.normalized, out hitInfo);

			if (!hitInfo.collider.gameObject.Equals (cible)) {
				return false;
			}

			float angle = Vector3.Angle (this.transform.forward, vecDistancePrincesse.normalized);

			if (angle <= this.demiAngleVision * niveauAttention) {

				if (hitInfo.distance <= this.distanceVision) {
					return true;
				}
			}
		}
		return false;
	}
}
