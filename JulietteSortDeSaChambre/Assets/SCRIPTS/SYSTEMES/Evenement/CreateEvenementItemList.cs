using UnityEngine;
using System.Collections;
using UnityEditor;

public class CreateEvenementItemList {


	[MenuItem("Assets/Create/Evenement Item List")]
	public static EvenementItemList  Create()
	{
		EvenementItemList asset = ScriptableObject.CreateInstance<EvenementItemList>();

		AssetDatabase.CreateAsset(asset, "Assets/InventoryItemList.asset");
		AssetDatabase.SaveAssets();
		return asset;
	}
}
