using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GroupeSocle : MonoBehaviour {

	public List<Socle> listSocle;
	public bool active;
	private SoundManager sm;

	// Use this for initialization
	void Awake () {
		active = true;
		sm = GameObject.FindGameObjectWithTag("SoundManager").GetComponent<SoundManager>();
	}
	
	// Update is called once per frame
	void Update () {
		if(active){
			if (activeEvent()) {
				GetComponent<EventManager> ().activation ();
				active = false;
				sm.setBackgroundMusic(sm.listeClips[1]);
			}
		}

	}

	public bool activeEvent(){
		foreach (Socle s in listSocle) {
			if (!s.utilise) {
				return false;
			}
		}
		return true;
	}
}
