using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class NormalSceneSetup : MonoBehaviour {
    
	void Start ()
    {
        if (UIController.instance.screenfader.fadeActive)
        {
            StartCoroutine(UIController.instance.screenfader.FadeIn());
        }
	}
}
