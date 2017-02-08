using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OutsideSceneSetup : MonoBehaviour {

    public GameObject SnowParticles;
    public GameObject SnowBackground;
    public GameObject DryBackground;

    private void Start()
    {
        if(UIController.instance.screenfader.fadeActive)
            StartCoroutine(UIController.instance.screenfader.FadeIn());

        if (GameManager.instance.charmTriggered)
        {
            SnowParticles.SetActive(false);
            SnowBackground.SetActive(false);
            DryBackground.SetActive(true);
        }
        else
        {
            SnowParticles.SetActive(true);
            SnowBackground.SetActive(true);
            DryBackground.SetActive(false);
        }
    }
}
