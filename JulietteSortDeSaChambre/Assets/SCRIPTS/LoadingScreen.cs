﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LoadingScreen : MonoBehaviour {

    void Start() {
		Cursor.visible = false;
		SceneManager.LoadSceneAsync(PlayerPrefs.GetString("SceneToLoad"));
    }

}
