using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class BedroomSceneSetup : MonoBehaviour {
    
    public GameObject charm;

	// Use this for initialization
	void Start () {
        bool hasCharm = GameManager.instance.player.items.Where(x => x.designation == ItemDesignation.WeatherCharm).Count() == 1
                        || GameManager.instance.chestStoredItems.Where(x => x.designation == ItemDesignation.WeatherCharm).Count() == 1;
        charm.SetActive(!hasCharm);
	}
}
