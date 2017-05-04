using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WriterSceneSetup : MonoBehaviour {
    
    public GameObject weedMusic;

    void Start()
    {
        if (UIController.instance.screenfader.fadeActive)
        {
            StartCoroutine(UIController.instance.screenfader.FadeIn());
        }

        if (QuestManager.instance.tvOff)
        {
            weedMusic.SetActive(false);
        }
        else
        {
            weedMusic.SetActive(true);
        }
    }
}
