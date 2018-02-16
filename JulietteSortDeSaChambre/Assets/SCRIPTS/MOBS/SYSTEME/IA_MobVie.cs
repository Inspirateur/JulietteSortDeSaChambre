using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IA_MobVie : MonoBehaviour {

	public int vieMax;
	public Transform hitEffect;
	protected int vieCourante;
	private IA_Agent agent;

	// Use this for initialization
	void Start () {
		vieCourante = vieMax;
		agent = GetComponent<IA_Agent> ();
	}

	// Update is called once per frame
	void Update () {
		
	}

	public void blesser(int degats, Vector3 hitPoint) {
		if (hitEffect != null && hitPoint != null) {
			Instantiate (hitEffect, hitPoint, hitEffect.transform.rotation);
		}

		vieCourante = Mathf.Max(vieCourante - degats, 0);
		Debug.Log ("PV : " + vieCourante);
		if (!estEnVie ()) {
			Debug.Log ("Et c'est la mort qui l'a assasiné");
			agent.mourir ();
		} else if(conditionEtatBlesse()) {
			agent.changerEtat (agent.etatEtreBlesseDefaut);
		}
	}

	public virtual bool conditionEtatBlesse(){
		return true;
	}

	public void blesserSansEtat(int degats, Vector3 hitPoint) {
		if (hitEffect != null && hitPoint != null) {
			Instantiate (hitEffect, hitPoint, hitEffect.transform.rotation);
		}

		vieCourante = Mathf.Max(vieCourante - degats, 0);
		Debug.Log ("PV : " + vieCourante);
		if (!estEnVie()) {
			agent.mourir ();
		}
	}

	public bool estEnVie() {
		return vieCourante > 0;
	}
}
