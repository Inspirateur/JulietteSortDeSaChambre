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

		}

	}

}

