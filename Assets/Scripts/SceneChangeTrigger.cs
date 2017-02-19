using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneChangeTrigger : MonoBehaviour {

    public string sceneName;
    private bool activated = false;

    void OnTriggerEnter2D(Collider2D col)
    {
        if (!activated)
        {
            GameManager.instance.StartSceneTransition (sceneName);
            activated = true;
        }
    }
}
