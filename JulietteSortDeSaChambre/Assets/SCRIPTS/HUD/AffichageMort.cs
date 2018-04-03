using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AffichageMort : MonoBehaviour {

	private Transform[] listTransform;

	public void Start(){
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
        for (int i = 1; i < listTransform.Length; i++) {
            listTransform[i].gameObject.SetActive(false);
        }
        CheckPointManager.getInstance().restartCheckPoint();
    }

}
