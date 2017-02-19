﻿using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Text.RegularExpressions;


public class DialogUI : MonoBehaviour
{
    public Text dialogField;
    public Text speaker;
    public Image dialogBox;
    public Image speakerBox;

    public bool dialogCompleted;

    public AudioClip defaultTextSfx;

    public string dialog = "";
    private int dialogTracker = 0;

    private float textDisplaySpeed;
    private float textDisplayTimer;

    private const float FAST_DISPLAY_SPEED = 0.0f;
    private const float SLOW_DISPLAY_SPEED = 0.03f;

    private AudioClip textSfx;

    // Use this for initialization
    void Awake()
    {
        dialogField.text = "";
        speaker.text = "";
        dialogCompleted = true;
    }

    // Update is called once per frame
    void Update()
    {
        //Do something where text appears according to the textDisplaySpeed
        if (textDisplayTimer > 0)
        {
            textDisplayTimer -= Time.deltaTime;
            return;
        }

        if (this.dialogField.text != dialog)
        {
            char[] dialogCharArray = new char[dialog.Length];
            dialogCharArray = this.dialogField.text.ToCharArray();
            dialogCharArray[dialogTracker] = dialog[dialogTracker];
            this.dialogField.text = new string(dialogCharArray);
            dialogTracker++;

            //This can be pretty jank, probably need to make an audio source onthe camera or something e-e
            AudioSource.PlayClipAtPoint(textSfx, CameraControls.instance.transform.position);

            textDisplayTimer = textDisplaySpeed;
            dialogCompleted = false;
        }
        else
            dialogCompleted = true;
    }

    public void displayDialog(string dialog, string speaker = "", AudioClip sfx = null, DisplaySpeed displaySpeed = DisplaySpeed.fast)
    {
        SetDialogSound(sfx);

        dialogCompleted = false;

        this.gameObject.SetActive(true);
        this.speaker.text = speaker;
        this.dialog = dialog;
        this.dialogTracker = 0;

        string taggedText = "";
        for (int i = 0; i < dialog.Length; i++)
        {
            if (dialog[i] == '<')
            {
                string insertTag = "";
                while (dialog[i] != '>')
                {
                    insertTag += dialog[i];
                    i++;
                }
                insertTag += dialog[i];
                taggedText += insertTag;
            }
            else
            {
                taggedText += " ";
            }
        }
        //Prevents the name from flickering
        this.dialogField.text = taggedText;


        setSpeed (displaySpeed);
    }

    /// <summary>
    /// use this to set custom sfx for what audio plays when the text shows up
    /// should be set before every displayDialog call
    /// </summary>
    /// <param name="sfx"></param>
    private void SetDialogSound(AudioClip sfx)
    {
        if (sfx == null)
            textSfx = defaultTextSfx;
        else
            textSfx = sfx;
    }

    public void closeDialog()
    {
        this.gameObject.SetActive(false);
        //this.dialog = "";
        this.dialogTracker = 0;
    }

    public void resolveDialog()
    {
        this.dialogField.text = dialog;
    }

    public void setSpeed(DisplaySpeed displaySpeed) {
        if (displaySpeed == DisplaySpeed.immediate)
        {
            this.dialogField.text = dialog;
        }
        else if (displaySpeed == DisplaySpeed.fast)
        {
            textDisplaySpeed = FAST_DISPLAY_SPEED;
        }
        else if (displaySpeed == DisplaySpeed.slow)
        {
            textDisplaySpeed = SLOW_DISPLAY_SPEED;
        }
    }
}

public enum DisplaySpeed
{
    immediate,
    slow,
    fast
}