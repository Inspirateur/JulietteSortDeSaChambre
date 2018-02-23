using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GlaceSol : MonoBehaviour {

	// Use this for initialization
	private GameObject glaceSol;
	private GameObject VisuelPouvoir;
	public float increment;
	public float maxHeight;

	public bool running;
	public float duration;
	public bool firstRun;
	void Start () {
		glaceSol=GameObject.Find("GlaceSol");
		duration=GameObject.Find("PouvoirGlace").GetComponent<PrincessePouvoirGlace>().duration;
		VisuelPouvoir=GameObject.Find("VisuelPouvoir");
		//glaceSol.SetActive(false);
		running=false;
		firstRun=true;
	}
	
	// Update is called once per frame
	void Update () {
		if(running){
			var tmp = glaceSol.transform.position;
			if(firstRun){
				//tmp-=2*glaceSol.transform.right;
				firstRun=false;
			}
			
			tmp.y+=increment;
			Debug.Log(tmp.y);
			if(tmp.y>=(maxHeight-0.1)+VisuelPouvoir.transform.position.y){
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
		Debug.Log("lauch anim");
		StartCoroutine(WaitForEndIcePower());
	}

	IEnumerator WaitForEndIcePower(){
		yield return new WaitForSeconds(duration);
		Debug.Log("Fin du pouvoir glace");
		var tmp = glaceSol.transform.position;
		tmp.y=3.85f;
		glaceSol.transform.position=tmp;
		Debug.Log(glaceSol.transform.position);
		glaceSol.SetActive(false);
		
	}
}
