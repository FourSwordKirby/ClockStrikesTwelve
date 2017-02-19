using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneChangeTrigger : MonoBehaviour {

    public string sceneName;

    void OnTriggerEnter2D(Collider2D col)
    {
        StartCoroutine(transitionRooms());
    }

    public IEnumerator transitionRooms()
    {
        GameManager.instance.playSound(SoundType.Environment, "RoomExit");
        StartCoroutine(UIController.instance.screenfader.FadeOut());
        while (UIController.instance.screenfader.fading)
        {
            yield return new WaitForSeconds(0.1f);
        }
        print ("starting load");
        SceneManager.LoadScene(sceneName);
        print ("loaded 1");
        yield return null;
        //try to move to the position of the door to this room
        print("all loaded");
    }
}
