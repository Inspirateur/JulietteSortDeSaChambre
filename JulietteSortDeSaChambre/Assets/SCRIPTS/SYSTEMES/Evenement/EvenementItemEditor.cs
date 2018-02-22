using UnityEngine;
using UnityEditor;
using UnityEditorInternal;
using System;
using System.Reflection;
using System.Collections;
using System.Collections.Generic;


public class EvenementItemEditor : EditorWindow {

	public EvenementItemList evenementItemList;
	private int viewIndex = 1;





	[MenuItem ("Window/Evenement Item Editor %#e")]
	static void  Init () 
	{
		EditorWindow.GetWindow (typeof (EvenementItemEditor));
	}

	void  OnEnable () {

		if(EditorPrefs.HasKey("ObjectPath")) 
		{
			string objectPath = EditorPrefs.GetString("ObjectPath");
			evenementItemList = AssetDatabase.LoadAssetAtPath (objectPath, typeof(EvenementItemList)) as EvenementItemList;

		}

	}

	void  OnGUI () {

		GUILayout.Space(10);
		if (evenementItemList != null) {
			if (GUILayout.Button("Show Item List")) 
			{
				EditorUtility.FocusProjectWindow();
				Selection.activeObject = evenementItemList;
			}
		}
		if (GUILayout.Button("Open Item List")) 
		{
			OpenItemList();
		}


		if (evenementItemList == null) {
			if (GUILayout.Button ("Create New Evenement List", GUILayout.ExpandWidth (false))) {
				CreateNewItemList ();
			}
			if (GUILayout.Button("Open Existing Item List", GUILayout.ExpandWidth(false))) 
			{
				OpenItemList();
			}
		}

		if (evenementItemList != null) {
			
			if (GUILayout.Button ("Add Item", GUILayout.ExpandWidth (false))) {
				AddItem ();
			}

			if (evenementItemList.listEvenement.Count > 0) 
			{
				GUILayout.BeginHorizontal ();
				viewIndex = Mathf.Clamp (EditorGUILayout.IntField ("Current Item", viewIndex, GUILayout.ExpandWidth(false)), 1, evenementItemList.listEvenement.Count);
				//Mathf.Clamp (viewIndex, 1, inventoryItemList.itemList.Count);
				EditorGUILayout.LabelField ("of   " +  evenementItemList.listEvenement.Count.ToString() + "  items", "", GUILayout.ExpandWidth(false));
				GUILayout.EndHorizontal ();

				evenementItemList.listEvenement[viewIndex-1].name = EditorGUILayout.TextField ("Item Name", evenementItemList.listEvenement[viewIndex-1].name as string);
				evenementItemList.listEvenement[viewIndex-1].objet = EditorGUILayout.ObjectField ("Objet Evenementiel", evenementItemList.listEvenement[viewIndex-1].objet, typeof (GameObject), true) as GameObject;

				if (evenementItemList.listEvenement [viewIndex - 1].objet != null) {

					Type t = evenementItemList.listEvenement [viewIndex - 1].objet.GetComponent<ObjetEvenementiel> ().GetType ();
					foreach (MethodBase m in t.GetMethods()) {
						if(m.Name.Contains("evenement")){
							evenementItemList.listEvenement [viewIndex - 1].listMethod.Add (m.Name);
						}
					}

					evenementItemList.listEvenement [viewIndex - 1].t = EditorGUILayout.Popup ("test",evenementItemList.listEvenement [viewIndex - 1].t, evenementItemList.listEvenement [viewIndex - 1].listMethod.ToArray ());

					if (evenementItemList.listEvenement [viewIndex - 1].t >= 0) {
						MethodBase m = t.GetMethod(evenementItemList.listEvenement [viewIndex - 1].listMethod[evenementItemList.listEvenement [viewIndex - 1].t]);
						foreach (ParameterInfo p in m.GetParameters()) {
							evenementItemList.listEvenement [viewIndex - 1].listParam.Add (null);
							Debug.Log (p.ParameterType.Name);
							switch (p.ParameterType.Name) {
							case "String":
								evenementItemList.listEvenement [viewIndex - 1].listParam[p.Position] = EditorGUILayout.TextField (evenementItemList.listEvenement [viewIndex - 1].listParam[p.Position] as string) ;
								break;
							}

						}
					}
				}



				GUILayout.Space(10);


			} 
			else 
			{
				GUILayout.Label ("This Inventory List is Empty.");
			}
		}

		if (GUI.changed) 
		{

				EditorUtility.SetDirty (evenementItemList);

		}



	}


	void CreateNewItemList () 
	{
		// There is no overwrite protection here!
		// There is No "Are you sure you want to overwrite your existing object?" if it exists.
		// This should probably get a string from the user to create a new name and pass it ...
		viewIndex = 1;
		evenementItemList = CreateEvenementItemList.Create();
		if (evenementItemList) 
		{
			evenementItemList.listEvenement = new List<EvenementItem>();
			string relPath = AssetDatabase.GetAssetPath(evenementItemList);
			EditorPrefs.SetString("ObjectPath", relPath);
		}


	}

	void AddItem () 
	{
		EvenementItem newItem = new EvenementItem();
		newItem.name = "New Item";
		evenementItemList.listEvenement.Add (newItem);
		viewIndex = evenementItemList.listEvenement.Count;
	}

	void OpenItemList () 
	{
		string absPath = EditorUtility.OpenFilePanel ("Select Inventory Item List", "", "");
		if (absPath.StartsWith(Application.dataPath)) 
		{
			string relPath = absPath.Substring(Application.dataPath.Length - "Assets".Length);
			evenementItemList = AssetDatabase.LoadAssetAtPath (relPath, typeof(EvenementItemList)) as EvenementItemList;
			if (evenementItemList.listEvenement == null)
				evenementItemList.listEvenement = new List<EvenementItem>();
			if (evenementItemList) {
				EditorPrefs.SetString("ObjectPath", relPath);
			}
		}
	}


}
