using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AffichagePouvoir : MonoBehaviour {


	private Dictionary<EnumPouvoir,GameObject> dicoPouvoirGo;
	private Dictionary<EnumPouvoir,bool> dicoPouvoirActive;
	private Dictionary<EnumPouvoir,PrincessePouvoir> dicoPouvoir;


	// Use this for initialization
	void Start () {
		dicoPouvoirGo = new Dictionary<EnumPouvoir, GameObject> ();
		dicoPouvoirActive =new Dictionary<EnumPouvoir, bool> ();
		dicoPouvoir =new Dictionary<EnumPouvoir, PrincessePouvoir> ();

		foreach (EnumPouvoirHUD enu in GetComponentsInChildren<EnumPouvoirHUD>(true)) {
			dicoPouvoirGo.Add (enu.pouvoir, enu.gameObject);
			dicoPouvoirActive.Add (enu.pouvoir, false);
		}
		dicoPouvoir.Add(EnumPouvoir.pouvoirGlace,GameObject.FindGameObjectWithTag ("Player").GetComponentInChildren<PrincessePouvoirGlace> (true));
	}
	
	// Update is called once per frame
	void Update () {
		if (InputManager.GetButtonDown ("pouvoirGlace")) {
			dicoPouvoirGo [EnumPouvoir.pouvoirGlace].transform.GetChild (0).gameObject.SetActive(true);
			dicoPouvoirActive [EnumPouvoir.pouvoirGlace] = true;
			StartCoroutine (pouvoirTimer (dicoPouvoir [EnumPouvoir.pouvoirGlace],dicoPouvoirGo [EnumPouvoir.pouvoirGlace].transform.GetChild (0).gameObject));
		}


	}


	IEnumerator pouvoirTimer(PrincessePouvoir pouvoir,GameObject go){

		for(var i=0.0f;i<120.0f;i++){
			Debug.Log (i / 120.0f);
			go.GetComponent<UnityEngine.UI.Image> ().fillAmount = (i/120.0f);
			yield return new WaitForSeconds(pouvoir.cooldown/120);
		}
		go.SetActive (false);
	}



}


