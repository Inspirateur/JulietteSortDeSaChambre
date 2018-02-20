using UnityEngine;  
#if UNITY_EDITOR
using System;
using UnityEditor;  
using UnityEditorInternal;


[CustomEditor(typeof(Levier))]
public class LevierEditor : Editor {

	private ReorderableList list;

	private void OnEnable() {
		list = new ReorderableList(serializedObject, 
			serializedObject.FindProperty("listEvent"), 
			true, true, true, true);
		list.drawElementCallback =  
			(Rect rect, int index, bool isActive, bool isFocused) => {
			var element = list.serializedProperty.GetArrayElementAtIndex(index);
			rect.y += 2;
			EditorGUI.PropertyField(
				new Rect(rect.x, rect.y,rect.width/2, EditorGUIUtility.singleLineHeight),
				element.FindPropertyRelative("objet"), GUIContent.none);

			if(element.FindPropertyRelative("objet").objectReferenceValue!=null){
				var e = (ObjetEvenementiel) element.FindPropertyRelative("objet").objectReferenceValue;
				Type t = e.GetType();

				foreach(System.Reflection.MethodInfo m in t.GetMethods()){
					
				}
			}

		};
		list.drawHeaderCallback = (Rect rect) => {  
			EditorGUI.LabelField(rect, "Liste Evenement");
		};

	
	}

	public override void OnInspectorGUI() {

		base.OnInspectorGUI ();

		serializedObject.Update();
		list.DoLayoutList();
		serializedObject.ApplyModifiedProperties();

		Levier myLevier = (Levier)target;


	}
}
#endif