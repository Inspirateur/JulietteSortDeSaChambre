using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AffichagePouvoir : MonoBehaviour {

	public UnityEngine.UI.Image fondPouvoir;
	private Dictionary<EnumPouvoir,GameObject> dicoPouvoirGo;
	private Dictionary<EnumPouvoir,PrincessePouvoir> dicoPouvoir;

	public float divideAngle;
	private bool activePouvoir;

	private float debut;


	// Use this for initialization
	void Start () {
		activePouvoir = false;
		dicoPouvoirGo = new Dictionary<EnumPouvoir, GameObject> ();
		dicoPouvoir =new Dictionary<EnumPouvoir, PrincessePouvoir> ();

		foreach (EnumPouvoirHUD enu in GetComponentsInChildren<EnumPouvoirHUD>(true)) {
			dicoPouvoirGo.Add (enu.pouvoir, enu.gameObject);
		}
		dicoPouvoir.Add(EnumPouvoir.pouvoirGlace,GameObject.FindGameObjectWithTag ("Player").GetComponentInChildren<PrincessePouvoirGlace> (true));

	}
	
	// Update is called once per frame
	void Update () {
		if (dicoPouvoir[EnumPouvoir.pouvoirGlace].isUnlocked &&
			InputManager.GetButtonDown ("pouvoirGlace") && 
			!(dicoPouvoirGo [EnumPouvoir.pouvoirGlace].transform.GetChild (0).gameObject.active)) 
		{
			dicoPouvoirGo [EnumPouvoir.pouvoirGlace].transform.GetChild (0).gameObject.SetActive(true);
			debut = Time.time;
			activePouvoir = true;
			//StartCoroutine (pouvoirTimer (dicoPouvoir [EnumPouvoir.pouvoirGlace],dicoPouvoirGo [EnumPouvoir.pouvoirGlace].transform.GetChild (0).gameObject));
		}

		if (activePouvoir) {
			if ((debut + dicoPouvoir [EnumPouvoir.pouvoirGlace].cooldown)+0.01f > Time.time) {
				Debug.Log (1 - ((Time.time - debut) / (dicoPouvoir [EnumPouvoir.pouvoirGlace].cooldown)));
				dicoPouvoirGo [EnumPouvoir.pouvoirGlace].transform.GetChild (0).gameObject.GetComponent<UnityEngine.UI.Image> ().fillAmount = (
				    1 - ((Time.time - debut) / (dicoPouvoir [EnumPouvoir.pouvoirGlace].cooldown))
				);
			} else {
				activePouvoir = false;
				dicoPouvoirGo [EnumPouvoir.pouvoirGlace].transform.GetChild (0).gameObject.SetActive (false);
			}
		}
	}


	public void setVisible(EnumPouvoir pouvoir){
		fondPouvoir.gameObject.SetActive (true);
		dicoPouvoirGo [pouvoir].gameObject.SetActive (true);
	}


}


