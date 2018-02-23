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
	private bool firstRun;
	void Start () {
		glaceSol=GameObject.Find("GlaceSol");
		duration=GameObject.Find("PouvoirGlace").GetComponent<PrincessePouvoirGlace>().duration;
		glaceSol.SetActive(false);
		running=false;
		firstRun=true;
	}
	
	// Update is called once per frame
	void Update () {
		if(running){
			var tmp = glaceSol.transform.position;
			if(firstRun){
				tmp-=2*glaceSol.transform.right;
				firstRun=false;
			}
			
			tmp.y+=increment;
			Debug.Log(tmp.y);
			if(tmp.y>=maxHeight-0.1){
				running=false;
				//Add song explosion des glacons 
				//+ anim destruction ?? 
			}else{
				glaceSol.transform.position=tmp;
			}
		}
	}

	public void LaunchAnim(){
		running=true;
		glaceSol.SetActive(true);
		StartCoroutine(WaitForEndIcePower());
	}

	IEnumerator WaitForEndIcePower(){
		yield return new WaitForSeconds(duration);
		Debug.Log("Fin du pouvoir glace");
		var tmp = glaceSol.transform.position;
		tmp.y=0;
		glaceSol.transform.position=tmp;
		glaceSol.SetActive(false);
		
	}
}
