using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheckPointableMob : CheckPointableEntity {

	// void Awake() {
	// 	Debug.Log("CheckPointableMob Awake start");

	// 	Debug.Log("CheckPointableMob Awake end");
	// }

    public override void OnRespawn()
    {
        Debug.Log(gameObject.ToString() + " : RESTART");
    }
}
