using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

public class Porte : ObjetEnvironnemental {
	
	private  Animator anim;
	public bool isDecorative;

	public List<ObjetNecessaire> objN;
	private PrincesseObjetProgression juliette;



	// Use this for initialization
	void Start () {
		anim = GetComponent<Animator>();
		objN = new List<ObjetNecessaire>();
		juliette= GameObject.FindGameObjectWithTag("Player").GetComponent<PrincesseObjetProgression>();
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	public override void Activation(){
		if (!isDecorative ) {
			if (isActivable()) {
				anim.SetBool("isOpen", true);
				isDecorative = true;
			}

		}
	}


	private bool isActivable(){

		foreach (ObjetNecessaire obj in objN) {
			if (juliette.listObjet.ContainsKey (obj.objet)) {
				if (juliette.listObjet [obj.objet] < obj.nombre) {
					return false;
				}
			}
		}

		return true;
	}

	public override EnumIconeInterraction getIconeInteraction ()
	{
		if (!isDecorative) {
			return EnumIconeInterraction.icone_null;
		}
		return base.getIconeInteraction ();
	}
}
