using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class DayEndEvent : MonoBehaviour {
    public DayEndSpook spookPrefab;
    bool inRoom = false;
    bool dieded = false;
    public float spawnDistance;
    public float spawnRNG;
    public float minSpawn;
    public float maxSpawn;

    private void Update()
    {
        if (inRoom)
        {
            StartCoroutine(GameManager.instance.ResetDay());
        }
        if (Input.GetKeyDown(KeyCode.Q)) StartCoroutine(DayEnd());
    }

    public IEnumerator DayEnd()
    {
        if(SceneManager.GetActiveScene().name == "PlayerBedroom")
        {
            inRoom = true;
            yield return null;
        }
        else
        {
            yield return StartCoroutine(spoooooooooooook());

            for(int i = 0; i < Random.Range(minSpawn, maxSpawn); i++)
            {
                spawnSpooks();
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
        //UIController.instance.screenfader.

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
            StartCoroutine(GameManager.instance.ResetDay());
        }
    }

    
}
