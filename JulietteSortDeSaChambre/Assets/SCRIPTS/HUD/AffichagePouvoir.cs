using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AffichagePouvoir : MonoBehaviour {


	private Dictionary<EnumPouvoir,GameObject> dicoPouvoirGo;
	private Dictionary<EnumPouvoir,PrincessePouvoir> dicoPouvoir;

	public float divideAngle;

	private bool visible;



	// Use this for initialization
	void Start () {
		dicoPouvoirGo = new Dictionary<EnumPouvoir, GameObject> ();
		dicoPouvoir =new Dictionary<EnumPouvoir, PrincessePouvoir> ();

		foreach (EnumPouvoirHUD enu in GetComponentsInChildren<EnumPouvoirHUD>(true)) {
			dicoPouvoirGo.Add (enu.pouvoir, enu.gameObject);
		}
		dicoPouvoir.Add(EnumPouvoir.pouvoirGlace,GameObject.FindGameObjectWithTag ("Player").GetComponentInChildren<PrincessePouvoirGlace> (true));
	}
	
	// Update is called once per frame
	void Update () {
		if (visible) {
			if (InputManager.GetButtonDown ("pouvoirGlace") && !(dicoPouvoirGo [EnumPouvoir.pouvoirGlace].transform.GetChild (0).gameObject.active)) {
				dicoPouvoirGo [EnumPouvoir.pouvoirGlace].transform.GetChild (0).gameObject.SetActive(true);
				StartCoroutine (pouvoirTimer (dicoPouvoir [EnumPouvoir.pouvoirGlace],dicoPouvoirGo [EnumPouvoir.pouvoirGlace].transform.GetChild (0).gameObject));
			}
		}
	}


	IEnumerator pouvoirTimer(PrincessePouvoir pouvoir,GameObject go){

		for(var i=1.0f;i<divideAngle+1;i++){
			go.GetComponent<UnityEngine.UI.Image> ().fillAmount = (1-(i/divideAngle));
			yield return new WaitForSeconds(pouvoir.cooldown/divideAngle);
		}
		go.SetActive (false);
	}

	public void setVisible(bool visible){
		visible = true;
	}


}


