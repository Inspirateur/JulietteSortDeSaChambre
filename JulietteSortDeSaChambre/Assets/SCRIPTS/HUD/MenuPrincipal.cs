using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using UnityEngine.EventSystems;// Required when using Event data.

public class MenuPrincipal : MonoBehaviour {

	[Header("Nom des Scenes :")]
	public string NomDeLaSceneTuto;
    public string NomDeLaSceneCredit;
    public string NomDeLaSceneChargement;

    [Header("Pannel Menu :")]
    public GameObject AffichePanelMenuPrincipal;

    [Header("Pannel Controle :")]
    public GameObject AffichePanelControle;

	[Header("Bouton Controle :")]
	public GameObject BouttonControle;

	[Header("Bouton Controle Retour :")]
	public GameObject BouttonControleRetour;
	private bool InControlePanel;

	[HideInInspector]
    public SoundEntity se;

    [Header("Son :")]
    public AudioClip TicSound;

    public GameObject Princesse;

	public AudioClip PageSound;

	void Awake() {
        se = GetComponent<SoundEntity>();

        AffichePanelControle.SetActive(false);
        AffichePanelMenuPrincipal.SetActive(true);
		InControlePanel = false;

		Cursor.visible = true;

		PlayerPrefs.SetString("SceneToLoad", null);
    }

    void Update() {
		if (InControlePanel) {
			EventSystem.current.SetSelectedGameObject(BouttonControleRetour);
		}
    }

	public void ChangeButton() {
		se.playOneShot(TicSound, 0.5f);
	}

	public void RetourMenuPrincipal() {
		InControlePanel = false;
		Princesse.GetComponent<Animator>().SetBool("Controle", false);
		Princesse.GetComponent<Animator>().SetTrigger("RangeBook");
		se.playOneShot(TicSound);
        AffichePanelMenuPrincipal.SetActive(true);
        AffichePanelControle.SetActive(false);
		EventSystem.current.SetSelectedGameObject(BouttonControle);
	}

    public void LancerPartie() {
        AffichePanelMenuPrincipal.SetActive(false);
        PlayerPrefs.SetString("SceneToLoad", NomDeLaSceneTuto);
        SceneManager.LoadScene(NomDeLaSceneChargement);
    }

    public void LanceCredit() {
        PlayerPrefs.SetString("SceneToLoad", NomDeLaSceneCredit);
        SceneManager.LoadScene(NomDeLaSceneChargement);
    }

    public void LanceControle() {
		InControlePanel = true;
		Princesse.GetComponent<Animator>().SetBool("Controle", true);
        Princesse.GetComponent<Animator>().SetTrigger("IsBook");
		se.playOneShot(PageSound);
		AffichePanelMenuPrincipal.SetActive(false);
        AffichePanelControle.SetActive(true);
		EventSystem.current.SetSelectedGameObject(BouttonControleRetour);
	}

    public void QuiterJeu() {
        Application.Quit();
        #if UNITY_EDITOR
            UnityEditor.EditorApplication.isPlaying = false;
        #endif
    }

}

