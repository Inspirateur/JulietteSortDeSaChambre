using UnityEngine;  
#if UNITY_EDITOR
using UnityEditor;  
using UnityEditorInternal;





[CustomEditor(typeof(AffichageInventaire))]
public class AffichageInventaireEditor : Editor {  
	private ReorderableList list;

	private void OnEnable() {
		list = new ReorderableList(serializedObject, 
			serializedObject.FindProperty("listObjet"), 
			true, true, true, true);
		
		list.drawElementCallback =  
			(Rect rect, int index, bool isActive, bool isFocused) => {

			var element = list.serializedProperty.GetArrayElementAtIndex(index);
			rect.y += 3;
			EditorGUI.PropertyField(
				new Rect(rect.x, rect.y,rect.width/2, EditorGUIUtility.singleLineHeight),
				element.FindPropertyRelative("objet"), GUIContent.none);
			EditorGUI.PropertyField(
				new Rect(rect.x+rect.width/2, rect.y,rect.width/2, EditorGUIUtility.singleLineHeight),
				element.FindPropertyRelative("image"), GUIContent.none);
			EditorGUI.PropertyField(
				new Rect(rect.x, rect.y+EditorGUIUtility.singleLineHeight,rect.width/2, EditorGUIUtility.singleLineHeight),
				element.FindPropertyRelative("size"), GUIContent.none);
			EditorGUI.PropertyField(
				new Rect(rect.x, rect.y+EditorGUIUtility.singleLineHeight*2,rect.width/2, EditorGUIUtility.singleLineHeight),
				element.FindPropertyRelative("scale"), GUIContent.none);
		};
		list.drawHeaderCallback = (Rect rect) => {  
			EditorGUI.LabelField(rect, "Liste image");
		};

		list.elementHeightCallback = delegate(int index) {
			var element = list.serializedProperty.GetArrayElementAtIndex(index);
			var elementHeight = EditorGUI.GetPropertyHeight(element)*3;
			var margin = EditorGUIUtility.standardVerticalSpacing;
			return elementHeight + margin;
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