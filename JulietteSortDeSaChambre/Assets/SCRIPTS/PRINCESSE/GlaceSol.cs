using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GlaceSol : MonoBehaviour {

	// Use this for initialization
	private GameObject glaceSol;
	public float increment;
	public float maxHeight;

	private bool running;
	private float duration;
	void Start () {
		glaceSol=GameObject.Find("GlaceSol");
		duration=GameObject.Find("Juliette").GetComponent<PrincessePouvoirGlace>().duration;
		running=false;
	}
	
	// Update is called once per frame
	void Update () {
		if(running){
			var tmp = glaceSol.transform.position;
			tmp.y+=increment;
			if(tmp.y>maxHeight){
				running=false;
				//Add song explosion des glacons 
				//+ anim destruction ?? 
				tmp.y=0;
			}
			glaceSol.transform.position=tmp;
		}
	}

	public void LaunchAnim(){
		running=true;
		glaceSol.SetActive(true);
	}

	IEnumerator WaitForEndIcePower(){
		yield return new WaitForSeconds(duration);
		glaceSol.SetActive(false);
		var tmp = glaceSol.transform.position;
		tmp.y=0;
	}
}
