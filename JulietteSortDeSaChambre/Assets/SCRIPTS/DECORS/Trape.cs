using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Trape : MonoBehaviour {

    private Animator Anim;
    [HideInInspector]
    public bool etatOuvert;

    void Awake()
    {
        Anim = GetComponent<Animator>();
        etatOuvert = false;
        updateEtat();
    }

    public void TrapeOuverture(){
        etatOuvert = true;
        updateEtat();
    }

	public void TrapeFermeture(){
        etatOuvert = false;
        updateEtat();
    }

    public void updateEtat(){
        Anim.SetBool ("CanOpen", etatOuvert);
        GetComponent<BoxCollider>().enabled = !etatOuvert;
    }
}
