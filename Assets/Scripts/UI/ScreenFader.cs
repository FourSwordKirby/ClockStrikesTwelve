﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ScreenFader : MonoBehaviour
{
    public Image screen;
    float fadeTime;
    public bool fading;
    public bool fadeActive { get { return this.screen.color != Color.clear; } set {; }}

    void Awake()
    {
        this.screen.color = Color.clear;
    }

    public IEnumerator FadeOut(float fadeTime = 1.0f)
    {
        fading = true;
        float timer = 0.0f;
        this.fadeTime = fadeTime;
        while (timer < fadeTime)
        {
            timer += Time.deltaTime;
            if (timer < fadeTime)
            {
                screen.color = Color.Lerp(Color.white - Color.black, Color.white, timer / fadeTime);
                yield return new WaitForSeconds(0.01f);
            }
        }
        fading = false;
        yield return null;
    }

    public IEnumerator FadeIn(float fadeTime = 1.0f)
    {
        fading = true;
        float timer = 0.0f;
        this.fadeTime = fadeTime;
        while (timer < fadeTime)
        {
            timer += Time.deltaTime;
            if (timer < fadeTime)
            {
                screen.color = Color.Lerp(Color.white, Color.white - Color.black, timer / fadeTime);
                yield return new WaitForSeconds(0.01f);
            }
        }
        fading = false;
        yield return null;
    }

    /// <summary>
    /// This is used to force the screenfader to suddenly be set to some specified color
    /// </summary>
    public void CutToColor(Color color)
    {
        screen.color = color;
        fading = false;
    }
}