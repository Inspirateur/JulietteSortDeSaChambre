using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tuto : ObjetEnvironnemental {



	public bool active;

	// Use this for initialization
	void Start () {
		active = true;
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	public override void Activation ()
	{
		if(active){
			active = false;
			GetComponent<EventManager> ().activation ();
		}

	}

	public override EnumIconeInterraction getIconeInteraction ()
	{
		if(active){
			return EnumIconeInterraction.icone_default;
		}
		return EnumIconeInterraction.icone_null;
	}


}
