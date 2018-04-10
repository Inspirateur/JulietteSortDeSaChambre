using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PrinceseMenu : MonoBehaviour {

	private float timer;
	public float minTimer;
	public float maxTimer;
	private SoundManager sm;
	public AudioClip[] CriPrince;
	public AudioClip[] Coups;
	public GameObject startPrince;
	public GameObject[] prince;
	public AudioClip PageSound;
	void Awake() {
		sm = GameObject.FindGameObjectWithTag("SoundManager").GetComponent<SoundManager>();
	}

	// Use this for initialization
	void Start () {
		ResetTimer();
	}
	
	// Update is called once per frame
	void Update () {
		if(Time.time > timer){
			GetComponent<Animator>().SetTrigger("Is");
			ResetTimer();
		}
	}

	private void ResetTimer() {
		timer = Time.time + Random.Range(minTimer,maxTimer);
	}

	public void bookOpen(){
		sm.playOneShot(PageSound);
	}

	public void PlayCoups() {
		sm.playOneShot(Coups[Random.Range(0,Coups.Length)], 0.4f);
		GameObject currentPrince = Instantiate(prince[Random.Range(0,prince.Length)],new Vector3(startPrince.transform.position.x, startPrince.transform.position.y, startPrince.transform.position.z), Quaternion.identity);
		currentPrince.GetComponent<Rigidbody>().AddForce(Vector3.left * Random.Range(9f,12f), ForceMode.Impulse);
		currentPrince.GetComponent<Rigidbody>().AddForce(Vector3.up * Random.Range(28f,32f), ForceMode.Impulse);
		currentPrince.GetComponent<Rigidbody>().AddForce(Vector3.forward * 4f, ForceMode.Impulse);
	}

	public void PlayCriPrince() {
		sm.playOneShot(CriPrince[Random.Range(0,CriPrince.Length)], 0.4f);
	}
}
