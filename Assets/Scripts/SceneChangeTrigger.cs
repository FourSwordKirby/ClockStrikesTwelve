using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneChangeTrigger : MonoBehaviour {

    public int sceneNumber;

    void OnTriggerEnter2D(Collider2D col)
    {
        StartCoroutine(transitionRooms());
    }

    public IEnumerator transitionRooms()
    {
        StartCoroutine(UIController.instance.screenfader.FadeOut());
        while (UIController.instance.screenfader.fading)
        {
            yield return new WaitForSeconds(0.1f);
        }
        SceneManager.LoadScene(sceneNumber);
        yield return null;
    }
}
