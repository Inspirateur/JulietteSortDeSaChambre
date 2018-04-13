using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class AffichageMort : MonoBehaviour {

	private Transform[] listTransform;

	private GameObject princesse;
	// public UnityEngine.UI.Button button;

	public GameObject boutonFocus;

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
		// var truc = gameObject.GetComponentsInChildren<UnityEngine.UI.Button>()[0];
		// //Debug.Log(truc.name);
		// truc.Select();
		// button.Select();
		EventSystem.current.SetSelectedGameObject(boutonFocus);
		Cursor.visible = true;
    }

	public void resetScene(){
		princesse.GetComponent<Animator>().SetBool("IsDead", false);
		princesse.GetComponent<PrincesseDeplacement>().UnlockPrincesse();
		princesse.GetComponent<PrincesseVie>().PlayOneTimeDie = false;
		for (int i = 1; i < listTransform.Length; i++) {
            listTransform[i].gameObject.SetActive(false);
        }
        CheckPointManager.getInstance().restartCheckPoint();
		Cursor.visible = false;
    }

	public void retourMenuPrincipal(){
		Cursor.visible = false;
		PlayerPrefs.SetString("SceneToLoad", "SCENES/MenuPrincipal");
        SceneManager.LoadScene("SCENES/LoadingScene");
	}

	public void ChangeColorOnSelect(){
		var color = new Color32(180, 180, 180, 255);
		GetComponentInChildren<Text>().color = color;
	}

	public void ChangeColoOnDeselect(){
		var color = new Color32(255, 255, 255, 255);
		GetComponentInChildren<Text>().color = color;
	}
	
}
