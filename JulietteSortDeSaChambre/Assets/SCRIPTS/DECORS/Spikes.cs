using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spikes : MonoBehaviour {

	[Header("Temps avant la premiere action :")]
	public float TimeBeforeStart;

	[Header("Temps entre chaque action :")]
	public float TimeRepos;

	private bool StopSpike;

	// Use this for initialization
	void Start () {
		StopSpike = false;
		StartCoroutine(WaitBeforeStart());
	}

	void Update() {
		if (gameObject.GetComponent<Animator> ().GetCurrentAnimatorStateInfo (0).IsName ("BeginRepos") && !StopSpike) {
			StartCoroutine (WaitBeforeUp ());
		}
	}

	void StopSpikes() {
		gameObject.GetComponent<Animator> ().SetBool ("CanUp", false);
		StopSpike = true;
	}

	void StartSpikes() {
		StopSpike = false;
	}

	IEnumerator WaitBeforeStart()
	{
		yield return new WaitForSeconds(TimeBeforeStart);
		gameObject.GetComponent<Animator> ().SetBool ("CanUp", true);
	}

	IEnumerator WaitBeforeUp()
	{
		gameObject.GetComponent<Animator> ().SetBool ("CanUp", false);
		yield return new WaitForSeconds(TimeRepos);
		gameObject.GetComponent<Animator> ().SetBool ("CanUp", true);
	}
}
