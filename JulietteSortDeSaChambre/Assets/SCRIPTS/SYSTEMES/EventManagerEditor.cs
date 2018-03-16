using UnityEngine;  
#if UNITY_EDITOR
using UnityEditor;  
using UnityEditorInternal;
using System.Collections.Generic;
using System.Reflection;


[CustomEditor(typeof(EventManager))]
public class EventManagerEditor : Editor {  



	private ReorderableList list;

	private ReorderableList list2;





	private void OnEnable() {
		list = new ReorderableList(serializedObject, 
			serializedObject.FindProperty("es"), 
			true, true, true, true);
		list.drawElementCallback =  
			
			(Rect rect, int index, bool isActive, bool isFocused) => {
			var element = list.serializedProperty.GetArrayElementAtIndex(index);
			rect.y += 2;
			EditorGUI.PropertyField(
				new Rect(rect.x, rect.y,rect.width, EditorGUIUtility.singleLineHeight),
				element.FindPropertyRelative("go"), GUIContent.none);


			EventManager myEventM = (EventManager)target;
			if(myEventM.es[index].go!=null){
				myEventM.es[index].clearAll();

				Evenement[] listEvenement = myEventM.es[index].go.GetComponents<Evenement>();
				for(var i=0;i<listEvenement.Length;i++){
					myEventM.es[index].name.Add(listEvenement[i].GetType().FullName);
				}
				if(myEventM.es[index].indice.Count<1){
					myEventM.es[index].indice.Add(0);
				}
				myEventM.es[index].indice[0] = EditorGUI.Popup(
					new Rect(rect.x, rect.y+rect.height/4, rect.width/2, EditorGUIUtility.singleLineHeight),
					myEventM.es[index].indice[0],
					myEventM.es[index].name.ToArray());


				string nameScript = myEventM.es[index].name[myEventM.es[index].indice[0]];

				foreach(Evenement e in listEvenement){
					if(e.GetType().Name.Equals(nameScript)){
						MethodInfo[] m=  e.GetType().GetMethods();
						for(var i=0;i<m.Length;i++){

							if(m[i].GetParameters().Length<=1 && m[i].ReturnType.Name.Equals("Void")) {
								if(m[i].GetParameters().Length==1){
									if(m[i].GetParameters()[0].ParameterType.Equals(typeof(int))){
										myEventM.es[index].nameM.Add(m[i].Name);
									}
								}else{
									myEventM.es[index].nameM.Add(m[i].Name);
								}
							}
						}
					}
				}

				if(myEventM.es[index].indiceM.Count<1){
					myEventM.es[index].indiceM.Add(0);
				}

				myEventM.es[index].indiceM[0] = EditorGUI.Popup(
					new Rect(rect.x+rect.width/2, rect.y+rect.height/4,rect.width/2, EditorGUIUtility.singleLineHeight),
					myEventM.es[index].indiceM[0],
					myEventM.es[index].nameM.ToArray());

				if(myEventM.es[index].enumParam.Count<1){
					myEventM.es[index].enumParam.Add(System.TypeCode.DBNull);
				}

				foreach(Evenement e in listEvenement){
					if(e.GetType().Name.Equals(nameScript)){
						MethodInfo[] m=  e.GetType().GetMethods();
						for(var i=0;i<m.Length;i++){
							if(m[i].Name.Equals(myEventM.es[index].nameM[myEventM.es[index].indiceM[0]])){
								if(m[i].GetParameters().Length==1){
									if(m[i].GetParameters()[0].ParameterType.Equals(typeof(int))){
										myEventM.es[index].enumParam[0]=System.TypeCode.Int32;
										EditorGUI.PropertyField(
											new Rect(rect.x, rect.y+(2*rect.height/4),rect.width/2, EditorGUIUtility.singleLineHeight),
											element.FindPropertyRelative("paramInt"), GUIContent.none);
									}else{
										myEventM.es[index].enumParam[0]=System.TypeCode.DBNull;
									}
								}else{
									myEventM.es[index].enumParam[0]=System.TypeCode.DBNull;
								}
							}
						}
					}
				}



			}

		};
		list.drawHeaderCallback = (Rect rect) => {  
			EditorGUI.LabelField(rect, "Evenement");
		};

		list.elementHeight = EditorGUIUtility.singleLineHeight*4;





	}

	public override void OnInspectorGUI() {

		base.OnInspectorGUI ();
	

		serializedObject.Update();
		//list.DoLayoutList();
		list.DoLayoutList();
		// Begin checking for changes to `GUI.changed`.
		serializedObject.ApplyModifiedProperties();


	}



}

#endif
