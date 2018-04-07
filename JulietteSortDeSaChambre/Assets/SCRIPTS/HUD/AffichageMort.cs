using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AffichageMort : MonoBehaviour {

	private Transform[] listTransform;

	private GameObject princesse;

	public void Start(){
		princesse = GameObject.FindGameObjectWithTag("Player");
		listTransform = gameObject.GetComponentsInChildren<Transform>(true);
	}

	public void Update(){

	}

	public void afficheMort(){
        /*Time.timeScale = 0;
		foreach(Transform t in listTransform){
			t.gameObject.SetActive (true);
		}*/
        for (int i = 1; i < listTransform.Length; i++) {
            listTransform[i].gameObject.SetActive(true);
        }
    }

	public void resetScene(){
		princesse.GetComponent<Animator>().SetBool("IsDead", false);
		princesse.GetComponent<PrincesseDeplacement>().UnlockPrincesse();
		for (int i = 1; i < listTransform.Length; i++) {
            listTransform[i].gameObject.SetActive(false);
        }
        CheckPointManager.getInstance().restartCheckPoint();
    }

}
