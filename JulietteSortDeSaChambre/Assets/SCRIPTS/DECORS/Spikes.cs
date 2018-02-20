using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spikes : MonoBehaviour {

	[Header("Temps avant la premiere action :")]
	public float TimeBeforeStart;

	[Header("Temps entre chaque action :")]
	public float TimeRepos;

	// Use this for initialization
	void Start () {
		StartCoroutine(WaitBeforeStart());
	}

	void Update() {
		if (gameObject.GetComponent<Animator> ().GetCurrentAnimatorStateInfo (0).IsName ("BeginRepos")) {
			StartCoroutine (WaitBeforeUp ());
		}
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
