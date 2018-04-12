using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using System.Runtime.Serialization.Formatters.Binary;
using System.IO;
using UnityEngine.SceneManagement;

public class GameControl : MonoBehaviour {

	public static GameControl control;

	public int vie;
    public EnumArmes ArmeCourante;
	public List<EnumArmes> listArmeTenu;
	public int skinIndex;

	void Awake(){
		if (control == null) {
			DontDestroyOnLoad (gameObject);
			control = this;
		} else if (control != this) {
			Destroy (gameObject);
		}
		if(!SceneManager.GetActiveScene().name.Equals("Chambre")){
			Load();
			Debug.Log("gfhbreg ureh gyue trhv rbtvjh brjhvbr esjvrsv jrsbeuvser ");
		}
	}

	public void Save()
	{
		BinaryFormatter bf = new BinaryFormatter ();
		FileStream file = File.Create(Application.persistentDataPath + "/playerInfo.dat");

		PlayerData data = new PlayerData();
		data.vie = vie;
	    data.ArmeCourante = ArmeCourante;
	    data.listArmeTenu = listArmeTenu;
		data.skinIndex = skinIndex;

		bf.Serialize(file, data);
		file.Close();
	}

	public void Load()
	{
		if (File.Exists (Application.persistentDataPath + "/playerInfo.dat")) {
			BinaryFormatter bf = new BinaryFormatter ();
			FileStream file = File.Open(Application.persistentDataPath + "/playerInfo.dat", FileMode.Open);
			PlayerData data = (PlayerData)bf.Deserialize(file);
			file.Close();

			vie = data.vie;
		    ArmeCourante = data.ArmeCourante;
		    listArmeTenu = data.listArmeTenu;
			skinIndex = data.skinIndex;
		}
	}

}

[Serializable]
class PlayerData
{
	public int vie;
	public EnumArmes ArmeCourante;
	public List<EnumArmes> listArmeTenu;
	public int skinIndex;
}