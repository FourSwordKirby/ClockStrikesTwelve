using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class BedroomSceneSetup : MonoBehaviour {
    
    public GameObject toilet;

	// Use this for initialization
	void Start ()
    {
        if (QuestManager.instance.introCompleted)
        {
            if (UIController.instance.screenfader.fadeActive)
                StartCoroutine(UIController.instance.screenfader.FadeIn());
        }

        bool dayStart = true;
        toilet.SetActive(dayStart);
	}
}
