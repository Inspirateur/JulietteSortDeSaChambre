using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.EventSystems;

public class AffichageMort : MonoBehaviour, ISelectHandler, IDeselectHandler {

	private Transform[] listTransform;

	public UnityEngine.UI.Button button;

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
		// var truc = gameObject.GetComponentsInChildren<UnityEngine.UI.Button>()[0];
		// //Debug.Log(truc.name);
		// truc.Select();
		button.Select();
    }

	public void resetScene(){
        for (int i = 1; i < listTransform.Length; i++) {
            listTransform[i].gameObject.SetActive(false);
        }
        CheckPointManager.getInstance().restartCheckPoint();
    }

	public void retourMenuPrincipal(){
		PlayerPrefs.SetString("SceneToLoad", "SCENES/MenuPrincipal");
        SceneManager.LoadScene("SCENES/LoadingScene");
		Debug.Log("quitter");
	}

	public void OnSelect(BaseEventData eventData){
		Debug.Log(eventData);
	}

	public void OnDeselect(BaseEventData eventData){

	}

}
