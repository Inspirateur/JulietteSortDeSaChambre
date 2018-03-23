using System.Collections;
using System.Collections.Generic;
using UnityEngine;
#if UNITY_EDITOR
using UnityEditor;

public class CreateCinematiqueItemList  {
	[MenuItem("Assets/Create/Cinematique Item List")]
	public static CinematiqueItemList  Create(string name)
	{
		CinematiqueItemList asset = ScriptableObject.CreateInstance<CinematiqueItemList>();

		AssetDatabase.CreateAsset(asset, "Assets/CINEMATIQUE/"+name+".asset");
		AssetDatabase.SaveAssets();
		return asset;
	}
}
#endif
