using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GOB_PouvoirGlace : MonoBehaviour {
    private bool isIced;
    private IA_Agent agent;

	// Use this for initialization
	void Start () {
        isIced = false;
        this.agent = GetComponent<IA_Agent>();
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "PouvoirGlace" && !isIced)
        {
            agent.changerEtat(GetComponent<GOB_E_Glace>());
            isIced = true;
        }
    }

    public void notifierNestPlusGlace(){
        this.isIced = false;
    }
}
