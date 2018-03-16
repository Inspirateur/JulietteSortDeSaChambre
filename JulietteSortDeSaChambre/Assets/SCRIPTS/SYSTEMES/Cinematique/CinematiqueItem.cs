using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class CinematiqueItem {

	public int dureeArret = 0;

	public Vector3 pos;
	public Vector3 rot;

	public void start(){
		Camera.main.transform.position = pos;
		Camera.main.transform.LookAt (pos+rot);
	}


}
