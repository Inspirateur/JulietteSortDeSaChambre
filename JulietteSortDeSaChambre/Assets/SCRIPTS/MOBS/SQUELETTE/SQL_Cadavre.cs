using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SQL_Cadavre : MonoBehaviour {

    public float delaiAvantDisparition;
	public AudioClip sonMort;
    public List<Rigidbody> listeParties;
    public float forceExplosionLaterale;
    public float forceExplosionVerticale;

	private float actualDelai;

	// Use this for initialization
	void Start()
	{
        GetComponent<SoundEntity> ().playOneShot(sonMort,1.0f);
		actualDelai = Time.time + delaiAvantDisparition;
        float demiForce = forceExplosionLaterale/2.0f;
        foreach(Rigidbody r in listeParties){
            r.AddForce(new Vector3(Random.value * forceExplosionLaterale - demiForce, (Random.value + 1.0f) * forceExplosionVerticale, Random.value * forceExplosionLaterale - demiForce));
        }
	}

	// Update is called once per frame
	void Update () {
		if(Time.time >= actualDelai){
			Destroy(this.gameObject);
		}
	}
}
