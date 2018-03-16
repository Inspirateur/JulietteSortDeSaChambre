using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Trape : MonoBehaviour {

    private Animator Anim;

    // public MeshCollider Door1;
    // public MeshCollider Door2;

    void Start()
    {
        Anim = GetComponent<Animator>();
        Anim.SetBool("CanOpen", false);
    }

    public void TrapeOuverture(){
        Anim.SetBool ("CanOpen", true);
        GetComponent<BoxCollider>().enabled = false;
        // Door1.enabled = false;
        // Door2.enabled = false;
    }

	public void TrapeFermeture(){
        Anim.SetBool ("CanOpen", false);
        GetComponent<BoxCollider>().enabled = true;
        // Door1.enabled = true;
        // Door2.enabled = true;
    }

}
