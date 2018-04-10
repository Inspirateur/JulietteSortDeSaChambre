using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
public class IntroScene : MonoBehaviour {
 public string SceneName;
 public float timer;
 public Text textPasserCinematique;
 private UnityEngine.Video.VideoPlayer cam;

	// Use this for initialization
	void Start () {
		textPasserCinematique.enabled = false;
		timer = Time.time + timer;
		cam = GameObject.Find("Main Camera").GetComponent<UnityEngine.Video.VideoPlayer>();
		cam.Play();
	}
	
	// Update is called once per frame
	void Update () {
		if(Time.time > timer){
			textPasserCinematique.enabled = true;
			if(Input.GetButtonDown("Interagir")|| !cam.isPlaying){
		  	SceneManager.LoadScene(SceneName);
			}
		}
	}
	
}
