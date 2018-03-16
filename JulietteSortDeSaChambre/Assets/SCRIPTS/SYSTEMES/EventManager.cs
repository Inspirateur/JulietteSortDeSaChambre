using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using System;
using System.Reflection;

public class EventManager : MonoBehaviour {
	 
	public List<Eve> es;


	public string nomEvent;

	public void activation(){
		foreach(Eve e in es){
			MethodInfo m = e.go.GetType ().GetMethod (e.nameM [e.indiceM [0]]);
			if (e.enumParam[0] != System.TypeCode.DBNull) {
				switch (e.enumParam[0]) {
				case System.TypeCode.Int32:
					object[] objectTemp = new object[1];
					objectTemp [0] = e.paramInt;
					m.Invoke (e.go,objectTemp);
					break;

				}
			} else {
				m.Invoke (e.go,null);
			}

		}

	}

}

