using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IA_Perception : MonoBehaviour {

	public float demiAngleVision;
	public float distanceVision;
	public float rayonAudition;

	public bool aReperer(GameObject cible, float niveauAttention) {
		Vector3 vecDistancePrincesse = cible.transform.position - this.transform.position;

		float distancePrincesse = vecDistancePrincesse.magnitude;

		if (distancePrincesse <= this.rayonAudition * niveauAttention) {
			return true;
		}

		RaycastHit hitInfo;

		Physics.Raycast(this.transform.position, vecDistancePrincesse.normalized, out hitInfo);

		if ( ! hitInfo.collider.gameObject.Equals(cible)) {
			return false;
		}

		float angle = Vector3.Angle (this.transform.forward, vecDistancePrincesse.normalized);

		if(angle <= this.demiAngleVision * niveauAttention) {

			if (hitInfo.distance <= this.distanceVision) {
				return true;
			}
		}

		return false;
	}
}
