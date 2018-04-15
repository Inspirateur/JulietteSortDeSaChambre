using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using System;
using System.Reflection;

public class EventManager : MonoBehaviour {


	 
	public List<Eve> es;

	public int index;
	public string nomEvent;

	public void activation(){
		index = 0;
//		Debug.Log ("ActivationEvenement");
		if(index < es.Count){
			lancerEvenement (es[index]);
		}
	}

	IEnumerator timer(Eve e){
		for(var i=0;i<1;i++){
			Debug.Log ("WAIT FIN "+e.name[0]);
			yield return new WaitWhile (()=> e.go.evenementIsEnCours());
			Debug.Log ("UNWAIT"+e.name[0]);
		}
		index++;
		if(index < es.Count){
			lancerEvenement (es[index]);
		}
	}


	public void lancerEvenement(Eve e){
		// Debug.Log("EVENEMENT :"+e.name[0]);
		MethodInfo m = e.go.GetType ().GetMethod (e.nameM [e.indiceM [0]]);
		if (e.enumParam[0] != System.TypeCode.DBNull) {
			object[] objectTemp = new object[1];
			switch (e.enumParam[0]) {
			case System.TypeCode.Int32:
				objectTemp [0] = e.paramInt;
				break;
			case System.TypeCode.Boolean:
				objectTemp [0] = e.paramBool;
				break;

			}
			m.Invoke (e.go,objectTemp);
		} else {
			m.Invoke (e.go,null);
		}

		GameControl.control.StartCoroutine (timer (e));
	}

}

