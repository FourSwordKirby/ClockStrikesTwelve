﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class DayEndEvent : MonoBehaviour {
    public DayEndSpook spookPrefab;
    bool inRoom = false;
    bool dieded = false;
    bool dayEnding = false;
    public float spawnDistance;
    public float spawnRNG;
    public float minSpawn;
    public float maxSpawn;

    public static DayEndEvent instance;
    void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(this);
        }
        else
        {
            if (this != instance)
            {
                Destroy(this.gameObject);
            }
        }
    }

    private void Update()
    {
        if (dayEnding && SceneManager.GetActiveScene().name == "PlayerBedroom")
        {
            StartCoroutine(GameManager.instance.ResetDay());
            dayEnding = false;
        }
    }

    public IEnumerator DayEnd()
    {
        if (!dayEnding)
        {
            dieded = false;
            dayEnding = true;
            if (SceneManager.GetActiveScene().name == "PlayerBedroom")
            {
                StartCoroutine(GameManager.instance.ResetDay());
                dayEnding = false;
                yield return null;
            }
            else
            {
                GameManager.instance.playSound(SoundType.Environment, "Spook");
                yield return StartCoroutine(spoooooooooooook());

                for (int i = 0; i < Random.Range(minSpawn, maxSpawn); i++)
                {
                    spawnSpooks();
                }
            }
        }
    }

    void spawnSpooks()
    {
        float x = Random.Range(-spawnRNG, spawnRNG);
        float y = Random.Range(-spawnRNG, spawnRNG);
        if (x < 0) x -= spawnDistance;
        else x += spawnDistance;
        if (y < 0) y -= spawnDistance;
        else y += spawnDistance;
        DayEndSpook instance = Instantiate(spookPrefab);
        instance.transform.position = new Vector3(x, y);
    }

    IEnumerator spoooooooooooook()
    {
        //Clock chimes 12

        //Dim screen
        GameManager.instance.roomSwitchSet(false);
        yield return StartCoroutine(UIController.instance.screenfader.Dim());

        //Rumbling/wierd images?

        //finishes when clock finishes chiming
        yield return null;
    }

    public void playerHit()
    {
        //fade to dark
        if (!dieded)
        {
            dieded = true;
            Player.instance.FreezePlayer();
            StartCoroutine(GameManager.instance.ResetDay());
            dayEnding = false;
        }
    }

    public bool isDayEnding()
    {
        return dayEnding;
    }

    
}
