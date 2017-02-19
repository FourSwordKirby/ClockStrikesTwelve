using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneChangeTrigger : MonoBehaviour {

    public string sceneName;

    void OnTriggerEnter2D(Collider2D col)
    {
        GameManager.instance.StartSceneTransition (sceneName);
    }
}
