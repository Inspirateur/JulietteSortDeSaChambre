using System.Collections;
using System.Collections.Generic;
using UnityEngine;



public class TestCamera : MonoBehaviour {

		public Transform camera_transform;

		private Vector3 velocity = Vector3.zero;

	// Use this for initialization
	void Start () {
		camera_transform = transform;
	}
	
	// Update is called once per frame
	void Update () {
		camera_transform.position = Vector3.SmoothDamp(camera_transform.position,GameObject.FindGameObjectWithTag("MainCamera").transform.position,ref velocity, 3.2F);
		Debug.Log(camera_transform.position);
	}
}
