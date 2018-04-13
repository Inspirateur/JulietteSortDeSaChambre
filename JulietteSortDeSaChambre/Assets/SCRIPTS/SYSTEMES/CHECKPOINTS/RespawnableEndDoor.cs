using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RespawnableEndDoor : RespawnableEntity {

    public Animator cadena;
    private Porte porte;
    private bool open;

	public override void onAwake() {
        porte = GetComponent<Porte>();
    }

    public override void setInitialState()
    {
        // Debug.Log(gameObject.ToString() + " : setInitialState");
        open = porte.isDecorative;
    }

    public override void onRespawn()
    {
        // Debug.Log(gameObject.ToString() + " : OnRespawn");
        if(porte.isDecorative != open){
            porte.setOpen(open);
            if(cadena != null){
                cadena.SetBool("CanOpen", open);
            }
        }
    }
}
