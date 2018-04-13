using UnityEngine;
#if UNITY_EDITOR
using UnityEditor;
using System.Collections;
using System.Collections.Generic;

public class CinematiqueWindowEditor : EditorWindow {

	public CinematiqueItemList cinematiqueItemList;
	private int viewIndex = 1;
	string name;
	string nameActu;
	public bool simulationNonActive;
	GameObject simulation;

	[MenuItem ("Window/Cinématique Editor %#e")]
	static void  Init () 
	{
		EditorWindow.GetWindow (typeof (CinematiqueWindowEditor));
	}

	void  OnEnable () {
		if(EditorPrefs.HasKey("ObjectPath")) 
		{
			string objectPath = EditorPrefs.GetString("ObjectPath");
			cinematiqueItemList = AssetDatabase.LoadAssetAtPath (objectPath, typeof(CinematiqueItemList)) as CinematiqueItemList;
		}

	}

	void  OnGUI () {
		GUILayout.BeginHorizontal ();
		GUILayout.Label ("Cinematique Item Editor : " + nameActu, EditorStyles.boldLabel);
		GUILayout.EndHorizontal ();


		GUILayout.BeginHorizontal ();
		GUILayout.Space(10);
		if (GUILayout.Button("Create New Item List", GUILayout.ExpandWidth(false))) 
		{
			CreateNewItemList();
		}
		name=EditorGUILayout.TextField (name);
		GUILayout.EndHorizontal ();

		GUILayout.BeginHorizontal ();
		if (GUILayout.Button("Open Existing Item List", GUILayout.ExpandWidth(false))) 
		{
			OpenItemList();
		}
		GUILayout.EndHorizontal ();


		GUILayout.BeginHorizontal ();
		if (cinematiqueItemList != null) {
			if (GUILayout.Button ("Show Item List")) {
				//EditorUtility.FocusProjectWindow ();
				Selection.activeObject = cinematiqueItemList;
			}
		} else {
			nameActu = "";
		}
		GUILayout.EndHorizontal ();

		if (cinematiqueItemList != null) 
		{
			GUILayout.BeginHorizontal ();

			cinematiqueItemList.isPassable = EditorGUILayout.Toggle ("Est passable : ", cinematiqueItemList.isPassable);
			cinematiqueItemList.desactiveBandeNoir = EditorGUILayout.Toggle ("Desactiver bande noire : ", cinematiqueItemList.desactiveBandeNoir);
			cinematiqueItemList.desactiveRetourCamera = EditorGUILayout.Toggle ("Desactiver Retour : ", cinematiqueItemList.desactiveRetourCamera);
			GUILayout.EndHorizontal ();
			GUILayout.BeginHorizontal ();

			if (GUILayout.Button("Prev", GUILayout.ExpandWidth(false))) 
			{
				if (viewIndex > 1)
					viewIndex --;
			}
			//viewIndex = Mathf.Clamp (EditorGUILayout.IntField (viewIndex, GUILayout.ExpandWidth(false)), 1, cinematiqueItemList.itemList.Count);
			EditorGUILayout.LabelField (viewIndex+"  of  " +  cinematiqueItemList.itemList.Count.ToString() + "  items", "", GUILayout.ExpandWidth(false));


			if (GUILayout.Button("Next", GUILayout.ExpandWidth(false))) 
			{
				if (viewIndex < cinematiqueItemList.itemList.Count) 
				{
					viewIndex ++;
				}
			}

			GUILayout.EndHorizontal ();

			if (GUILayout.Button("Add Item", GUILayout.ExpandWidth(false))) 
			{
				AddItem();
			}
			if (GUILayout.Button("Delete Item", GUILayout.ExpandWidth(false))) 
			{
				DeleteItem(viewIndex - 1);
			}

			if (cinematiqueItemList.itemList.Count > 0) 
			{
				cinematiqueItemList.itemList [viewIndex - 1].isShaking = EditorGUILayout.Toggle ("Tremblement", cinematiqueItemList.itemList [viewIndex - 1].isShaking);
				cinematiqueItemList.itemList [viewIndex - 1].pos = EditorGUILayout.Vector3Field ("Position", cinematiqueItemList.itemList [viewIndex - 1].pos);
				cinematiqueItemList.itemList [viewIndex - 1].rot = EditorGUILayout.Vector3Field ("Rotation", cinematiqueItemList.itemList [viewIndex - 1].rot);
				cinematiqueItemList.itemList [viewIndex - 1].dureeArret = EditorGUILayout.IntField ("Duree arret", cinematiqueItemList.itemList [viewIndex - 1].dureeArret);
				cinematiqueItemList.itemList [viewIndex - 1].dureeAcces = EditorGUILayout.IntField ("Temps pour accéder au point", cinematiqueItemList.itemList [viewIndex - 1].dureeAcces);
				// inventoryItemList.itemList[viewIndex-1].itemObject = EditorGUILayout.ObjectField ("Item Object", inventoryItemList.itemList[viewIndex-1].itemObject, typeof (Rigidbody), false) as Rigidbody;
				cinematiqueItemList.itemList [viewIndex - 1].son = EditorGUILayout.ObjectField("Son",cinematiqueItemList.itemList [viewIndex - 1].son,typeof(AudioClip),false) as AudioClip;
				cinematiqueItemList.itemList [viewIndex - 1].texte = EditorGUILayout.TextArea (cinematiqueItemList.itemList [viewIndex - 1].texte);

				if(GUILayout.Button("Simulation", GUILayout.ExpandWidth(false))){
					Selection.activeObject = GameObject.FindGameObjectWithTag ("MainCamera");
					GameObject.FindGameObjectWithTag ("MainCamera").transform.position = cinematiqueItemList.itemList [viewIndex - 1].pos;
					GameObject.FindGameObjectWithTag ("MainCamera").transform.forward = cinematiqueItemList.itemList [viewIndex - 1].rot;
					simulationNonActive = false;
				}

				if (!simulationNonActive) {
					if(GUILayout.Button("TakePos", GUILayout.ExpandWidth(false))){
						cinematiqueItemList.itemList [viewIndex - 1].pos = GameObject.FindGameObjectWithTag ("MainCamera").transform.position;
						cinematiqueItemList.itemList [viewIndex - 1].rot = GameObject.FindGameObjectWithTag ("MainCamera").transform.forward;
					}
				}
		
			} 
			else 
			{
				GUILayout.Label ("This Inventory List is Empty.");
			}

			if (GUI.changed) 
			{
				EditorUtility.SetDirty(cinematiqueItemList);
			}


		}



	}

	public void CreateNewItemList(){
		viewIndex = 0;
		cinematiqueItemList = CreateCinematiqueItemList.Create(name);
		if (cinematiqueItemList) 
		{
			cinematiqueItemList.itemList = new List<CinematiqueItem>();
			string relPath = AssetDatabase.GetAssetPath(cinematiqueItemList);
			EditorPrefs.SetString("ObjectPath", relPath);
			nameActu = relPath;

		}

	}

	public void OpenItemList(){
		viewIndex = 1;
		string absPath = EditorUtility.OpenFilePanel ("Select Inventory Item List", "", "");
		if (absPath.StartsWith(Application.dataPath)) 
		{
			string relPath = absPath.Substring(Application.dataPath.Length - "Assets".Length);
			cinematiqueItemList = AssetDatabase.LoadAssetAtPath (relPath, typeof(CinematiqueItemList)) as CinematiqueItemList;
			if (cinematiqueItemList.itemList == null)
				cinematiqueItemList.itemList = new List<CinematiqueItem>();
			if (cinematiqueItemList) {
				EditorPrefs.SetString("ObjectPath", relPath);
			}
			nameActu = relPath;
		}


	}

	public void AddItem(){
		CinematiqueItem newItem = new CinematiqueItem();
		cinematiqueItemList.itemList.Add (newItem);
		viewIndex = cinematiqueItemList.itemList.Count;
	}

	public void DeleteItem(int index){
		cinematiqueItemList.itemList.RemoveAt (index);
		if(index!=0){
			viewIndex--;
		}
		if (cinematiqueItemList.itemList.Count == 0) {
			viewIndex = 0;
		}
	}

}
#endif

