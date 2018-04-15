using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnObjectFromMobs : MonoBehaviour {

	public GameObject ObjectSpawn;
	public bool spawn = true;
	private bool PremiereFois;
	private float theta;
	private IA_MobVie vie;

	// Use this for initialization
	void Start () {
		vie = GetComponent<IA_MobVie>() ;
		PremiereFois = false;
	}

	// Update is called once per frame
	void Update () {
		if (spawn) {	
			if (vie.estEnVie ()) {
				ObjectSpawn.SetActive (false);
				PremiereFois = true;
			} else if (!vie.estEnVie () && PremiereFois == true) {
				ObjectSpawn.transform.position = this.transform.position;
				this.GetComponent<BoxCollider> ().enabled = false;
				Destroy (GetComponent<Rigidbody> ());
				ObjectSpawn.SetActive (true);
				theta = Random.value * 360;
				ObjectSpawn.GetComponent<Rigidbody> ().AddForce (new Vector3 (Mathf.Cos (theta) * 200, 200, Mathf.Sin (theta) * 200));
				PremiereFois = false;
			}
		}
	}
}
