using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AffichageCinematique : MonoBehaviour {


	public UnityEngine.UI.Image BandeNoirHaut;
	public UnityEngine.UI.Image BandeNoirBas;

	public UnityEngine.UI.Text passable;


	public Vector3 BandeNoirHautFinal;
	public Vector3 BandeNoirBasFInal;

	public Vector3 velocity;
	public bool etatBandeNoir;

	// Use this for initialization
	void Start () {
	/*	velocity = Vector3.zero;
		BandeNoirBasFInal = BandeNoirBas.transform.position;
		BandeNoirHautFinal = BandeNoirHaut.transform.position;
		BandeNoirBasFInal.y += 40;
		BandeNoirHautFinal.y -= 40;*/

	}
	
	// Update is called once per frame
	void Update () {
	/*	if(etatBandeNoir){
			BandeNoirBas.transform.position = Vector3.SmoothDamp (BandeNoirBas.transform.position, BandeNoirBasFInal, ref velocity, 0.01f, 1000);
			BandeNoirHaut.transform.position = Vector3.SmoothDamp (BandeNoirHaut.transform.position, BandeNoirHautFinal, ref velocity, 0.01f, 1000);
		}*/
	}


	public void setActiveBandeNoir(bool etat){
		BandeNoirBas.gameObject.SetActive (etat);
		BandeNoirHaut.gameObject.SetActive (etat);

	}

	public void setActivePassable (bool active){
		Debug.Log ("PAS : " + active);
		passable.gameObject.SetActive (active);
	}
}

//448
