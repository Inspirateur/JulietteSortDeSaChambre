using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LoadingScreen : MonoBehaviour {

    void Start() {
        StartCoroutine(LoadSceneAsynchronously(PlayerPrefs.GetString("SceneToLoad")));
    }

	public IEnumerator LoadSceneAsynchronously (string SceneName) {
        AsyncOperation op = SceneManager.LoadSceneAsync(SceneName);
    
        while (!op.isDone){
            yield return null;
        }
    }

}
