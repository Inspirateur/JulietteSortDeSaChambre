using UnityEngine;  
#if UNITY_EDITOR
using UnityEditor;  
using UnityEditorInternal;

[CustomEditor(typeof(AffichageVie))]
public class AffichageVieEditor : Editor {  
	private ReorderableList list;


	private void OnEnable() {
		list = new ReorderableList(serializedObject, 
			serializedObject.FindProperty("listVie"), 
			true, true, true, true);
		list.drawElementCallback =  
			(Rect rect, int index, bool isActive, bool isFocused) => {
			var element = list.serializedProperty.GetArrayElementAtIndex(index);
			rect.y += 2;
			EditorGUI.PropertyField(
				new Rect(rect.x, rect.y,rect.width/2, EditorGUIUtility.singleLineHeight),
				element.FindPropertyRelative("enu"), GUIContent.none);
			EditorGUI.PropertyField(
				new Rect(rect.x+rect.width/2, rect.y,rect.width/2, EditorGUIUtility.singleLineHeight),
				element.FindPropertyRelative("image"), GUIContent.none);
		};
		list.drawHeaderCallback = (Rect rect) => {  
			EditorGUI.LabelField(rect, "Objet de progression nécessaire");
		};


		list.onCanAddCallback = (ReorderableList l) => {  
			return l.count < System.Enum.GetNames(typeof(EnumVieEtat)).Length;
		};
	}

	public override void OnInspectorGUI() {

		base.OnInspectorGUI ();

		serializedObject.Update();
		list.DoLayoutList();
		serializedObject.ApplyModifiedProperties();



	}
}
#endif