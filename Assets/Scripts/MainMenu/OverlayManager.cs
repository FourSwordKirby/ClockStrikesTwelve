using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class OverlayManager : MonoBehaviour {

    public static OverlayManager instance;

    public Image fade;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(this);
            gameObject.GetComponent<Canvas>().worldCamera = GameObject.Find("Main Camera").GetComponent<Camera>();
        }
        else if (this != instance)
        {
            Destroy(this.gameObject);
        }
    }

    public void init()
    {
        fade.canvasRenderer.SetAlpha(255f);
        StartCoroutine(fadeOut());
        
    }

    public IEnumerator fadeIn()
    {
        if (fade.canvas.worldCamera == null) fade.canvas.worldCamera = GameObject.Find("Main Camera").GetComponent<Camera>();
        float idx = 0f;
        while (fade.canvasRenderer.GetAlpha() < 250f)
        {
            yield return new WaitForSecondsRealtime(0.02f);
            fade.canvasRenderer.SetAlpha(idx);
            idx += 25f;
        }
    }

    public IEnumerator fadeOut()
    {
        if (fade.canvas.worldCamera == null) fade.canvas.worldCamera = GameObject.Find("Main Camera").GetComponent<Camera>();
        float idx = 255f;
        while (fade.canvasRenderer.GetAlpha() > 5f)
        {
            yield return new WaitForSecondsRealtime(0.02f);
            fade.canvasRenderer.SetAlpha(idx);
            idx -= 25f;
        }
    }
}
