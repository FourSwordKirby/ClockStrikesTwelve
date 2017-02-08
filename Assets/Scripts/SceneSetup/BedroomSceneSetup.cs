using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class BedroomSceneSetup : MonoBehaviour {
    
    public GameObject charm;

	// Use this for initialization
	void Start ()
    {
        if (GameManager.instance.introCompleted)
        {
            if (UIController.instance.screenfader.fadeActive)
                StartCoroutine(UIController.instance.screenfader.FadeIn());
        }


        bool hasCharm = Player.instance.items.Where(x => x.designation == ItemDesignation.WeatherCharm).Count() == 1
                        || GameManager.instance.chestStoredItems.Where(x => x.designation == ItemDesignation.WeatherCharm).Count() == 1;
        charm.SetActive(!hasCharm);
	}
}
