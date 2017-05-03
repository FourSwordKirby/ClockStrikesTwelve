using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class HallwaySceneSetup : MonoBehaviour {

    public GameObject doorLock;
    public GameObject weedMusic;

	void Start ()
    {
        if (QuestManager.instance.tvOff)
        {
            weedMusic.SetActive(false);
        }
        else
        {
            weedMusic.SetActive(true);
        }

        if (UIController.instance.screenfader.fadeActive)
        {
            StartCoroutine(UIController.instance.screenfader.FadeIn());
        }

        if (QuestManager.instance.maintenanceCompleted)
        {
            doorLock.SetActive(false);
        }
	}
}
