using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class HallwaySceneSetup : MonoBehaviour {

    public GameObject doorLock;

	void Start ()
    {
        if (UIController.instance.screenfader.fadeActive)
        {
            StartCoroutine(UIController.instance.screenfader.FadeIn());
        }

        if (GameObject.Find("QuestManager").GetComponent<QuestManager>().maintencenceRequestCalled)
        {
            doorLock.SetActive(false);
        }
	}
}
