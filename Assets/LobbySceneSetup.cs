using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LobbySceneSetup : MonoBehaviour
{
    public GameObject LobbyTrigger;

    void Start()
    {
        if (UIController.instance.screenfader.fadeActive)
        {
            StartCoroutine(UIController.instance.screenfader.FadeIn());
        }

        if (QuestManager.instance.introCompleted)
        {
            LobbyTrigger.SetActive(false);
        }
    }
}

