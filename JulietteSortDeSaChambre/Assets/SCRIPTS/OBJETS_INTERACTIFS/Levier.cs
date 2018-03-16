using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Levier : ObjetEnvironnemental {

	private  Animator anim;
	private bool active;

	public AudioClip LevierActivation;

	public List<Evenement> listEvenement;

	public bool isReactivable;

	private EventManager eventManager;



	// Use this for initialization
	void Start () {
		anim = GetComponent<Animator>();
		active = false;
		eventManager = GetComponent<EventManager> ();
	}
	
	// Update is called once per frame
	void Update () {
		//Debug.Log (anim.GetCurrentAnimatorStateInfo (0).IsName ("levierMonte"));
	}

	public override void Activation(){
		if(isActivable()){
			if (active) {
				if (isReactivable) {
					active = false;
					sm.playOneShot(LevierActivation);
					anim.SetBool("isDown", true);
					anim.SetBool("isUp", false);

					eventManager.activation ();

					/*foreach (Evenement e in listEvenement) {
						e.desactivation ();
					}*/

				}
			} else {
				
				active = true;
				sm.playOneShot(LevierActivation);
				anim.SetBool("isDown", false);
				anim.SetBool("isUp", true);


				foreach (EventManager eM in GetComponents<EventManager>()) {
					if (eM.nomEvent.Equals ("action")) {
						eM.activation ();
					}
				}


				/*foreach (Evenement e in listEvenement) {
					e.activation ();
				}*/


			}
		}
	}

	public bool isActivable(){
		if (anim.GetCurrentAnimatorStateInfo (0).IsName ("levierMonte")) {
			return false;
		}
		if (anim.GetCurrentAnimatorStateInfo (0).IsName ("levierdescend")) {
			return false;
		}
		return  true;

	}

	override
	public EnumIconeInterraction getIconeInteraction(){

		if (active && !isReactivable) {
			return EnumIconeInterraction.icone_null;
		}

		if (!isActivable()) {
			return EnumIconeInterraction.icone_null;
		}

		return EnumIconeInterraction.icone_default;
	}


}
