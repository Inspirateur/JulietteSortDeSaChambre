using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrapeEvenement : Evenement {
    private Trape trape;

    void Start () {
        trape = this.GetComponent<Trape>();
    }

    public override void activation(){
        trape.TrapeOuverture ();
    }

    public override void desactivation(){
        trape.TrapeFermeture ();
    }
}
