using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spikes : MonoBehaviour {
	public float TimeBeforeStart;
	private bool CanUp;
	// Use this for initialization
	void Start () {

		StartCoroutine(WaitBeforeStart());

	}
	
	// Update is called once per frame
	void Update () {
		if (CanUp) {
			gameObject.GetComponent<Animator> ().SetBool ("CanUp", true);
		}
	}
	IEnumerator WaitBeforeStart()
	{
		yield return new WaitForSeconds(TimeBeforeStart);
		CanUp = true;
	}
}
