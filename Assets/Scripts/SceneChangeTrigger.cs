using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneChangeTrigger : MonoBehaviour {

    public string sceneName;
    private bool activated = false;

    void OnTriggerEnter2D(Collider2D col)
    {
        bool isDayEnding = GameManager.instance.dayEnd.isDayEnding();
        if ((!isDayEnding && !activated && GameManager.instance.canSwitchRooms()) ||
            (sceneName == "PlayerBedroom"))
        {
            MonoBehaviour.print(sceneName);
            MonoBehaviour.print(GameManager.instance.dayEnd.isDayEnding());
            MonoBehaviour.print(sceneName == "PlayerBedroom");
            GameManager.instance.StartSceneTransition (sceneName);
            activated = true;
        }
    }
}
