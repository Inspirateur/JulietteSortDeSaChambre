using System;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public struct Eve  {

	public Evenement go;

	public List<string> name;
	public List<int> indice;

	public List<string> nameM;
	public List<int> indiceM;



	public List<System.TypeCode> enumParam;
	public int paramInt;


	public void clearAll(){
		name.Clear ();
		nameM.Clear ();
	}
}
